using Godot;

using PrototipoMyha;
using System;

public partial class LittleBall : RigidBody2D
{
    private bool HasCollidedWithFloor = false;
     [Export] public Area2D EmitWarningArea;

 


    public void Launch(Vector2 initialImpulse)
    {
        ApplyImpulse(initialImpulse);
    }



    public void _SignalWhenTouchFloor(Node2D body)
    {
        if (body.IsInGroup(EnumGroups.terrain.ToString()) && !HasCollidedWithFloor)
        {
            HasCollidedWithFloor = true;

            // Checa manualmente todos os corpos já presentes
            foreach (var node in EmitWarningArea.GetOverlappingBodies())
            {
                if (node is Node2D n2d && n2d.IsInGroup(EnumGroups.enemy.ToString()))
                {
                    SignalManager.Instance.EmitSignal(nameof(SignalManager.EnemyEnteredWarningArea), this.GlobalPosition);
                }
            }
            this.EmitWarningArea.QueueFree();
        }
    }
}
