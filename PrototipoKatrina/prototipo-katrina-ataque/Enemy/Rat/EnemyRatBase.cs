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

    [Export] private RayCast2D _RaycastDetectPlayer;
    [Export] private Timer TimerToChasePlayer;
    [Export] private Area2D RangeAttackArea;

    private CollisionShape2D _RangeAttackAreaCollision;

    private PlayerGlobal _PlayerGlobal;
    private Sprite2D _Sprite;



    override public void _Ready()
    {
        _PlayerGlobal = PlayerGlobal.GetPlayerGlobalInstance();
        this._RangeAttackAreaCollision = RangeAttackArea.GetNode<CollisionShape2D>("CollisionShape2D");
        this._Sprite = GetNode<Sprite2D>("Sprite2D");
        SignalManager.Instance.EnemyEnteredWarningArea += OnEnemyEnteredWarningArea;
    }

    protected Vector2 GetMoveDirection() => MoveDirection;
    protected void SetMoveDirection(Vector2 direction) => MoveDirection = direction;

}
