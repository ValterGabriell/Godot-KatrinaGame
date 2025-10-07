using Godot;
using PrototipoMyha.Player.Components.Interfaces;

namespace KatrinaGame.Core.Interfaces
{
    public interface IAttackComponent : IPlayerComponent
    {
        bool CanAttack { get; }
        void Attack();
        void SetAttackCooldown(float cooldown);
    }
}