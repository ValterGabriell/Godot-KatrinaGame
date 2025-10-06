using System;
using Godot;

namespace PrototipoMyha.Enemy;

public partial class EnemyRatBase : EnemyBase
{
    public void OnEnemyEnteredWarningArea(Vector2 InPositionToGo)
    {
        if(this.CurrentState != State.Dead && this.CurrentState != State.Chase){
                this.CurrentState = State.LookingForPlayerInDistractedArea;
                this.CurrentPlayerPositionToChase = InPositionToGo;
        }

    }

    /*called on enemy rat movement*/
    protected void ChaseDistractedArea()
    {
        if (CurrentState == State.LookingForPlayerInDistractedArea && CurrentPlayerPositionToChase != Vector2.Zero)
        {
            Vector2 direction = this.CurrentPlayerPositionToChase - this.GlobalPosition;
            direction = direction.Normalized();
            direction.Y = 0;
            Velocity = direction * this.EnemyResource.ChaseSpeed * 0.1f; // Move at half speed when going to distraction
            FlipEnemy(direction.X);

            if (this.GlobalPosition.DistanceTo(CurrentPlayerPositionToChase) < 10f)
            {
                this.Velocity = Vector2.Zero;
                this.CurrentPlayerPositionToChase = Vector2.Zero;
                LookForPlayerTimer.Start();
            }
        }
    }

    public void _SignalWhenLookForPlayer()
    {
        if (this.CurrentState == State.LookingForPlayerInDistractedArea)
              this.CurrentState = State.Roaming;
      
    }
}
