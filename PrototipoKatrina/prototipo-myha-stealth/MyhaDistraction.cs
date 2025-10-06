using System;
using Godot;

namespace PrototipoMyha;

public partial class Myha_2 : CharacterBody2D
{


    public void ThrowBallToDistract(float delta)
    {
        var ball = BallScene.Instantiate<LittleBall>();
        ball.GlobalPosition = this.GlobalPosition;
        GetParent().AddChild(ball);

        var direction = this.Sprite.FlipH ? -1 : 1;
        ball.Launch(new Vector2(400 * direction, -250)); 
    }
}
