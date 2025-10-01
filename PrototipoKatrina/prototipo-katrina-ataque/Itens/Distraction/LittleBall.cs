using Godot;
using System;

public partial class LittleBall : RigidBody2D
{
    public void Launch(Vector2 initialImpulse)
    {
        ApplyImpulse(initialImpulse);
    }
}
