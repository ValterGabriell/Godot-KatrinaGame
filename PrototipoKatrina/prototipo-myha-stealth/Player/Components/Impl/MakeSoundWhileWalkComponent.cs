using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
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
    public class MakeSoundWhileWalkComponent : IMakeSoundWhileWalkComponent
    {
        private BasePlayer BasePlayer;
        private IMovementComponent MovementComponent;

        public MakeSoundWhileWalkComponent(BasePlayer BasePlayer)
        {
            this.BasePlayer = BasePlayer;
        }

        public void HandleInput(double delta)
        {
            
        }

        public void Initialize(BasePlayer player)
        {
            this.MovementComponent = this.BasePlayer.GetComponent<IMovementComponent>();
        }

        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {
            GD.Print(this.MovementComponent.IsPlayerWalking);
        }
    }
}
