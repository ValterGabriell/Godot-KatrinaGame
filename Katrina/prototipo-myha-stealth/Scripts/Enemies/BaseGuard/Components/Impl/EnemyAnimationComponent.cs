using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl
{
    public partial class EnemyAnimationComponent : Node, IEnemyBaseComponents
    {
        private EnemyBase _Enemy;
        public void Initialize(EnemyBase enemyBase)
        {
            this._Enemy = enemyBase;
        }

        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {
            if(this._Enemy.CurrentEnemyState == Enemy.States.EnemyState.Roaming)
                this._Enemy.AnimatedSprite2DEnemy.Play(EnumGuardMove.roaming.ToString());
            else if(this._Enemy.CurrentEnemyState == Enemy.States.EnemyState.Alerted)
                this._Enemy.AnimatedSprite2DEnemy.Play(EnumGuardMove.start_warning.ToString());
            else if(this._Enemy.CurrentEnemyState == Enemy.States.EnemyState.Waiting)
                this._Enemy.AnimatedSprite2DEnemy.Play(EnumGuardMove.end_warning.ToString());
            else if(this._Enemy.CurrentEnemyState == Enemy.States.EnemyState.Chasing)
                this._Enemy.AnimatedSprite2DEnemy.Play(EnumGuardMove.shoot.ToString());

        }
    }
}
