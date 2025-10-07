using Godot;
using KatrinaGame.Core.Interfaces;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Player.StateManager;
using System.Collections.Generic;

namespace KatrinaGame.Core
{
    public abstract partial class BasePlayer : CharacterBody2D
    {
        [Export] public string PlayerName { get; set; }
        [Export] public Sprite2D Sprite { get; set; }

        public PlayerState CurrentPlayerState { get; private set; } = new();

        protected Dictionary<System.Type, IPlayerComponent> Components = new();

        // Componentes principais
        public IHealthComponent HealthComponent => GetComponent<IHealthComponent>();
        public IMovementComponent MovementComponent => GetComponent<IMovementComponent>();
        public IAttackComponent AttackComponent => GetComponent<IAttackComponent>();

        [Signal]
        public delegate void PlayerDeathEventHandler();

        [Signal]
        public delegate void PlayerDamagedEventHandler(float damage);

        public void ChangeState(PlayerState newState)
        {
            CurrentPlayerState = newState;
        }

        public override void _Ready()
        {
            InitializeComponents();
            SetupSignals();
        }

        public override void _Process(double delta)
        {
            foreach (var component in Components.Values)
            {
                component.Process(delta);
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            HandleInput(delta);

            foreach (var component in Components.Values)
            {
                component.PhysicsProcess(delta);
            }

            MoveAndSlide();
        }

        protected void FlipRaycast(float direction, List<RayCast2D> Rasycast)
        {
            foreach (var current in Rasycast)
            {
                current.TargetPosition = new Vector2(direction * Mathf.Abs(current.TargetPosition.X), current.TargetPosition.Y);
            }
        }

        protected virtual void HandleInput(double delta)
        {
            foreach (var component in Components.Values)
            {
                component.HandleInput(delta);
            }
        }

        protected abstract void InitializeComponents();

        protected virtual void SetupSignals()
        {
            if (HealthComponent != null)
            {
                // Conectar sinais de saúde se necessário
            }
        }

        public void AddComponent<T>(T component) where T : IPlayerComponent
        {
            Components[typeof(T)] = component;
            component.Initialize(this);
        }

        public T GetComponent<T>() where T : IPlayerComponent
        {
            Components.TryGetValue(typeof(T), out var component);
            return (T)component;
        }

    }
}