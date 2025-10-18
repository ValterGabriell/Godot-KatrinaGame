using Godot;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Interfaces;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Enemy.States;
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
        public void Initialize(EnemyBase enemyBase)
        {
            this._Enemy = enemyBase;
            this._initialPosition = enemyBase.GlobalPosition;
            SetNewRandomTarget();
        }


        public void PhysicsProcess(double delta) { }

        public void Process(double delta)
        {
            
            if (LastEnemyState != this._Enemy.CurrentEnemyState || LastEnemyState == null)
            {
                LastEnemyState = this._Enemy.CurrentEnemyState;
                enemyStateHandler = GetEnemyStateHandler.GetStateHandler(
                    state: _Enemy.CurrentEnemyState,
                    WaitTime: _waitTimer,
                    MaxWaitTime: _maxWaitTime,
                    SetNewWaitTimeWhenWaiting: SetNewRandomTarget
                    );

            }

            if(enemyStateHandler != null)
            {
                if(this._Enemy.CurrentEnemyState == EnemyState.Chasing)
                    _targetPosition = PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition();
                _waitTimer = enemyStateHandler.ExecuteState(
                        delta: delta,
                        InEnemy: _Enemy,
                        InTargetPosition: _targetPosition);
                UpdateSpriteDirection();
            }
           
        }

        // NOVO MÉTODO: Determina e atualiza a direção do sprite
        private void UpdateSpriteDirection()
        {
            // Calcula a direção do movimento comparando posição atual com destino
            Vector2 direction = (_targetPosition - _Enemy.GlobalPosition).Normalized();

            // Se há movimento horizontal significativo
            if (Mathf.Abs(direction.X) > 0.1f)
            {
                // Se direction.X > 0, está indo para a direita; se < 0, para a esquerda
                _Enemy.AnimatedSprite2DEnemy.FlipH = direction.X < 0;
            }
        }


        private void SetNewRandomTarget()
        {
            IPatrolTypeHandler patrolStrategyHandler = GetPatrolType.GetHandler(this._Enemy.EnemyResource.PatrolStyle);
            Vector2 randomOffset = patrolStrategyHandler.GetPatrolTarget(_patrolRadius, _random);
            _targetPosition = _initialPosition + randomOffset;
        }
    }
}
