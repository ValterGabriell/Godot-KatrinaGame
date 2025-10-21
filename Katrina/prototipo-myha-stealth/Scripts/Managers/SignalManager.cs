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
    [Signal] public delegate void PlayerIsMovingEventHandler(float NoiseValue);
    [Signal] public delegate void PlayerStopedEventHandler();
    [Signal] public delegate void FlipObjectEventHandler(int direction);


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
