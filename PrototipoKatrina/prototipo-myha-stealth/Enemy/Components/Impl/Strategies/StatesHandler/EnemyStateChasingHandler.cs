using PrototipoMyha.Enemy.States;
using System;
using Godot;

namespace PrototipoMyha.Enemy.Components.Impl.Strategies.StatesHandler
{
    public class EnemyStateChasingHandler : IEnemyStateHandler
    {
        public float ExecuteState(
    double delta,
    EnemyBase InEnemy,
    Vector2 InTargetPosition,
    Random InRandom,
    float InWaitTime,
    float InMaxWaitTime,
  Action _)
        {
            Vector2 directionToPlayer = (InEnemy.CurrentPlayerPositionToChase - InEnemy.GlobalPosition).Normalized();
            InEnemy.Velocity = directionToPlayer * InEnemy.EnemyResource.ChaseSpeed * (float)delta;

            return InWaitTime;
        }
    }
}