using System;
using Godot;

namespace PrototipoKatrina.Enemy;


public partial class EnemyRatBase
{

    protected void DetectPlayer()
    {
        if (RaycastDetectPlayer.IsColliding() && CurrentState != State.Dead)
        {
            var collider = RaycastDetectPlayer.GetCollider();
            if (collider is CharacterBody2D)
            {
                this.RangeAttackArea.Monitoring = true;
                this.RangeAttackAreaCollision.Disabled = false;
                this.CurrentState = State.Chase;
                this.CurrentPlayerPositionToChase = (collider as CharacterBody2D).GlobalPosition;
                TimerToChasePlayer.Start();
                SetMoveDirection(this.CurrentPlayerPositionToChase - this.GlobalPosition);
            }
        }
    }

    protected void ChasePlayer(float delta)
    {
        if (CurrentState == State.Chase && CurrentPlayerPositionToChase != Vector2.Zero)
        {
            Vector2 direction = this.playerGlobal.GetPlayerPosition() - this.GlobalPosition;
            direction = direction.Normalized();
            direction.Y = 0;
            Velocity = direction * this.EnemyResource.ChaseSpeed;
        }
    }

    protected void _on_time_to_chase_player_timeout()
    {
        this.CurrentState = State.Roaming;
        this.CurrentPlayerPositionToChase = Vector2.Zero;
        SetMoveDirection(Vector2.Zero);
        this.Velocity = Vector2.Zero;
        this.RangeAttackArea.Monitoring = false;
        this.RangeAttackAreaCollision.Disabled = true;
        TimerToChasePlayer.Stop();
    }

    protected void _on_range_attack_area_body_entered(Node2D node2D)
    {
        if(node2D is CharacterBody2D && node2D.IsInGroup(EnumGroups.player.ToString()))
        {
           var player = node2D as Katrina;
           player.ApplyDamage(this.EnemyResource.DamageAmount);
        }
    }
}


