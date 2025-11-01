using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using System;

public partial class PlayerManager : Node
{
    private Vector2 CurrentPlayerPosition;

    private static PlayerManager PlayerGlobalInstance = null;

    public BasePlayer BasePlayer { get; private set; } = null;
    public static float RunNoiseRadius { get; private set; } = 150f;
    public static float SneakNoiseRadius { get; private set; } = 50f;
    public static float JumpNoiseRadius { get; private set; } = 50f;
    public override void _Ready()
    {
        var playerInTree = GetTree().GetNodesInGroup("player");
        BasePlayer = playerInTree.Count > 0
            ? playerInTree[0] as BasePlayer
            : null;

        CurrentPlayerPosition = playerInTree.Count > 0
            ? BasePlayer.GlobalPosition
            : Vector2.Zero;



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
