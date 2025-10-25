using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl.EnemyMovement.Strategies.Interfaces;
using PrototipoMyha.Scripts.Utils;
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
            InWaitTime = inWaitTime;
            InMaxWaitTime = inMaxWaitTime;
           
        }

        public float ExecuteState(
            double delta,
            EnemyBase InEnemy, Vector2? InTargetPosition = null)
        {
            float distanceToTarget = InEnemy.GlobalPosition.DistanceTo(InTargetPosition.Value);
            
            if (distanceToTarget < 5f) // Chegou perto do target
            {

                // Para e espera um pouco
                InWaitTime = InRandom.Next(1, (int)InMaxWaitTime);
                InEnemy.Velocity = new Vector2(0, InEnemy.Velocity.Y);
                InEnemy.SetState(EnemyState.Waiting);
            }
            else
            {
                Vector2 direction = (InTargetPosition.Value - InEnemy.GlobalPosition).Normalized();
                FlipEnemyDirection(InEnemy, direction);

                float horizontalVelocity = direction.X * InEnemy.EnemyResource.MoveSpeed * (float)delta;
                InEnemy.Velocity = new Vector2(horizontalVelocity, InEnemy.Velocity.Y);
            }

            if(InEnemy.CurrentEnemyState != EnemyState.Chasing)
                DetectAndChasePlayer(InEnemy);

            return InWaitTime;
        }

        private static void FlipEnemyDirection(EnemyBase InEnemy, Vector2 direction)
        {
            int directionSign = direction.X > 0 ? 1 : -1;

            RaycastUtils.FlipRaycast(directionSign, [InEnemy.RayCast2DDetection]);
            SpriteUtils.FlipSprite(directionSign, InEnemy.AnimatedSprite2DEnemy);
        }

        private static void DetectAndChasePlayer(EnemyBase InEnemy)
        {
            (BasePlayer playerDetected, bool isColliding) = RaycastUtils.IsColliding<BasePlayer>(InEnemy.RayCast2DDetection);

            if (isColliding 
                && playerDetected is MyhaPlayer myha 
                && myha.CurrentPlayerState != Player.StateManager.PlayerState.HIDDEN)
            {
     
                InEnemy.SetState(EnemyState.Chasing);
        
            }
        }
    }
}
