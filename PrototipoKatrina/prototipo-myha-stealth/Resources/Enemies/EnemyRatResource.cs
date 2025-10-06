using System;
using Godot;

namespace PrototipoMyha.Resources.Enemies;

public partial class EnemyRatResource: EnemyResources
{
    [Export] public float AttackRange = 20f;
    [Export] public float ChaseRange = 100f;
}
