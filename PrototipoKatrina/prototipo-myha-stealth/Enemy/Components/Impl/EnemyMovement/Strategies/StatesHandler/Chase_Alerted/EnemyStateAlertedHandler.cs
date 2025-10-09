using Godot;
using PrototipoMyha.Enemy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted
{
    public class EnemyStateAlertedHandler : EnemyStateChaseAlertedBase
    {
        private float alertWaitDuration = 4.0f; 
        public EnemyStateAlertedHandler(Vector2 lastKnownPlayerPosition) : base(lastKnownPlayerPosition)
        {
            GD.Print("EnemyStateAlertedHandler initialized.");
        }

        public override float ExecuteState(
            double delta,
            EnemyBase InEnemy, Vector2? InPositionToChase = null)
        {

            alertWaitDuration -= (float)delta;
            InEnemy.Velocity = Vector2.Zero;
            float waitTime = 3;
            if (alertWaitDuration <= 0f)
            {
                waitTime = base.ExecuteState(delta, InEnemy);
            }
            return waitTime;
        }
    }
}
