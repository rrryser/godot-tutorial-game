using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int speed = 500;
	[Signal]
	public delegate void LaserEventHandler(Vector2 pos);

	private Marker2D LaserStartPosition;
	private Timer Cooldown; 
	private bool canShoot = true;
	private AudioStreamPlayer2D LaserSound; 
	private AudioStreamPlayer2D DamageSound; 
	
	// Called when the node enters the scene tree for the first time. 
	public override void _Ready()
	{
		Position = new Vector2(100, 500);
		LaserStartPosition = GetNode<Marker2D>("LaserStartPosition");
		Cooldown = GetNode<Timer>("Cooldown");
		LaserSound = GetNode<AudioStreamPlayer2D>("LaserSound");
		DamageSound = GetNode<AudioStreamPlayer2D>("DamageSound");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var direction = Input.GetVector("left", "right", "up", "down");
		Velocity = direction * speed;
		MoveAndSlide();

		// shoot input
		if (Input.IsActionJustPressed("shoot") && canShoot)
		{
			EmitSignal(SignalName.Laser, LaserStartPosition.GlobalPosition);
			LaserSound.Play();
			canShoot = false;
			Cooldown.Start();
		}
	}

	public void OnCooldownTimeout()
	{
		canShoot = true;
	}

	public void PlayCollisionSound()
	{
		DamageSound.Play();
	}

}
