using System;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler
{
    public class GetPatrolType
    {
        public static IPatrolTypeHandler GetHandler(PatrolType IsCirclePatrol)
        {
            return IsCirclePatrol switch
            {
                PatrolType.Circle => new PatrolTypeCircleHandlerImpl(),
                PatrolType.XAxis => new PatrolTypeXAxisHandlerImpl(),
                _ => throw new NotImplementedException($"Patrol type not implemented"),
            };
        }
    }
}
