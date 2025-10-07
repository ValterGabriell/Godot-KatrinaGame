using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;

namespace KatrinaGame.Components
{
    public partial class HealthComponent : Node, IHealthComponent
    {
        [Export] public float MaxHealth { get; private set; } = 100f;
        [Export] public float CurrentHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        private BasePlayer _player;

        [Signal]
        public delegate void HealthChangedEventHandler(float currentHealth, float maxHealth);

        [Signal]
        public delegate void DeathEventHandler();

        public void Initialize(BasePlayer player)
        {
            _player = player;
            CurrentHealth = MaxHealth;
        }

        public void Process(double delta) { }
        public void PhysicsProcess(double delta) { }
        public void HandleInput(double delta) { }

        public void TakeDamage(float damage, Vector2 force = default)
        {
            if (!IsAlive) return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
            EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
            _player.EmitSignal(BasePlayer.SignalName.PlayerDamaged, damage);

            if (force != Vector2.Zero)
            {
                ApplyKnockback(force);
            }

            if (!IsAlive)
            {
                HandleDeath();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
            EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
        }

        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
            EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
        }

        private void ApplyKnockback(Vector2 force)
        {
            if (_player.MovementComponent != null)
            {
                _player.MovementComponent.IsMovementBlocked = true;
                _player.Velocity = force;

                // Desbloqueio automático após um tempo
                GetTree().CreateTimer(0.3).Timeout += () =>
                {
                    if (_player.MovementComponent != null)
                        _player.MovementComponent.IsMovementBlocked = false;
                };
            }
        }

        private void HandleDeath()
        {
            EmitSignal(SignalName.Death);
            _player.EmitSignal(BasePlayer.SignalName.PlayerDeath);

            if (_player.MovementComponent != null)
            {
                _player.MovementComponent.IsMovementBlocked = true;
                _player.Velocity = Vector2.Zero;
            }
        }

        public void Cleanup() { }
    }
}