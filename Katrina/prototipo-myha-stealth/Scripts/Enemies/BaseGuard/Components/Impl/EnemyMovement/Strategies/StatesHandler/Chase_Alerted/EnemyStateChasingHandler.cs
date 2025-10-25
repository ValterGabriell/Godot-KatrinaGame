using Godot;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted
{
    public class EnemyStateChasingHandler : EnemyStateChaseAlertedBase
    {
        public EnemyStateChasingHandler(Vector2 inTargetMovement) : base(inTargetMovement)
        {

        }

        public override float ExecuteState(double delta, EnemyBase InEnemy, Vector2? InPositionToChase = null)
        {
            InEnemy.TimerToChase.Start();
            return base.ExecuteState(delta, InEnemy, InPositionToChase);
        }
    }
}