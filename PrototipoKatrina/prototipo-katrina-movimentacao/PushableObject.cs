using Godot;
using System;

public partial class PushableObject : RigidBody2D
{
    [Export]
    public float RaycastSize = 20.0f;

    [Export]
    public Area2D areaCima; // Referencie sua Area2D pelo editor

    public override void _PhysicsProcess(double delta)
    {
        SyncStackedObjects();
    }

    public void SyncStackedObjects()
    {
        foreach (var body in areaCima.GetOverlappingBodies())
        {
            if (body is PushableObject pushable)
            {
                // Suaviza para o movimento em X e Y
                float suavidade = 0.18f; // ajuste para mais/menos "grudado"
                Vector2 alvo = this.LinearVelocity;
                pushable.LinearVelocity = pushable.LinearVelocity.Lerp(alvo, suavidade);
            }
        }
    }

    public void Push(float amountX, float amountY = 0)
    {
        // Aplica uma força em X e (opcionalmente) Y
        LinearVelocity = new Vector2(amountX, amountY);
        LockRotation = true;
    }
}