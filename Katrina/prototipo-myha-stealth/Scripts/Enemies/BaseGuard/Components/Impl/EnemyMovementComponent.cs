
using Godot;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl.EnemyMovement.Strategies.Interfaces;
using PrototipoMyha.Utilidades;
using System;

namespace PrototipoMyha.Enemy.Components.Impl
{
    public partial class EnemyMovementComponent : Node, IEnemyBaseComponents
    {
        private EnemyBase _Enemy;
        private Random _random = new Random();
        private Vector2 _targetPosition;
        private float _waitTimer = 0f;
        private float _maxWaitTime = 3f;
        private float _patrolRadius = 200f; // Raio de patrulhamento
        private Vector2 _initialPosition;
        private EnemyState? LastEnemyState = null;
        private IEnemyStateHandler enemyStateHandler;
        
        // Novos campos para limites da plataforma
        private Vector2 _platformMinBounds;
        private Vector2 _platformMaxBounds;
        private bool _platformBoundsDetected = false;
        private float _edgeDetectionDistance = 32f; // Distância para detectar bordas

        public void Initialize(EnemyBase enemyBase)
        {
            this._Enemy = enemyBase;
            this._initialPosition = enemyBase.GlobalPosition;
            DetectPlatformBounds();
            SetNewRandomTarget();
        }

        public void PhysicsProcess(double delta) { }

        public void Process(double delta)
        {
            if (HasEnemyStateChanged())
            {
                LastEnemyState = this._Enemy.CurrentEnemyState;
                enemyStateHandler = GetEnemyStateHandler.GetStateHandler(
                    state: _Enemy.CurrentEnemyState,
                    WaitTime: _waitTimer,
                    MaxWaitTime: _maxWaitTime,
                    SetNewWaitTimeWhenWaiting: SetNewRandomTarget
                    );
            }

            if (enemyStateHandler != null)
            {
                if (this._Enemy.CurrentEnemyState == EnemyState.Chasing)
                {
                    Vector2 playerPosition = PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition();
                    _targetPosition = ClampPositionToPlatform(playerPosition);
                }

                _waitTimer = enemyStateHandler.ExecuteState(
                        delta: delta,
                        InEnemy: _Enemy,
                        InTargetPosition: _targetPosition);
            }
        }

        private bool HasEnemyStateChanged()
        {
            return LastEnemyState != this._Enemy.CurrentEnemyState || LastEnemyState == null;
        }

        private void SetNewRandomTarget()
        {
            IPatrolTypeHandler patrolStrategyHandler = GetPatrolType.GetHandler(this._Enemy.EnemyResource.PatrolStyle);
            Vector2 randomOffset = patrolStrategyHandler.GetPatrolTarget(_patrolRadius, _random);
            Vector2 potentialTarget = _initialPosition + randomOffset;
            
            // Limita o alvo à plataforma
            _targetPosition = ClampPositionToPlatform(potentialTarget);
        }

        /// <summary>
        /// Detecta os limites da plataforma usando raycasting
        /// </summary>
        private void DetectPlatformBounds()
        {
            var spaceState = _Enemy.GetWorld2D().DirectSpaceState;
            
            // Detecta limite esquerdo
            float leftBound = DetectEdge(spaceState, Vector2.Left);
            
            // Detecta limite direito  
            float rightBound = DetectEdge(spaceState, Vector2.Right);
            
            // Define os limites da plataforma
            _platformMinBounds = new Vector2(leftBound, _initialPosition.Y);
            _platformMaxBounds = new Vector2(rightBound, _initialPosition.Y);
            _platformBoundsDetected = true;
            
            GD.Print($"Platform bounds detected: Left={leftBound}, Right={rightBound}");
        }

        /// <summary>
        /// Detecta a borda da plataforma em uma direção específica
        /// </summary>
        private float DetectEdge(PhysicsDirectSpaceState2D spaceState, Vector2 direction)
        {
            Vector2 currentPos = _initialPosition;
            float maxDistance = _patrolRadius * 2; // Distância máxima para procurar
            float stepSize = 16f; // Tamanho do passo para verificação
            
            for (float distance = 0; distance < maxDistance; distance += stepSize)
            {
                Vector2 checkPos = currentPos + (direction * distance);
                
                // Raycast para baixo para verificar se há chão
                var query = PhysicsRayQueryParameters2D.Create(
                    checkPos,
                    checkPos + Vector2.Down * _edgeDetectionDistance
                );
                
                var result = spaceState.IntersectRay(query);
                
                // Se não há colisão, encontramos a borda
                if (result.Count == 0)
                {
                    return checkPos.X - (direction.X * stepSize); // Retorna a última posição segura
                }
            }
            
            // Se não encontrou borda, retorna a posição máxima testada
            return currentPos.X + (direction.X * maxDistance);
        }

        /// <summary>
        /// Limita uma posição aos limites da plataforma
        /// </summary>
        private Vector2 ClampPositionToPlatform(Vector2 position)
        {
            if (!_platformBoundsDetected)
                return position;

            Vector2 clampedPosition = position;
            
            // Limita horizontalmente
            clampedPosition.X = Mathf.Clamp(
                clampedPosition.X, 
                _platformMinBounds.X + _edgeDetectionDistance, 
                _platformMaxBounds.X - _edgeDetectionDistance
            );
            
            return clampedPosition;
        }

        /// <summary>
        /// Verifica se uma posição está dentro dos limites da plataforma
        /// </summary>
        public bool IsPositionWithinPlatform(Vector2 position)
        {
            if (!_platformBoundsDetected)
                return true;

            return position.X >= _platformMinBounds.X && position.X <= _platformMaxBounds.X;
        }

        /// <summary>
        /// Método para redefinir os limites da plataforma (útil se o inimigo mudar de plataforma)
        /// </summary>
        public void RecalculatePlatformBounds()
        {
            _initialPosition = _Enemy.GlobalPosition;
            DetectPlatformBounds();
        }
    }
}