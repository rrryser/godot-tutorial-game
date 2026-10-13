using Godot;
using System;

public partial class Laser : Area2D
{
    public int speed = 1000;
    public override void _Ready()
    {
        Sprite2D sprite = GetNode<Sprite2D>("LaserImage");
        Tween tween = CreateTween();
        tween.TweenProperty(sprite, "scale", new Vector2(1, 1), 0.1).From(new Vector2(0,0));
    }
    public override void _Process(double delta)
    {
        Position += new Vector2(0, -(float)(speed * delta));
    }
}
