using Godot;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;

namespace KatrinaGame.Components
{
    public partial class AttackComponent : Node, IAttackComponent
    {
        [Export] public RayCast2D AttackRaycast { get; set; }
        [Export] public float AttackDamage { get; set; } = 25f;
        [Export] public float AttackForce { get; set; } = 20f;
        [Export] public float AttackCooldown { get; set; } = 0.5f;

        public bool CanAttack => _attackTimer <= 0;

        private BasePlayer _player;
        private float _attackTimer = 0f;

        [Signal]
        public delegate void AttackExecutedEventHandler();

        public void Initialize(BasePlayer player)
        {
            _player = player;
        }

        public void Process(double delta)
        {
            if (_attackTimer > 0)
            {
                _attackTimer -= (float)delta;
            }
        }

        public void PhysicsProcess(double delta) { }
        public void HandleInput(double delta) { }

        public void Attack()
        {
            if (!CanAttack) return;

            _attackTimer = AttackCooldown;

            if (AttackRaycast?.IsColliding() == true)
            {
                var collider = AttackRaycast.GetCollider();

                if (collider is BasePlayer targetPlayer)
                {
                    Vector2 attackDirection = (_player.Sprite.FlipH ? Vector2.Left : Vector2.Right);
                    Vector2 force = attackDirection * AttackForce;

                    targetPlayer.HealthComponent?.TakeDamage(AttackDamage, force);
                }
            }

            EmitSignal(SignalName.AttackExecuted);
        }

        public void SetAttackCooldown(float cooldown)
        {
            AttackCooldown = cooldown;
        }

        public void Cleanup() { }
    }
}