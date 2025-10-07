using Godot;
using PrototipoMyha.Player.Components.Interfaces;

namespace KatrinaGame.Core.Interfaces
{
    public interface IHealthComponent : IPlayerComponent
    {
        float MaxHealth { get; }
        float CurrentHealth { get; }
        bool IsAlive { get; }

        void TakeDamage(float damage, Vector2 force = default);
        void Heal(float amount);
        void SetMaxHealth(float maxHealth);
    }
}