using Godot;
using PrototipoMyha.Player.StateManager;
using System;

namespace PrototipoMyha;

public partial class SignalManager : Node
{
    [Signal] public delegate void EnemyEnteredWarningAreaEventHandler(Vector2 InPositionToGo);
    [Signal] public delegate void EnemyRatStopLookForPlayerEventHandler(Vector2 InPositionToGo);
    [Signal] public delegate void PlayerStateChangedEventHandler(PlayerState NewState);
    [Signal] public delegate void EnemySpottedPlayerEventHandler();

    public static SignalManager Instance { get; private set; }


    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            QueueFree();
        }
    }
}
