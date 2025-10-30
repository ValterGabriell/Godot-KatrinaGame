using Godot;
using KatrinaGame.Core.Interfaces;
using PrototipoMyha;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;

namespace KatrinaGame.Core
{
    public abstract partial class BasePlayer : CharacterBody2D
    {
        [Export] public bool IsMovementBlocked { get; private set; } = false;
        [Export] public float Speed { get; set; } = 300f;
        [Export] public float SneakSpeed { get; set; } = 100f;
        [Export] public float JumpVelocity { get; set; } = -300f;
        [Export] public float Gravity { get; set; } = 700f;
        public PlayerState CurrentPlayerState { get; private set; } = PlayerState.IDLE;

        protected Dictionary<string, IPlayerBaseComponent> Components = new();
        [Export] public AnimatedSprite2D AnimatedSprite2D { get; set; }



  
        public override void _Ready()
        {
            InstanciateComponents();
            foreach (var component in Components.Values)
            {
                component.Initialize(this);
            }

        }



        public void BlockMovement()
        {
            GDLogger.PrintInfo("Blocking Movement");
            IsMovementBlocked = true;
        }

        public void UnblockMovement()
        {
            GDLogger.PrintInfo("Unblocking Movement");
            IsMovementBlocked = false;
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
            if (IsMovementBlocked && Velocity.Length() > 0f)
            {
                Velocity *= 0.8f;
                if (Velocity.Length() < 1f)
                    Velocity = Vector2.Zero;
            }
            MoveAndSlide();
        }

        public void SetState(PlayerState newState)
        {
            CurrentPlayerState = newState;
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

        protected abstract void InstanciateComponents();


        public void AddComponent<T>(T component) where T : IPlayerBaseComponent
        {
            Components[typeof(T).ToString()] = component;
            AddChild(component as Node);
            
        }

        public T GetComponent<T>() where T : IPlayerBaseComponent
        {
            Components.TryGetValue(typeof(T).ToString(), out var component);
            return (T)component;
        }

    }
}