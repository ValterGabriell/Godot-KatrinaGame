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

    protected override void InstanciateComponents()
    {
        var enemyMovement = new EnemyMovementComponent();

        AddComponent(enemyMovement);
    }
}