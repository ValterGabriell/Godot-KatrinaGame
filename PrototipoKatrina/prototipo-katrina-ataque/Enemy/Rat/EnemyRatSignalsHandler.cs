using System;
using Godot;

namespace PrototipoKatrina.Enemy;

public partial class EnemyRatBase : EnemyBase
{
    public void OnEnemyEnteredWarningArea(Vector2 InPositionToGo)
    {
        GD.Print("EnemyRatBase received signal to go to position: " + InPositionToGo);
        this.CurrentState = State.LookingForPlayerInDistractedArea;
        this.CurrentPlayerPositionToChase = InPositionToGo;
    }
    
    protected void ChaseDistractedArea()
    {
        if (CurrentState == State.LookingForPlayerInDistractedArea && CurrentPlayerPositionToChase != Vector2.Zero)
        {
            Vector2 direction = this.CurrentPlayerPositionToChase - this.GlobalPosition;
            direction = direction.Normalized();
            direction.Y = 0;
            Velocity = direction * this.EnemyResource.ChaseSpeed * 0.5f; // Move at half speed when going to distraction
            FlipEnemy(direction.X);
        }
    }
}
