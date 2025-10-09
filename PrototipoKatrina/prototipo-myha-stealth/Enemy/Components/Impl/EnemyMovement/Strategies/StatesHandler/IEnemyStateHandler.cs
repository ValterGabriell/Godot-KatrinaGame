using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler
{
    public interface IEnemyStateHandler
    {
        float ExecuteState(
    double delta,
    EnemyBase InEnemy,
    Vector2 InTargetPosition,
    Random InRandom,
    float InWaitTime,
    float InMaxWaitTime,
  Action SetNewRandomTarget);
    }
}
