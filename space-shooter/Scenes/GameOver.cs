using Godot;
using System;

public partial class GameOver : Control
{
	private AudioStreamPlayer2D GameOverSound;
	public override void _Ready()
	{
		Label scoreLabel = GetNode<Label>("CenterContainer/VBoxContainer/Label2");
		scoreLabel.Text = scoreLabel.Text + Global.Score.ToString();
		GameOverSound = GetNode<AudioStreamPlayer2D>("GameOverSound");
		GameOverSound.Play();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("restart"))
		{
			GetTree().ChangeSceneToFile("res://Scenes/level.tscn");
		}
	}
}
