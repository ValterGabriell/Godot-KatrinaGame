using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Interfaces
{
    public interface IEnemyStateHandler
    {
        float ExecuteState(double delta,EnemyBase InEnemy, Vector2? InTargetPosition = null);
    }
}
