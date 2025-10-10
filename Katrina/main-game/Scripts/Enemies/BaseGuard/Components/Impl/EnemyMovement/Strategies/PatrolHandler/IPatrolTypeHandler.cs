

using Godot;
using System;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler
{
    public interface IPatrolTypeHandler
    {
        Vector2 GetPatrolTarget(float InPatrolRadius, Random InRandomInstance);
    }
}
