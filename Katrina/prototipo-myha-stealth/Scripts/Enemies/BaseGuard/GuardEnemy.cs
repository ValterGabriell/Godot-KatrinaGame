// GuardController.cs
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Impl;
using PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl;
using PrototipoMyha.Scripts.Utils.Objetos;

public partial class GuardEnemy : EnemyBase
{

    protected override void InstanciateSpecificComponents()
    {
        AddComponent(new EnemyMovementComponent(this));
        AddComponent(new EnemyAnimationComponent(this));
    }
}