using System;
using Godot;

namespace PrototipoKatrina.Enemy;

public partial class EnemyRatBase 
{
    public override void _Process(double delta)
    {
   
        if (!IsOnFloor())
            this.Velocity += Vector2.Down * (float)delta * 1000;

        Move((float)delta);
        MoveAndSlide();
    }

    private void Move(float delta)
    {
        if (CurrentState == State.Roaming && MoveDirection != Vector2.Zero)
        {
            this.Velocity = MoveDirection * EnemyResource.MoveSpeed * delta;
            CurrentState = State.Roaming;
        }
        
        if(CurrentState != State.Dead)
            DetectPlayer();

        if (CurrentState == State.Chase)
        {
            ChasePlayer(delta);
        }

        if (CurrentState == State.Dead)
            this.Velocity = Vector2.Zero;

    }


    public void _on_direction_timer_timeout()
    {

        DirectionTimer.WaitTime = Choose([5f, 10f]);

        if (CurrentState != State.Chase)
        {
            float dirX = Choose([Vector2.Right.X, Vector2.Left.X]);
            MoveDirection = new Vector2(dirX, 0);
            this.Velocity = Vector2.Zero;
        }
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
