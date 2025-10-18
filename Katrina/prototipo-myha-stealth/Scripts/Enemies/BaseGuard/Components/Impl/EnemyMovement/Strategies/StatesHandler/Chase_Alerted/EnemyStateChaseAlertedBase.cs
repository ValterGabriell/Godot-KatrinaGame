using Godot;
using KatrinaGame.Core;
using PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl.EnemyMovement.Strategies.Interfaces;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted
{
    public class EnemyStateChaseAlertedBase : IEnemyStateHandler
    {
        private Vector2 InTargetMovement;
        private bool IsInvestigatingArea = false;

        public EnemyStateChaseAlertedBase(Vector2 inTargetMovement)
        {
            InTargetMovement = inTargetMovement;
        }

        public virtual float ExecuteState(
           double delta,
           EnemyBase InEnemy,
           Vector2? InPositionToChase = null)
        {
            if (InPositionToChase.HasValue)
                this.InTargetMovement = InPositionToChase.Value;
            return HandleChaseMovement(InEnemy);
        }

 
        private float HandleChaseMovement(EnemyBase InEnemy)
        {
            Vector2 directionToPlayer = (InTargetMovement - InEnemy.GlobalPosition).Normalized();
            float horizontalVelocity = directionToPlayer.X * InEnemy.EnemyResource.ChaseSpeed;
            InEnemy.Velocity = new Vector2(horizontalVelocity, InEnemy.Velocity.Y);

            float distanceToTarget = InEnemy.GlobalPosition.DistanceTo(InTargetMovement);

            if (InEnemy.RayCast2DDetection != null)
            {
                (BasePlayer _, bool isColliding) = RaycastUtils.IsColliding<BasePlayer>(InEnemy.RayCast2DDetection);

                float raycastDirection = directionToPlayer.X > 0 ? 1 : -1;
                RaycastUtils.FlipRaycast(raycastDirection, [InEnemy.RayCast2DDetection]);


                if (isColliding)
                    InEnemy.TimerToChase.Start();


                if (InEnemy.CurrentEnemyState == States.EnemyState.Alerted 
                    && distanceToTarget < 20f && !IsInvestigatingArea)
                {
                    IsInvestigatingArea = true;
                    InEnemy.TimerToStayAlert.Start();
                }
 
            }

            return 3;
        }

    }
}
