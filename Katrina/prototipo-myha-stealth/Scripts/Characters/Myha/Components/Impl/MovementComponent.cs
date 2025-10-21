using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using KatrinaGame.Scripts.Utils;
using PrototipoMyha;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Scripts.Utils;
using PrototipoMyha.Utilidades;

namespace KatrinaGame.Components
{
    public partial class MovementComponent : Node, IMovementComponent
    {

        [Export] public bool IsMovementBlocked { get; set; } = false;

        private BasePlayer _player;
        private float _inertiaDeceleration = 20f;

        private bool _wasOnFloorLastFrame = true; // Adicionar esta variável

        //usando no walk sound area
        public bool IsPlayerWalking { get; private set; } = false;

        public SignalManager SignalManager { get; private set; } = SignalManager.Instance;

        public void Initialize(BasePlayer player)
        {
            _player = player;
        }

        public void Process(double delta) { }

        public void PhysicsProcess(double delta)
        {
            ApplyGravity(delta);
            CheckLanding(); 
        }

        public void HandleInput(double delta) { }

        public void Move(Vector2 direction, float CurrentSpeed)
        {
            GDLogger.PrintInfo(CurrentSpeed);
            PlayerManager.GetPlayerGlobalInstance().UpdatePlayerPosition(_player.GlobalPosition);
            if (IsMovementBlocked) return;

            var isPlayerMoving = direction.X != 0;
            var isPlayerOnFloor = this._player.IsOnFloor();
            var isPlayerJumping = this._player.CurrentPlayerState == PlayerState.JUMPING;

            if (isPlayerMoving)
            {
                IsPlayerWalking = true;
                _player.Velocity = new Vector2(direction.Normalized().X * CurrentSpeed, _player.Velocity.Y);
                UpdateSpriteDirection(direction);
            }

            if (IsMovingConditionMet(isPlayerMoving, isPlayerOnFloor, isPlayerJumping))
            {
                var newState = CurrentSpeed == _player.Speed ? PlayerState.RUN : PlayerState.SNEAK;
                var newRadiusSound = CurrentSpeed == _player.Speed ? PlayerManager.RunNoiseRadius : PlayerManager.SneakNoiseRadius;
                this._player.SetState(newState);
                SignalManager.EmitSignal(nameof(SignalManager.PlayerIsMoving), newRadiusSound);
            }

            if (IsIdleConditionMet(isPlayerMoving, isPlayerOnFloor, isPlayerJumping))
            {
                this._player.SetState(PlayerState.IDLE);
                SignalManager.EmitSignal(nameof(SignalManager.PlayerStoped));
            }

            if (!isPlayerMoving)
            {
                IsPlayerWalking = false;
                _player.Velocity = new Vector2(
                    Mathf.MoveToward(_player.Velocity.X, 0, _inertiaDeceleration),
                    _player.Velocity.Y
                );
            }

            CheckLanding();
        }

        public void Jump()
        {
            if (IsMovementBlocked || !_player.IsOnFloor()) return;

            this._player.SetState(PlayerState.JUMPING);
            _player.Velocity = new Vector2(_player.Velocity.X, _player.JumpVelocity);
            SignalManager.EmitSignal(nameof(SignalManager.PlayerIsMoving), PlayerManager.JumpNoiseRadius);
        }


        private static bool IsIdleConditionMet(bool isPlayerMoving, bool isPlayerOnFloor, bool isPlayerJumping)
        {
            return !isPlayerMoving && !isPlayerJumping && isPlayerOnFloor;
        }

        private static bool IsMovingConditionMet(bool isPlayerMoving, bool isPlayerOnFloor, bool isPlayerJumping)
        {
            return isPlayerMoving && isPlayerOnFloor && !isPlayerJumping;
        }

        private void UpdateSpriteDirection(Vector2 direction)
        {
            if (direction.X > 0)
            {
                _player.AnimatedSprite2D.FlipH = false;
            }
            else if (direction.X < 0)
            {
                _player.AnimatedSprite2D.FlipH = true;
            }
        }

  
        // Adicionar este método para detectar quando o jogador pousa
        private void CheckLanding()
        {
            /*
            Frame 1: Jogador no chão → _wasOnFloorLastFrame = true
            Frame 2: Jogador pula → _player.IsOnFloor() = false → _wasOnFloorLastFrame = false  
            Frame 3: Jogador no ar → _wasOnFloorLastFrame = false
            Frame 4: Jogador pousa → _player.IsOnFloor() = true, _wasOnFloorLastFrame = false
                     ↳ DETECÇÃO DE POUSO! (!false && true = true)
            Frame 5: Após detecção → _wasOnFloorLastFrame = true
             */
            bool isOnFloorNow = _player.IsOnFloor();

            // Se não estava no chão no frame anterior mas agora está (acabou de pousar)
            if (!_wasOnFloorLastFrame && isOnFloorNow && _player.CurrentPlayerState == PlayerState.JUMPING)
            {
                // CORREÇÃO: Ao pousar, verifica se está se movendo para escolher o estado apropriado
                if (IsPlayerWalking)
                {
                    _player.SetState(PlayerState.RUN);
                }
                else
                {
                    _player.SetState(PlayerState.IDLE);
                }
            }

            _wasOnFloorLastFrame = isOnFloorNow;
        }

        public void ApplyGravity(double delta)
        {
            if (!_player.IsOnFloor())
            {
                _player.Velocity = new Vector2(_player.Velocity.X, _player.Velocity.Y + _player.Gravity * (float)delta);
            }
        }

        public void Cleanup() { }
    }
}