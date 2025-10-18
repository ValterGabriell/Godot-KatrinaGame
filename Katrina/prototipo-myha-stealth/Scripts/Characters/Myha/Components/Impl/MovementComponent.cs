using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using KatrinaGame.Scripts.Utils;
using PrototipoMyha.Utilidades;

namespace KatrinaGame.Components
{
    public partial class MovementComponent : Node, IMovementComponent
    {
        [Export] public float Speed { get; set; } = 200f;
        [Export] public float RunSpeed { get; set; } = 350f;
        [Export] public float JumpVelocity { get; set; } = -400f;
        [Export] public float Gravity { get; set; } = 700f;
        [Export] public bool IsMovementBlocked { get; set; } = false;


        private BasePlayer _player;
        private float _inertiaDeceleration = 20f;
        public bool IsPlayerWalking { get; private set; } =  false;

        public void Initialize(BasePlayer player)
        {
            _player = player;

        }

        public void Process(double delta) { }

        public void PhysicsProcess(double delta)
        {
            ApplyGravity(delta);
        }

        public void HandleInput(double delta) { }

        public void Move(Vector2 direction, bool isRunning = false)
        {
           
            PlayerManager.GetPlayerGlobalInstance().UpdatePlayerPosition(_player.GlobalPosition);
            if (IsMovementBlocked) return;

            float currentSpeed = isRunning ? RunSpeed : Speed;

            if (direction != Vector2.Zero)
            {
                // Só muda para animação de corrida se não estiver pulando
                if (_player.IsOnFloor())
                {
                    this._player.AnimatedSprite2D.Play(EnumAnimations.run.ToString());
                }
              
                IsPlayerWalking = true;
                _player.Velocity = new Vector2(direction.Normalized().X * currentSpeed, _player.Velocity.Y);
            }
            else
            {
                // Só muda para idle se estiver no chão
                if (_player.IsOnFloor())
                {
                    this._player.AnimatedSprite2D.Play(EnumAnimations.idle.ToString());
                }
                IsPlayerWalking = false;
                // Aplica inércia
                _player.Velocity = new Vector2(
                    Mathf.MoveToward(_player.Velocity.X, 0, _inertiaDeceleration),
                    _player.Velocity.Y
                );
            }
        }

        public void Jump()
        {
           
          
            if (IsMovementBlocked || !_player.IsOnFloor()) return;
            this._player.AnimatedSprite2D.Play(EnumAnimations.jump_up.ToString());
            _player.Velocity = new Vector2(_player.Velocity.X, JumpVelocity);
        }

        public void ApplyGravity(double delta)
        {
            if (!_player.IsOnFloor())
            {
                _player.Velocity = new Vector2(_player.Velocity.X, _player.Velocity.Y + Gravity * (float)delta);
            }
        }

        public void Cleanup() { }
    }
}