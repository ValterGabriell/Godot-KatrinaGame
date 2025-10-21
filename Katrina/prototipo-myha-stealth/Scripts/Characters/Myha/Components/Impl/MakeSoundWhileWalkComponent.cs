using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Utilidades;
using System;

namespace PrototipoMyha.Player.Components.Impl
{
    public interface IMakeSoundWhileWalkComponent : IPlayerBaseComponent
    {
    }
    public partial class MakeSoundWhileWalkComponent : Node, IMakeSoundWhileWalkComponent
    {
        private MyhaPlayer MyhaPlayer;
        private SignalManager SignalManager = SignalManager.Instance;

        public MakeSoundWhileWalkComponent(MyhaPlayer BasePlayer)
        {
            this.MyhaPlayer = BasePlayer;
        }

        public void HandleInput(double delta) {}

        public void Initialize(BasePlayer player)
        {
            this.MyhaPlayer.SoundAreaWalkingComponent.BodyEntered += OnBodyEntered;
            this.SignalManager.PlayerIsMoving += OnMyhaIsMoving;
            this.SignalManager.PlayerStoped += OnMyhaStoped;
        }

        private void OnMyhaStoped()
        {
            this.MyhaPlayer.AlterRadiusCollisionSoundArea(0);
        }

        private void OnMyhaIsMoving(float NoiseValue)
        {
            this.MyhaPlayer.AlterRadiusCollisionSoundArea(NoiseValue);
        }

        private void OnBodyEntered(Node2D area)
        {
            if (area is EnemyBase enemy)
            {
                enemy.SetState(Enemy.States.EnemyState.Alerted);
            }
        }



        public void Process(double delta){ }

        public void PhysicsProcess(double delta){}
    }
}
