using Godot;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Interfaces;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler
{
    public class EnemyStateWaitingHandler : IEnemyStateHandler
    {
        private float InWaitTime;
        private Action SetNewRandomTarget;

        public EnemyStateWaitingHandler(float inWaitTime, Action setNewRandomTarget)
        {
            InWaitTime = inWaitTime;
            SetNewRandomTarget = setNewRandomTarget;
        }

        public float ExecuteState(
          double delta,
          EnemyBase InEnemy, Vector2? InPositionToChase = null)
        {
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
