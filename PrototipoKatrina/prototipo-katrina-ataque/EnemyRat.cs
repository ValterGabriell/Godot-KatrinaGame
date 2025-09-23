using Godot;
using System;
using System.Collections.Generic;

namespace PrototipoKatrina;
public partial class EnemyRat : CharacterBody2D
{
    [Export] private EnemyResources EnemyResource;
    [Export] private Timer DirectionTimer;

    enum State
    {
        Idle,
        Roaming,
        Attack,
        Chase,
        Dead
    }

    private State CurrentState = State.Idle;
    private Vector2 MoveDirection = Vector2.Zero;


}
