using Godot;
using PrototipoKatrina.Enemy;
using System;
using System.Collections.Generic;

namespace PrototipoKatrina.Enemy;

public partial class EnemyRatBase : EnemyBase
{
    [Export] protected Timer DirectionTimer;

    /// <summary>
    /// Direção do movimento do rato, usado para patrulhar o cenário, ou seja, andar para frente e para trás.
    /// Essa direcao é alterada a cada tempo determinado pelo DirectionTimer.
    /// Ela vai apenas alterar o eixo X, ou seja, andar para a esquerda ou para a direita.
    /// </summary>
    private Vector2 MoveDirection = Vector2.Zero;

    [Export] private RayCast2D RaycastDetectPlayer;
    [Export] private Timer TimerToChasePlayer;

    private PlayerGlobal playerGlobal;


    override public void _Ready()
    {
        playerGlobal = PlayerGlobal.GetPlayerGlobalInstance();
        GD.Print("Enemy Rat Ready");
    }

    protected Vector2 GetMoveDirection() => MoveDirection;
    protected void SetMoveDirection(Vector2 direction) => MoveDirection = direction;

}
