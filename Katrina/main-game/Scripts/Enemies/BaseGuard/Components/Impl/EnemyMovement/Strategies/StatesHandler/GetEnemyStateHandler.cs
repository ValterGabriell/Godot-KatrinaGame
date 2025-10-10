using Godot;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Interfaces;
using PrototipoMyha.Enemy.States;
using System;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler
{
    public class GetEnemyStateHandler
    {
        public static IEnemyStateHandler GetStateHandler(
            EnemyState state,
            float WaitTime,
            float MaxWaitTime,
            Action SetNewWaitTimeWhenWaiting
            )
        {
            return state switch
            {
                EnemyState.Roaming => new EnemyStateRoamingHandler(WaitTime, MaxWaitTime),
                EnemyState.Waiting => new EnemyStateWaitingHandler(WaitTime, SetNewWaitTimeWhenWaiting),
                EnemyState.Chasing => new EnemyStateChasingHandler(Vector2.Zero),
                EnemyState.Alerted => new EnemyStateAlertedHandler(PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition()),
                _ => throw new NotImplementedException($"State handler for {state} is not implemented."),
            };
        }

    }
}
