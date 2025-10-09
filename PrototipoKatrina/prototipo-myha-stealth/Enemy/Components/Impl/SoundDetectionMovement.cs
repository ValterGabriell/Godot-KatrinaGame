using Godot;
using PrototipoMyha.Enemy.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl
{
    public partial class SoundDetectionMovementComponent : Node, IEnemyBaseComponents
    {
        private Area2D _SoundDetectionArea;

        public SoundDetectionMovementComponent(Area2D soundDetectionArea)
        {
            _SoundDetectionArea = soundDetectionArea;
        }

        public void Initialize(EnemyBase enemyBase) 
        {
            GD.Print("SoundDetectionMovementComponent initialized.");
            if(_SoundDetectionArea != null)
                this._SoundDetectionArea.AreaEntered += OnAreaEntered;
        }

        private void OnAreaEntered(Area2D area)
        {
            GD.Print("Sound detected by enemy!");
        }

        public void PhysicsProcess(double delta) { }

        public void Process(double delta)
        {
           
        }
    }
}
