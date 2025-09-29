using Godot;
using System;

public partial class EnemyResources : Resource
{
    [Export] public int MaxHealth = 100;

    [Export] public int DamageAmount = 10;

    [Export] public float MoveSpeed = 900f;

    [Export] public float ChaseSpeed = 100f;
    [Export] public float ForcePushDamage = 200f;

}
