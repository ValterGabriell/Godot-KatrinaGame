using Godot;
using PrototipoMyha.Enemy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.Strategies.StatesHandler
{
    public class EnemyStateWaitingHandler : IEnemyStateHandler
    {
        public float ExecuteState(
          double delta,
          EnemyBase InEnemy,
          Vector2 InTargetPosition,
          Random InRandom,
          float InWaitTime,
          float InMaxWaitTime,
          Action SetNewRandomTarget)
        {
            GD.Print("Waiting... Time left: " + InWaitTime);
            InWaitTime -= (float)delta;
            InEnemy.Velocity = Vector2.Zero;

            if (InWaitTime <= 0f)
            {
                SetNewRandomTarget();
                InEnemy.SetState(EnemyState.Roaming);
            }
            return InWaitTime;
        }
    }
}
