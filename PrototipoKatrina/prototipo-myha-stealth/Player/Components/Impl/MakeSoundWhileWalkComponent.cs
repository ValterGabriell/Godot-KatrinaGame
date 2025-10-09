using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using KatrinaGame.Players;
using PrototipoMyha.Enemy;
using PrototipoMyha.Player.Components.Interfaces;
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
        private IMovementComponent MovementComponent;

        public MakeSoundWhileWalkComponent(MyhaPlayer BasePlayer)
        {
            this.MyhaPlayer = BasePlayer;
        }

        public void HandleInput(double delta)
        {
            
        }

        public void Initialize(BasePlayer player)
        {
            this.MovementComponent = this.MyhaPlayer.GetComponent<IMovementComponent>();
            this.MyhaPlayer.SoundAreaWalkingComponent.BodyEntered += OnBodyEntered;
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
            if (ShouldDisableWalkingSoundArea())
            {
                this.MyhaPlayer.SoundAreaWalkingComponent.Monitoring = false;
                this.MyhaPlayer.SoundAreaWalkingColiisonComponent.Disabled = true;
            }

            if (ShouldEnableWalkingSoundArea())
            {
                this.MyhaPlayer.SoundAreaWalkingComponent.Monitoring = true;
                this.MyhaPlayer.SoundAreaWalkingColiisonComponent.Disabled = false;
            }

        }

        private bool ShouldEnableWalkingSoundArea()
        {
            return this.MovementComponent.IsPlayerWalking
                            && this.MyhaPlayer.SoundAreaWalkingComponent.Monitoring == false
                            && this.MyhaPlayer.SoundAreaWalkingColiisonComponent.Disabled == true;
        }

        private bool ShouldDisableWalkingSoundArea()
        {
            return !this.MovementComponent.IsPlayerWalking
                            && this.MyhaPlayer.SoundAreaWalkingComponent.Monitoring == true
                            && this.MyhaPlayer.SoundAreaWalkingColiisonComponent.Disabled == false;
        }




        public void PhysicsProcess(double delta)
        {

        }
    }
}
