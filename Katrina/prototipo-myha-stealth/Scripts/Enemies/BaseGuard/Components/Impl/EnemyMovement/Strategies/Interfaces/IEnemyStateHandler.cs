using Godot;
using PrototipoMyha.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl.EnemyMovement.Strategies.Interfaces
{
    public interface IEnemyStateHandler
    {
        float ExecuteState(double delta,EnemyBase InEnemy, Vector2? InTargetPosition = null);
    }
}
