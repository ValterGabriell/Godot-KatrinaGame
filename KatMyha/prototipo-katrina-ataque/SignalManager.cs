using System;
using Godot;

namespace PrototipoKatrina;

public partial class SignalManager : Node
{
    [Signal] public delegate void EnemyEnteredWarningAreaEventHandler(Vector2 InPositionToGo);
    [Signal] public delegate void EnemyRatStopLookForPlayerEventHandler(Vector2 InPositionToGo);
   
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
