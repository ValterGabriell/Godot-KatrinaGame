// GuardController.cs
using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Impl;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl;
using System;
using System.Linq;

public partial class GuardEnemy : EnemyBase
{

    protected override void InstanciateSpecificComponents()
    {
        AddComponent(new EnemyMovementComponent(this));
        AddComponent(new EnemyAnimationComponent(this));
    }
}