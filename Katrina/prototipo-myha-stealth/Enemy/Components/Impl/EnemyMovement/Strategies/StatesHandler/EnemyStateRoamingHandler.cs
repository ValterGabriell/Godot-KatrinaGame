using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Interfaces;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Utilidades;
using System;


namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler
{
    public class EnemyStateRoamingHandler : IEnemyStateHandler
    {
        [Signal] public delegate void ChasePlayerEventHandler();

        private Random InRandom = new Random();
        private float InWaitTime;
        private float InMaxWaitTime;

        public EnemyStateRoamingHandler(float inWaitTime, float inMaxWaitTime)
        {
            GDLogger.PrintDebug("Entering Roaming State");
            InWaitTime = inWaitTime;
            InMaxWaitTime = inMaxWaitTime;
        }

        public float ExecuteState(
            double delta,
            EnemyBase InEnemy, Vector2? InTargetPosition = null)
        {
            float distanceToTarget = InEnemy.GlobalPosition.DistanceTo(InTargetPosition.Value);

            if (distanceToTarget < 20f) // Chegou perto do target
            {
                // Para e espera um pouco
                InEnemy.SetState(EnemyState.Waiting);
                InWaitTime = InRandom.Next(1, (int)InMaxWaitTime);
                InEnemy.Velocity = new Vector2(0, InEnemy.Velocity.Y);
            }
            else
            {
                Vector2 direction = (InTargetPosition.Value - InEnemy.GlobalPosition).Normalized();

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
