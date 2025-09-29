using System;
using Godot;

namespace PrototipoKatrina.Enemy;


public partial class EnemyRatBase
{

    protected void DetectPlayer()
    {
        if (IsPlayerDetected() && _RaycastDetectPlayer.GetCollider() is CharacterBody2D collider)
            InitiateChase(collider); 
    }

    private void InitiateChase(GodotObject collider)
    {
        //if the raycast is colliding with the player, start chasing again, if it is.
        this.RangeAttackArea.Monitoring = true;
        this._RangeAttackAreaCollision.Disabled = false;
        this.CurrentState = State.Chase;
        this.CurrentPlayerPositionToChase = (collider as CharacterBody2D).GlobalPosition;
        TimerToChasePlayer.Start();
    }


    private bool IsPlayerDetected()
    {
        return _RaycastDetectPlayer.IsColliding() && CurrentState != State.Dead;
    }


    protected void ChasePlayer(float delta)
    {
        if (CurrentState == State.Chase && CurrentPlayerPositionToChase != Vector2.Zero)
        {
            Vector2 direction = this._PlayerGlobal.GetPlayerPosition() - this.GlobalPosition;
            direction = direction.Normalized();
            direction.Y = 0;
            Velocity = direction * this.EnemyResource.ChaseSpeed;
            FlipEnemy(direction.X); 
        }
    }

    protected void _on_time_to_chase_player_timeout()
    {
        this.CurrentState = State.Roaming;
        this.CurrentPlayerPositionToChase = Vector2.Zero;
        this.Velocity = Vector2.Zero;
        this.RangeAttackArea.Monitoring = false;
        this._RangeAttackAreaCollision.Disabled = true;
        TimerToChasePlayer.Stop();
    }

    protected void _on_range_attack_area_body_entered(Node2D node2D)
    {
        if(node2D is CharacterBody2D && node2D.IsInGroup(EnumGroups.player.ToString()))
        {
           var player = node2D as Katrina;
           player.ApplyDamage(
            damageAmount: this.EnemyResource.DamageAmount,
            force: new Vector2(this.EnemyResource.ForcePushDamage * (this.IsFacingRight ? 1 : -1), -this.EnemyResource.ForcePushDamage / 2)
           );
        }
    }
}


