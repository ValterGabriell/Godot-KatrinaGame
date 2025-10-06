using System;
using Godot;

namespace PrototipoMyha.Enemy;

public partial class EnemyBase : CharacterBody2D
{
    [Export] protected EnemyResources EnemyResource;

    protected Vector2 CurrentPlayerPositionToChase = Vector2.Zero;

    protected enum State
    {
        Idle,
        Roaming,
        Attack,
        Chase,
        Dead,
        LookingForPlayerInDistractedArea
    }
    
    protected State CurrentState = State.Roaming;


}
