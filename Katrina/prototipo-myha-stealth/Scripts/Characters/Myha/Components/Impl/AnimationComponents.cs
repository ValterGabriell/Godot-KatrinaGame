using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using KatrinaGame.Scripts.Utils;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Scripts.Characters.Myha.Components.Impl
{
    public interface IAnimationComponents : IPlayerBaseComponent
    {
    }
    public partial class AnimationComponents : Node, IAnimationComponents
    {
        private BasePlayer _player;
        SignalManager SignalManager;

        public AnimationComponents(BasePlayer player)
        {
            _player = player;
        }

        public void HandleInput(double delta)
        {
            
        }

        public void Initialize(BasePlayer player)
        {
          this._player = player;
            this.SignalManager = SignalManager.Instance;
            this.SignalManager.PlayerHasChangedState += OnPlayerHasChangedState;
        }

        private void OnPlayerHasChangedState(string animationToPlay)
        {
            this._player.AnimatedSprite2D.Play(animationToPlay);
            this._player.SoundAnimatedSprite2D.Play(animationToPlay);

        }

        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {

        }
    }
}
