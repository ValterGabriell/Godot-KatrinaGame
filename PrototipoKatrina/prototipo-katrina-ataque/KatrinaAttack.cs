using System;
using System.Threading.Tasks;
using Godot;

namespace PrototipoKatrina;

public partial class Katrina : CharacterBody2D
{
    public void Attack()
    {
        GD.Print("Ataque");
        this.AttackArea.Monitoring = true;
        this.AttackArea.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public void _on_attack_area_body_entered(Node2D node2D)
    {
        if (node2D is Enemy.EnemyBase && node2D.IsInGroup(EnumGroups.enemy.ToString()))
        {
            GD.Print("Colidiu com inimigo");
            var enemy = node2D as Enemy.EnemyRatBase;
            enemy.ApplyDamage(
                damageAmount: this.Attack01Force,
                force: new Vector2(300 * (this.Sprite.FlipH ? -1 : 1), -150)
            );
            enemy.InitiateChase(this.GlobalPosition);
        }

    }
}
