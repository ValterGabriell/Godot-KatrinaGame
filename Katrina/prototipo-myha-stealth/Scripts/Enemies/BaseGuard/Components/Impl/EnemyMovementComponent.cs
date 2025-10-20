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

        public EnemyMovementComponent(EnemyBase enemy)
        {
            this._Enemy = enemy;
            this._initialPosition = enemy.GlobalPosition;
        }

        public void Initialize()
        {
 
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
                    _targetPosition = PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition();

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
            _targetPosition = _initialPosition + randomOffset;

        }
    }
}