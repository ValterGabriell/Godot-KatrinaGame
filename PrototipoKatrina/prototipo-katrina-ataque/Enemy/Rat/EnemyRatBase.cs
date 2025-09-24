using Godot;
using PrototipoKatrina.Enemy;
using System;
using System.Collections.Generic;

namespace PrototipoKatrina.Enemy;

public partial class EnemyRatBase : EnemyBase
{
    [Export] protected Timer DirectionTimer;
    protected Vector2 MoveDirection = Vector2.Zero;

    [Export] private RayCast2D RaycastDetectPlayer;
    [Export] private Timer TimerToChasePlayer;

    private PlayerGlobal playerGlobal;
    
    
    override public void _Ready()
    {
        playerGlobal = PlayerGlobal.GetPlayerGlobalInstance();
        GD.Print("Enemy Rat Ready");
    }

}
