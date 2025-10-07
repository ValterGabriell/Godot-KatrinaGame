using PrototipoMyha.Enemy.States;
using System;

namespace PrototipoMyha.Enemy.Components.Impl.Strategies.StatesHandler
{
    public class GetEnemyStateHandler
    {
        public static IEnemyStateHandler GetStateHandler(EnemyState state)
        {
            return state switch
            {
                EnemyState.Roaming => new EnemyStateRoamingHandler(),
                EnemyState.Waiting => new EnemyStateWaitingHandler(),
                EnemyState.Chase => new EnemyStateChasingHandler(),
                _ => throw new NotImplementedException($"State handler for {state} is not implemented."),
            };
        }
    }
}
