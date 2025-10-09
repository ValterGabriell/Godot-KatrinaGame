using Godot;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler;
using PrototipoMyha.Enemy.Components.Interfaces;
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

        public void Initialize(EnemyBase enemyBase)
        {
            this._Enemy = enemyBase;
            this._initialPosition = enemyBase.GlobalPosition;
            SetNewRandomTarget();
        }


        public void PhysicsProcess(double delta) { }

        public void Process(double delta)
        {

            IEnemyStateHandler enemyStateHandler = GetEnemyStateHandler.GetStateHandler(_Enemy.CurrentEnemyState);

            _waitTimer = enemyStateHandler.ExecuteState(
                delta: delta,
                InEnemy: _Enemy,
                InTargetPosition: _targetPosition,
                InRandom: _random,
                InWaitTime: _waitTimer,
                InMaxWaitTime: _maxWaitTime,
                SetNewRandomTarget: SetNewRandomTarget);
        }
       

        private void SetNewRandomTarget()
        {
            IPatrolTypeHandler patrolStrategyHandler = GetPatrolType.GetHandler(this._Enemy.EnemyResource.PatrolStyle);
            Vector2 randomOffset = patrolStrategyHandler.GetPatrolTarget(_patrolRadius, _random);
            _targetPosition = _initialPosition + randomOffset;
        }
    }
}
