using Godot;


namespace PrototipoKatrina;
public partial class Katrina : CharacterBody2D
{
    private static PlayerGlobal PlayerGlobalInstance = null;

    override public void _Ready()
    {
        PlayerGlobalInstance = PlayerGlobal.GetPlayerGlobalInstance();
    }
    

    public override void _PhysicsProcess(double delta)
    {
        
        PlayerGlobalInstance.UpdatePlayerPosition(this.GlobalPosition);

        // Chama a função para processar a entrada do jogador.
        GetInput((float)delta);

        // Aplica a gravidade se o jogador não estiver no chão.
        if (!IsOnFloor())
            Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * (float)delta);

        if (Velocity == Vector2.Zero)
        {
            CurrentPlayerMovement = EnumMove.IDLE;
        }

        // Aplica o movimento.
        MoveAndSlide();

        // Chama a função para empurrar objetos.
        PushObject((float)delta);

    }

    public void SetPlayerMovementBlocked(bool bloqueado)
    {
        IsMovementBlocked = bloqueado;
    }

    // Processa a entrada do jogador.
    public void GetInput(float delta)
    {
        Vector2 inputVector = Vector2.Zero;

        // Obtém o input horizontal.
        if (Input.IsActionPressed("ui_right") && !IsMovementBlocked)
        {
            inputVector.X += 1;
            CurrentPlayerMovement = EnumMove.RIGHT;
        }
           
        if (Input.IsActionPressed("ui_left") && !IsMovementBlocked)
        {
            inputVector.X -= 1;
            CurrentPlayerMovement = EnumMove.LEFT;
        }
            

        // Checa se a tecla SHIFT está pressionada.
        bool isRunning = Input.IsKeyPressed(Key.Shift);
        float currentSpeed = isRunning ? RunSpeed : Speed;

        // Movimento horizontal
        if (inputVector != Vector2.Zero)
        {
            Velocity = new Vector2(inputVector.Normalized().X * currentSpeed, Velocity.Y);

            // Flip do sprite e do Raycast
            if (inputVector.X > 0)
            {
                Sprite.FlipH = false;
                PushRaycast.TargetPosition = new Vector2(Mathf.Abs(PushRaycast.TargetPosition.X), PushRaycast.TargetPosition.Y);
            }
            else if (inputVector.X < 0)
            {
                Sprite.FlipH = true;
                PushRaycast.TargetPosition = new Vector2(-Mathf.Abs(PushRaycast.TargetPosition.X), PushRaycast.TargetPosition.Y);
            }
        }
        else
        {
            // Aplica inércia para desacelerar o movimento.
            // Use um valor baixo (ex: 20) para suavidade
            Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, 20f), Velocity.Y);
        }

        // Aplica gravidade se não estiver no chão
        if (!IsOnFloor())
            Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * delta);

        // Lógica de pulo (só pula ao pressionar, não ao segurar)
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()) // ui_accept = espaço por padrão
        {
            this.SetPlayerMovementBlocked(bloqueado: false);
            Velocity = new Vector2(Velocity.X, JumpVelocity);
        }

        // NÃO zere Velocity.Y ao tocar o chão, deixe a física cuidar!
        // Só zere se estiver bugando grudando no chão, mas normalmente não precisa.
    }
}