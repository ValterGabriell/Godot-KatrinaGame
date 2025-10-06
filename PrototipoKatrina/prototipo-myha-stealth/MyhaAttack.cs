using System;
using System.Threading.Tasks;
using Godot;

namespace PrototipoMyha;

public partial class Myha_2 : CharacterBody2D
{
    public void Attack()
    {
        GD.Print("Ataque");
        if(this.AttackRaycast.IsColliding())
        {
            var collider = this.AttackRaycast.GetCollider();
            if (collider is Enemy.EnemyBase && (collider as Node2D).IsInGroup(EnumGroups.enemy.ToString()))
            {
                GD.Print("Atacou o inimigo");
                var enemy = collider as Enemy.EnemyRatBase;
                enemy.ApplyDamage(
                    damageAmount: this.Attack01Force,
                    force: new Vector2(300 * (this.Sprite.FlipH ? -1 : 1), -150)
                );
            }
        }
    }
}
