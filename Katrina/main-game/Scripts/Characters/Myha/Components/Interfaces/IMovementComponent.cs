using Godot;
using PrototipoMyha.Player.Components.Interfaces;

namespace KatrinaGame.Core.Interfaces
{
    public interface IMovementComponent : IPlayerBaseComponent
    {
        float Speed { get; set; }
        float RunSpeed { get; set; }
        float JumpVelocity { get; set; }
        bool IsMovementBlocked { get; set; }
        bool IsPlayerWalking { get;}

        void Move(Vector2 direction, bool isRunning = false);
        void Jump();
        void ApplyGravity(double delta);
    }
}