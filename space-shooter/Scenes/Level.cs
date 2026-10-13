using Godot;
using System;

public partial class Level : Node2D
{
    private PackedScene meteorScene = GD.Load<PackedScene>("res://Scenes/meteor.tscn");
    private PackedScene laserScene = GD.Load<PackedScene>("res://Scenes/laser.tscn");
    private Node2D Meteors;
    private Node2D Lasers;
    private Node2D Stars;
    private Player Player; 
    private int health = 3;

    public override void _Ready()
    {
        GetTree().CallGroup("ui", "set_health", health);

        Meteors = GetNode<Node2D>("Meteors");
        Lasers = GetNode<Node2D>("Lasers");
        Stars = GetNodeOrNull<Node2D>("Stars");
        Player = GetNode<Player>("Player");

        float sizeX = GetViewport().GetVisibleRect().Size.X;
        float sizeY = GetViewport().GetVisibleRect().Size.Y;
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();


        float randomScale = rng.RandfRange(1f, 2f);
        foreach (AnimatedSprite2D star in Stars.GetChildren())
        {
            star.Position = new Vector2(rng.Randf() * sizeX, rng.Randf() * sizeY);
            star.Scale = new Vector2(randomScale, randomScale);
            star.SpeedScale = rng.RandfRange(0.6f, 1.4f);
        }

        
        
    }

    public void OnMeteorTimerTimeout()
    {
        Meteor meteor = meteorScene.Instantiate<Meteor>();
        Meteors.AddChild(meteor);
        meteor.Connect(Meteor.SignalName.MeteorHitCollision, new Callable(this, "OnMeteorHitCollision"));
    }

    public void OnMeteorHitCollision(Node2D body)
    {
        health -= 1;
        GetTree().CallGroup("ui", "set_health", health);
        Player.PlayCollisionSound();
        if (health <= 0)
        {
            GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
        }

    }

    public void OnPlayerLaser(Vector2 pos)
    {
        Node2D laser = laserScene.Instantiate<Node2D>();
        Lasers.AddChild(laser);
        laser.Position = pos;
    }
}
