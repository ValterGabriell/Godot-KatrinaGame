using Godot;
using PrototipoKatrina;
using System;
using System.Diagnostics.Contracts;

public partial class ShakeObject : StaticBody2D
{
    [Export] public Area2D AreaDetectPlayer;
    private float RotationSpeed = 5.0f;
    private bool isPlayerOnObject = false;
    private Katrina player;
    private int direction = 1; // 1 para direita, -1 para esquerda

    public override void _Ready()
    {
        AreaDetectPlayer.BodyEntered += OnBodyEntered;
        AreaDetectPlayer.BodyExited += OnBodyExited;
    }

    private void OnBodyExited(Node2D body)
    {
        if (body.IsInGroup(EnumGroups.player.ToString()))
        {
            isPlayerOnObject = false;
            player = null;
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup(EnumGroups.player.ToString()))
        {
            isPlayerOnObject = true;
            player = body as Katrina;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player != null)
        {
            var currentPlayerMovement = player.GetCurrentPlayerMovement();
            if (isPlayerOnObject)
            {
                Vector2 deltaPlataforma = Vector2.Zero;
                if (currentPlayerMovement == EnumMove.RIGHT)
                {
                    direction = 1;
                    this.RotationDegrees += RotationSpeed * (float)delta;
                    player.AfetaEquilibrio(RotationDegrees * direction * (float)delta);
                }

                if (currentPlayerMovement == EnumMove.LEFT)
                {
                    direction = -1;
                    this.RotationDegrees += RotationSpeed * direction * (float)delta;
                    player.AfetaEquilibrio(RotationDegrees * direction * (float)delta);
                }

                if(currentPlayerMovement == EnumMove.IDLE)
                {
                    player.ReduzEquilibrio((float)delta );
                }
            }

          

            // MOVER HORIZONTALMENTE JUNTO COM O PLAYER (opcional)
            // Exemplo: move junto com o player enquanto ele anda em cima da plataforma
            float moveSpeed = 10f; // ajuste conforme necessário
            if (currentPlayerMovement == EnumMove.RIGHT)
            {
                this.Position += new Vector2(moveSpeed * (float)delta, 0);
            }
            else if (currentPlayerMovement == EnumMove.LEFT)
            {
                this.Position -= new Vector2(moveSpeed * (float)delta, 0);
            }
        }
        
    }
}
