using Godot;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Scripts.Utils;

namespace KatrinaGame.Core.Interfaces
{
    public interface IMovementComponent : IPlayerBaseComponent
    {

        void Move(Vector2 direction, float PlayerSpeed);
        void Jump();
        void ApplyGravity(double delta);
        bool IsWallWalkInactive();
            bool IsWallWalkActive();
    }
}