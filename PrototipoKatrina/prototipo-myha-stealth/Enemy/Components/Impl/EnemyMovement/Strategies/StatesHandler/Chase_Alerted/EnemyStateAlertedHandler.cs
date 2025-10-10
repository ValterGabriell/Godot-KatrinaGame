using Godot;
using PrototipoMyha.Utilidades;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted
{
    public class EnemyStateAlertedHandler : EnemyStateChaseAlertedBase
    {
        private float alertWaitDuration = 2.0f; 
        public EnemyStateAlertedHandler(Vector2 lastKnownPlayerPosition) : base(lastKnownPlayerPosition)
        {
            GDLogger.PrintDebug("Entering Alerted State");
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
