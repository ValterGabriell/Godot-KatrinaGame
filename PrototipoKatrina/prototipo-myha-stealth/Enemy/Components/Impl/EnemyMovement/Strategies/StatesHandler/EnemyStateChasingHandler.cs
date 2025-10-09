using Godot;
using KatrinaGame.Core;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Utilidades;
using System;
using static Godot.TextServer;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler
{
    public class EnemyStateChasingHandler : IEnemyStateHandler
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
            
            Vector2 directionToPlayer = (PlayerGlobal.GetPlayerGlobalInstance().GetPlayerPosition() - InEnemy.GlobalPosition).Normalized();
            float horizontalVelocity = directionToPlayer.X * InEnemy.EnemyResource.ChaseSpeed;
            InEnemy.Velocity = new Vector2(horizontalVelocity, InEnemy.Velocity.Y);


            if (InEnemy.RayCast2DDetection != null)
            {
                (BasePlayer playerDetected, bool isColliding) = RaycastUtils.IsColliding<BasePlayer>(InEnemy.RayCast2DDetection);

                float raycastDirection = directionToPlayer.X > 0 ? 1 : -1;
                RaycastUtils.FlipRaycast(raycastDirection, [InEnemy.RayCast2DDetection]);


                if (isColliding)
                    InEnemy.TimerToChase.Start();

            }
                
            return 3;
        }
    }
}