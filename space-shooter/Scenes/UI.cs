using Godot;
using System;

public partial class UI : CanvasLayer
{
    private static Texture2D playerLifeTexture = (Texture2D)GD.Load("res://SpaceShooterRedux/PNG/UI/playerLife1_red.png");
    private int timeElapsed = 0;
    private void set_health(int amount)
    {
        // Remove all children
        HBoxContainer hBoxContainer = GetNode<HBoxContainer>("MarginContainer2/HBoxContainer");
        for (int child = hBoxContainer.GetChildCount() - 1; child >= 0; child--)
        {
            hBoxContainer.GetChild(child).QueueFree();
        }
        // create new children amount is set by health
        for (int i = 0; i < amount; i++)
        {
            TextureRect textureRect = new TextureRect();
            textureRect.Texture = playerLifeTexture;
            hBoxContainer.AddChild(textureRect);
            textureRect.StretchMode = TextureRect.StretchModeEnum.KeepAspect;
        }
    }

    private void OnScoreTimerTimeout()
    {
        timeElapsed += 1;
        Global.Score = timeElapsed;
        Label scoreLabel = GetNode<Label>("MarginContainer/Label");
        scoreLabel.Text = timeElapsed.ToString();
    }
}
