using Godot;
using PrototipoKatrina;
using System;

public partial class PlayerGlobal : Node
{
    private Vector2 CurrentPlayerPosition = Vector2.Zero;

    private static PlayerGlobal PlayerGlobalInstance = null;

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

    public static PlayerGlobal GetPlayerGlobalInstance()
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
