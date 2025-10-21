using Godot;

using System;

public partial class PlayerManager : Node
{
    private Vector2 CurrentPlayerPosition = Vector2.Zero;

    private static PlayerManager PlayerGlobalInstance = null;


    public static float RunNoiseRadius { get; private set; } = 100f;
    public static float JumpNoiseRadius { get; private set; } = 50f;
    public override void _Ready()
    {
        if (PlayerGlobalInstance == null)
        {
            PlayerGlobalInstance = this;
        }
        else
        {
            QueueFree();
        }
    }

    public static PlayerManager GetPlayerGlobalInstance()
    {
        return PlayerGlobalInstance;
    }

    public void UpdatePlayerPosition(Vector2 newPosition)
    {
        CurrentPlayerPosition = newPosition;
    }

    public Vector2 GetPlayerPosition()
    {
        return CurrentPlayerPosition;
    }
}
