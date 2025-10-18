using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using KatrinaGame.Scripts.Utils;
using PrototipoMyha.Player.Components.Interfaces;
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
        }

        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {

            // Só muda para animação de corrida se não estiver pulando
            if (_player.CurrentPlayerState == Player.StateManager.PlayerState.RUN)
            {
                this._player.AnimatedSprite2D.Play(EnumAnimations.run.ToString());
            }
            // Só muda para idle se estiver no chão
            if (_player.CurrentPlayerState == Player.StateManager.PlayerState.IDLE)
            {
                this._player.AnimatedSprite2D.Play(EnumAnimations.idle.ToString());
            }

            // Só muda para idle se estiver no chão
            if (_player.CurrentPlayerState == Player.StateManager.PlayerState.JUMPING)
            {
                this._player.AnimatedSprite2D.Play(EnumAnimations.jump_up.ToString());
            }
        }
    }
}
