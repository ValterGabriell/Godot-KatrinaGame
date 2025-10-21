using Godot;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Scripts.Utils;

namespace KatrinaGame.Core.Interfaces
{
    public interface IMovementComponent : IPlayerBaseComponent
    {
        bool IsMovementBlocked { get; set; }
        void Move(Vector2 direction, float PlayerSpeed);
        void Jump();
        void ApplyGravity(double delta);
    }
}