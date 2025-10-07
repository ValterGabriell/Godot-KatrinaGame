using Godot;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Enemy.States;
using System.Collections.Generic;

namespace PrototipoMyha.Enemy;

public abstract partial class EnemyBase : CharacterBody2D
{
    [Export] public EnemyResources EnemyResource;

    public Vector2 CurrentPlayerPositionToChase { get; private set; } = Vector2.Zero;

    protected Dictionary<string, IEnemyBaseComponents> Components = new();

    public EnemyState CurrentEnemyState { get; private set; }  = EnemyState.Roaming;


    public void SetState(EnemyState newState)
    {
        CurrentEnemyState = newState;
    }

    public void AddComponent<T>(T component) where T : IEnemyBaseComponents
    {
        Components[component.GetType().ToString()] = component;
        AddChild(component as Node);
    }

    public void RemoveComponent<T>(T component) where T : IEnemyBaseComponents
    {
        Components.Remove(component.GetType().ToString());
        RemoveChild(component as Node);
        (component as Node).QueueFree();
    }
    protected abstract void InstanciateComponents();

    public override void _Ready()
    {
        InstanciateComponents();

        foreach (var component in Components.Values)
        {
            component.Initialize(this);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (var component in Components.Values)
        {
            component.PhysicsProcess(delta);
        }
        MoveAndSlide();
    }

    public override void _Process(double delta)
    {
        foreach (var component in Components.Values)
        {
            component.Process(delta);
        }
    }

    protected EnemyState CurrentState = EnemyState.Roaming;
}
