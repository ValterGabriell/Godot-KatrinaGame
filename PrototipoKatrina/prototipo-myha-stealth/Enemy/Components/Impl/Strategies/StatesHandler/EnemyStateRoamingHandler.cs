using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Utilidades;
using System;


namespace PrototipoMyha.Enemy.Components.Impl.Strategies.StatesHandler
{
    public class EnemyStateRoamingHandler : IEnemyStateHandler
    {
        [Signal] public delegate void ChasePlayerEventHandler();

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

            if (distanceToTarget < 20f) // Chegou perto do target
            {
                // Para e espera um pouco
                InEnemy.SetState(EnemyState.Waiting);
                InWaitTime = InRandom.Next(1, (int)InMaxWaitTime);
                InEnemy.Velocity = new Vector2(0, InEnemy.Velocity.Y);
            }
            else
            {
                Vector2 direction = (InTargetPosition - InEnemy.GlobalPosition).Normalized();

                if (InEnemy.RayCast2DDetection != null)
                {
                    float raycastDirection = direction.X > 0 ? 1 : -1;
                    RaycastUtils.FlipRaycast(raycastDirection, [InEnemy.RayCast2DDetection]);
                }

                float horizontalVelocity = direction.X * InEnemy.EnemyResource.MoveSpeed * (float)delta;
                InEnemy.Velocity = new Vector2(horizontalVelocity, InEnemy.Velocity.Y);
            }

            DetectAndChasePlayer(InEnemy);

            return InWaitTime;
        }

        private static void DetectAndChasePlayer(EnemyBase InEnemy)
        {
            (BasePlayer playerDetected, bool isColliding) = RaycastUtils.IsColliding<BasePlayer>(InEnemy.RayCast2DDetection);
            if (isColliding 
                && playerDetected is MyhaPlayer myha 
                && myha.CurrentPlayerState != Player.StateManager.PlayerState.HIDDEN)
            {
                InEnemy.SetState(EnemyState.Chasing);
                InEnemy.TimerToChase.Start();
            }
        }
    }
}
