using Godot;
using KatrinaGame.Core.Interfaces;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Player.StateManager;
using System.Collections.Generic;

namespace KatrinaGame.Core
{
    public abstract partial class BasePlayer : CharacterBody2D
    {
        public PlayerState CurrentPlayerState { get; private set; } = PlayerState.IDLE;

        protected Dictionary<string, IPlayerBaseComponent> Components = new();

        public override void _Ready()
        {
            InstanciateComponents();
            foreach (var component in Components.Values)
            {
                component.Initialize(this);
            }
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
            GD.Print("Recebi isso: " + typeof(T).ToString());
            AddChild(component as Node);
            
        }

        public T GetComponent<T>() where T : IPlayerBaseComponent
        {
            Components.TryGetValue(typeof(T).ToString(), out var component);
            GD.Print("Busquei isso: " + typeof(T).ToString());
            return (T)component;
        }

    }
}