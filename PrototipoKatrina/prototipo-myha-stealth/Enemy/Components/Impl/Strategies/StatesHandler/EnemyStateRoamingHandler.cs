using Godot;
using PrototipoMyha.Enemy.States;
using System;


namespace PrototipoMyha.Enemy.Components.Impl.Strategies.StatesHandler
{
    public class EnemyStateRoamingHandler : IEnemyStateHandler
    {

        public float ExecuteState(
            double delta,
            EnemyBase InEnemy,
            Vector2 InTargetPosition,
            Random InRandom,
            float InWaitTime,
            float InMaxWaitTime,
            Action _)
        {
            float distanceToTarget = InEnemy.GlobalPosition.DistanceTo(InTargetPosition);
            //GD.Print("Distance to Target: " + distanceToTarget);
            if (distanceToTarget < 20f) // Chegou perto do target
            {
                // Para e espera um pouco
                InEnemy.SetState(EnemyState.Waiting);
                InWaitTime = InRandom.Next(1, (int)InMaxWaitTime);
                InEnemy.Velocity = Vector2.Zero;
            }
            else
            {
                // Move em direção ao target
                Vector2 direction = (InTargetPosition - InEnemy.GlobalPosition).Normalized();
                InEnemy.Velocity = direction * InEnemy.EnemyResource.MoveSpeed * (float)delta;
            }

            return InWaitTime;
        }

    }
}
