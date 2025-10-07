using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Player.StateManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrototipoMyha.Enemy.Components.Impl
{
    public partial class EnemyMovementComponent : Node, IEnemyBaseComponents
    {
        private EnemyBase _Enemy;


        public void Initialize(EnemyBase enemyBase)
        {
            this._Enemy = enemyBase;
        }


        public void PhysicsProcess(double delta)
        {
            this._Enemy.MoveAndSlide();
        }

        public void Process(double delta) { }


    }
}
