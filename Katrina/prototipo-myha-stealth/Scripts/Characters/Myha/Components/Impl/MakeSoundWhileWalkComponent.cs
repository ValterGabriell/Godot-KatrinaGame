using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using KatrinaGame.Players;
using PrototipoMyha.Enemy;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void HandleInput(double delta)
        {
            
        }

        public void Initialize(BasePlayer player)
        {
            this.MyhaPlayer.SoundAreaWalkingComponent.BodyEntered += OnBodyEntered;
            this.SignalManager.MyhaIsMoving += OnMyhaIsMoving;
        }

        private void OnMyhaIsMoving(float NoiseValue)
        {
            GDLogger.PrintInfo("MakeSoundWhileWalkComponent - OnMyhaIsMoving - NoiseValue: " + NoiseValue);
        }

        private void OnBodyEntered(Node2D area)
        {
            if (area is EnemyBase enemy)
            {
                enemy.SetState(Enemy.States.EnemyState.Alerted);
            }
        }



        public void Process(double delta)
        {


        }

        public void PhysicsProcess(double delta)
        {

        }
    }
}
