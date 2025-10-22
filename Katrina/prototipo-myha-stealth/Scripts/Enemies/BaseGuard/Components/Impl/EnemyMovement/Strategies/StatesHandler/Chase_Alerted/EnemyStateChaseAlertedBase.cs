using Godot;
using KatrinaGame.Core;
using PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl.EnemyMovement.Strategies.Interfaces;
using PrototipoMyha.Scripts.Utils;
using PrototipoMyha.Utilidades;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted
{
    public class EnemyStateChaseAlertedBase : IEnemyStateHandler
    {
        private Vector2 InTargetMovement;
        private bool IsInvestigatingArea = false;
        /// <summary>
        /// foi criado porque havia um bug que o raycast ficava doido quando o inimigo chegava proximo do local, mudando
        /// rapidamente de 1 pra -1
        /// </summary>
        private float RaycastDirectionWhenStartToAlert = 0f;

        // Threshold para considerar se estão no mesmo nível vertical
        private const float VerticalLevelThreshold = 30f;
        private const int TIME_TO_LOOK_UP_DOWN = 300;
        private int controlTimeToLookUpDown = 0;

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

            // Calcula apenas a distância horizontal (eixo X)
            float horizontalDistanceToTarget = Mathf.Abs(InTargetMovement.X - InEnemy.GlobalPosition.X);

            // Verifica se o jogador não está no mesmo nível vertical
            bool isPlayerAtDifferentLevel = IsPlayerAtDifferentVerticalLevel(InEnemy);

            // Se o jogador estiver em nível diferente, pare o movimento horizontal
            float horizontalVelocity = 0f;
            if (!isPlayerAtDifferentLevel)
            {
                horizontalVelocity = directionToPlayer.X * InEnemy.EnemyResource.ChaseSpeed;
            }

            InEnemy.Velocity = new Vector2(horizontalVelocity, InEnemy.Velocity.Y);


            if (InEnemy.RayCast2DDetection != null)
            {
                (BasePlayer _, bool isColliding) = RaycastUtils.IsColliding<BasePlayer>(InEnemy.RayCast2DDetection);

                float direction = directionToPlayer.X > 0 ? 1 : -1;

                ///por padrao true
                if (IsRaycastDirectionNotInitialized())
                {
                    FlipEnemyDirection(InEnemy, directionToPlayer);
                }

                if (isColliding)
                    InEnemy.TimerToChase.Start();

                // Ativa animação específica quando o jogador está em nível diferente
                if (isPlayerAtDifferentLevel)
                {
                    ActivateLookUpDownAnimation(InEnemy);
                }

                if (InEnemy.CurrentEnemyState == States.EnemyState.Alerted
                    && horizontalDistanceToTarget < 20f && !IsInvestigatingArea && !isPlayerAtDifferentLevel)
                {
                    ToggleRaycastDirectionOnAlert(direction);
                    InEnemy.SetState(States.EnemyState.Waiting);
                }
            }

            return 3;
        }

        private static void FlipEnemyDirection(EnemyBase InEnemy, Vector2 direction)
        {
            int directionSign = direction.X > 0 ? 1 : -1;

            RaycastUtils.FlipRaycast(directionSign, [InEnemy.Raycast2DBounderie, InEnemy.RayCast2DDetection]);
            SpriteUtils.FlipSprite(directionSign, InEnemy.AnimatedSprite2DEnemy);
        }


        /// <summary>
        /// Verifica se o jogador está em um nível vertical diferente do inimigo
        /// </summary>
        /// <param name="enemy">O inimigo para comparar posição</param>
        /// <returns>True se o jogador estiver em nível diferente, False caso contrário</returns>
        private bool IsPlayerAtDifferentVerticalLevel(EnemyBase enemy)
        {
            float verticalDistance = Mathf.Abs(InTargetMovement.Y - enemy.GlobalPosition.Y);
            return verticalDistance > VerticalLevelThreshold;
        }

        /// <summary>
        /// Ativa a animação específica quando o jogador está em nível vertical diferente
        /// </summary>
        /// <param name="enemy">O inimigo que deve executar a animação</param>
        private void ActivateLookUpDownAnimation(EnemyBase enemy)
        {
            controlTimeToLookUpDown++;
            // Determina se o jogador está acima ou abaixo
            bool isPlayerAbove = InTargetMovement.Y < enemy.GlobalPosition.Y;
      
            // Exemplo de como ativar animação específica
            if (isPlayerAbove)
            {
                // Animação olhando para cima
                GDLogger.PrintDebug("Player is above - Enemy stopped and looking up");
            }
            else
            {
                // Animação olhando para baixo
                GDLogger.PrintDebug("Player is below - Enemy stopped and looking down");
            }

            if (controlTimeToLookUpDown > TIME_TO_LOOK_UP_DOWN)
            {
                enemy.SetState(States.EnemyState.Waiting);
                controlTimeToLookUpDown = 0;
            }
              
        }

        private bool IsRaycastDirectionNotInitialized()
        {
            return RaycastDirectionWhenStartToAlert == 0;
        }

        private void ToggleRaycastDirectionOnAlert(float raycastDirection)
        {
            if (RaycastDirectionWhenStartToAlert == 0)
                RaycastDirectionWhenStartToAlert = raycastDirection;
            else
                RaycastDirectionWhenStartToAlert = 0f;
        }
    }
}