using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Interfaces;
using System;

namespace PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl
{
    public partial class EnemyAnimationComponent : Node, IEnemyBaseComponents
    {
        private EnemyBase _Enemy;
        private bool IsWalkingToAlert = false;
        private SignalManager SignalManager;
        public void Initialize(EnemyBase enemyBase)
        {
            this._Enemy = enemyBase;
            this.SignalManager = SignalManager.Instance;
            this.SignalManager.EnemySpottedPlayer += OnEnemySpottedPlayer;
        }

  

        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {
         
            if(this._Enemy.CurrentEnemyState != Enemy.States.EnemyState.Alerted)
                IsWalkingToAlert = false;

            var currentAnimation = this._Enemy.CurrentEnemyState switch
            {
                Enemy.States.EnemyState.Roaming => EnumGuardMove.roaming.ToString(),
                Enemy.States.EnemyState.Alerted => EnumGuardMove.start_warning.ToString(),
                Enemy.States.EnemyState.Waiting => EnumGuardMove.end_warning.ToString(),
                Enemy.States.EnemyState.Chasing => EnumGuardMove.shoot.ToString(),
                _ => EnumGuardMove.roaming.ToString(),
            };


            if (!IsWalkingToAlert)
                this._Enemy.AnimatedSprite2DEnemy.Play(currentAnimation);
            else
                this._Enemy.AnimatedSprite2DEnemy.Play(EnumGuardMove.walk_alert.ToString());
        }

        private void OnEnemySpottedPlayer()
        {
            IsWalkingToAlert = true;
        }
    }
}
