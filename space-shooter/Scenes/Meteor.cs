using Godot;
using System;

public partial class Meteor : Area2D
{
    private Sprite2D sprite;
    [Export]
    private Texture2D[] sprites;
    private int speed = 400;
    private int directionX = 1;
    private float rotationSpeed = 0;
    private AudioStreamPlayer2D ExplosionSound;
    [Signal]
    public delegate void MeteorHitCollisionEventHandler(Node2D body);
    private bool canCollide = true;
    public override void _Ready()
    {
        ExplosionSound = GetNode<AudioStreamPlayer2D>("ExplosionSound");
        sprite = GetNode<Sprite2D>("MeteorImage");
        RandomizeSprite();

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();
        
        speed = rng.RandiRange(200, 500);
        rotationSpeed = rng.RandfRange(40, 100);

        directionX = rng.RandiRange(-1, 1); 

    

        float width = GetViewport().GetVisibleRect().Size.X;
        float randomX = rng.RandfRange(0, width);
        float randomY = rng.RandfRange(-150, -50);
        Position = new Vector2(randomX, randomY);
    }

    public override void _Process(double delta)
    {
        Position += new Vector2(directionX, 1) * speed * (float)delta;
        RotationDegrees += rotationSpeed * (float)delta;
    }
    public void OnBodyEntered(Node2D body)
    {
        if(canCollide)
        {
            EmitSignal(SignalName.MeteorHitCollision, body);
        }
    }

    public void RandomizeSprite()
    {
        if (sprites == null || sprites.Length == 0)
        {
            return;
        }

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();
        int index = rng.RandiRange(0, sprites.Length - 1);
        sprite.Texture = sprites[index];
    }

    public async void OnAreaEntered(Area2D area)
    {
        area.CallDeferred(Node.MethodName.QueueFree);
        ExplosionSound.Play();
        sprite.Hide();
        canCollide = false;
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        QueueFree();
    }
}
