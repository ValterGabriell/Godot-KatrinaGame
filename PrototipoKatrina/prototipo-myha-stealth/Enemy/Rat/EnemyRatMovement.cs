using System;
using Godot;

namespace PrototipoKatrina.Enemy;

public partial class EnemyRatBase 
{
    private bool IsFacingRight = false;
    public override void _Process(double delta)
    {

        if (!IsOnFloor())
            this.Velocity += Vector2.Down * (float)delta * 1000;

        Move((float)delta);
        MoveAndSlide();
    }

    private void Move(float delta)
    {
        if (CurrentState == State.Roaming && GetMoveDirection() != Vector2.Zero)
            this.Velocity = GetMoveDirection() * EnemyResource.MoveSpeed * delta;
        
        
        if (CurrentState != State.Dead)
            DetectPlayer();

        if (CurrentState == State.Chase)
            ChasePlayer();

        if(CurrentState == State.LookingForPlayerInDistractedArea)
            ChaseDistractedArea();
        

        if (CurrentState == State.Dead)
            this.Velocity = Vector2.Zero;

    }


    public void _on_direction_timer_timeout()
    {
        DirectionTimer.WaitTime = Choose([5f, 10f]);
        if (CurrentState != State.Chase)
        {
            float enemyMovementDirectionX = Choose([Vector2.Right.X, Vector2.Left.X]);
            SetMoveDirection(new Vector2(enemyMovementDirectionX, 0));
            FlipEnemy(enemyMovementDirectionX);
        }
    }

    private void FlipEnemy(float enemyMovementDirectionX)
    {
        this.IsFacingRight = enemyMovementDirectionX > 0;
        this._Sprite.FlipH = enemyMovementDirectionX < 0;
        this._RaycastDetectPlayer.TargetPosition = new Vector2(50 * enemyMovementDirectionX, 0);
    }


    private float Choose(float[] array)
    {
        Shuffle(array);
        return array[0];
    }
    
    
    public void Shuffle(float[] array)
    {
        Random rng = new Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            float value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
}
