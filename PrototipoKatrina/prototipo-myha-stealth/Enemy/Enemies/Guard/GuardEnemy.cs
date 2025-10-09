// GuardController.cs
using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Impl;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Player.StateManager;
using System;
using System.Linq;

public partial class GuardEnemy : EnemyBase
{
    [ExportGroup("Soundable")]
    [Export] public Area2D AreaDetectPlayerSound = null;
    protected override void InstanciateSpecificComponents()
    {
        AddComponent(new EnemyMovementComponent());
        AddComponent(new SoundDetectionMovementComponent(AreaDetectPlayerSound));
    }
}