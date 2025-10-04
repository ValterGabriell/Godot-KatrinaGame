using Godot;
using PrototipoKatrina;
using System;

public partial class LittleBall : RigidBody2D
{
    private bool HasCollidedWithFloor = false;
     [Export] public Area2D EmitWarningArea;

 


    public void Launch(Vector2 initialImpulse)
    {
        ApplyImpulse(initialImpulse);
    }


    public void _on_floor_collision_area_body_entered(Node2D body)
    {
        if (body.IsInGroup(EnumGroups.terrain.ToString()) && !HasCollidedWithFloor)
        {
            GD.Print("Collided with floor");
            HasCollidedWithFloor = true;

            // Ativa monitoramento da área de aviso se necessário
            EmitWarningArea.Monitoring = true;
            
            // Checa manualmente todos os corpos já presentes
            foreach (var node in EmitWarningArea.GetOverlappingBodies())
            {
                if (node is Node2D n2d && n2d.IsInGroup(EnumGroups.enemy.ToString()))
                {
                    GD.Print("Enemy was already in warning area");
                    SignalManager.Instance.EmitSignal(nameof(SignalManager.EnemyEnteredWarningArea), this.GlobalPosition);
                }
            }
        }
    }

    public void _on_emit_warning_area_body_entered(Node2D body)
    {
        if (body.IsInGroup(EnumGroups.enemy.ToString()) && HasCollidedWithFloor)
        {
            GD.Print("Enemy entered warning area");
            SignalManager.Instance.EmitSignal(nameof(SignalManager.EnemyEnteredWarningArea), this.GlobalPosition);
        }
    }

}
