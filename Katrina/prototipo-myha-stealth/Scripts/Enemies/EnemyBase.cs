using Godot;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Scripts.Utils;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;

namespace PrototipoMyha.Enemy;

public abstract partial class EnemyBase : CharacterBody2D
{
    [Export] public EnemyResources EnemyResource;

    [ExportGroup("Detection")]
    [Export] public RayCast2D RayCast2DDetection = null;
    [Export] public CircleShape2D CircleAreaDetection = null;

    [ExportGroup("Chasing")]
    [Export] public Timer TimerToChase = null;
    [Export] public AnimatedSprite2D AnimatedSprite2DEnemy = null;

    [ExportGroup("Bounderies")]
    [Export] public RayCast2D Raycast2DBounderie = null;




    protected Dictionary<string, IEnemyBaseComponents> Components = new();
    private SignalManager SignalManager;
    public EnemyState CurrentEnemyState { get; private set; }  = EnemyState.Roaming;


    public override void _Ready()
    {
        InstanciateSpecificComponents();
        TimerToChase.Timeout += OnTimerToChaseTimeout;
        SignalManager = SignalManager.Instance;
        SignalManager.FlipObject += FlipEnemy;
        foreach (var component in Components.Values)
        {
            component.Initialize();
        }
    }

    private void OnTimerToStayAlertTimeout()
    {
        if (this.CurrentEnemyState == EnemyState.Alerted)
            SetState(EnemyState.Waiting);
    }

    public void FlipEnemy(int direction)
    {
        RaycastUtils.FlipRaycast(direction, [RayCast2DDetection, Raycast2DBounderie]);
        SpriteUtils.FlipSprite(direction, AnimatedSprite2DEnemy);
    }

    private void OnTimerToChaseTimeout()
    {
        if(this.CurrentEnemyState == EnemyState.Chasing)
            SetState(EnemyState.Waiting);
    }

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
    protected abstract void InstanciateSpecificComponents();

    public override void _PhysicsProcess(double delta)
    {
        foreach (var component in Components.Values)
        {
            component.PhysicsProcess(delta);
        }

        if (!IsOnFloor())
        {
    
            this.Velocity += new Vector2(0, EnemyResource.Gravity) * (float)delta;
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
