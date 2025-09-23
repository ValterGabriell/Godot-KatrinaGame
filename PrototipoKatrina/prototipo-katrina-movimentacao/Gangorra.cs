using Godot;
using System;

// Define a classe Gangorra, que herda de StaticBody2D (um corpo estático na cena)
public partial class Gangorra : StaticBody2D
{
    private CharacterBody2D player = null;

    [Export] public float GangorraComprimento = 200.0f;

    [Export] public float Gravidade = 2.0f;

    [Export] public float LimiteAnguloGraus = 30.0f;


    private float velocidade = 0.0f;

    private float limiteAnguloRad => Mathf.DegToRad(LimiteAnguloGraus);
    
    public void _on_area_2d_body_entered(Node2D body)
    {
        if (body is CharacterBody2D)
        {
            player = body as CharacterBody2D;
        }
    }
    
    public void _on_area_2d_body_exited(Node2D body)
    {
        if (body is CharacterBody2D)
        {
            player = null;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player == null)
        {
            GD.Print("Player não está na gangorra");
            // Se o player não está na gangorra, desacelera gradualmente (simula atrito)
            velocidade *= 0.96f;
              // Aplica a velocidade angular à rotação da gangorra
            Rotation += velocidade * (float)delta;

            // Limita a rotação para não passar do ângulo máximo para cada lado
            if (Rotation > limiteAnguloRad)
            {
                Rotation = limiteAnguloRad;
                velocidade = 0f;
            }
            else if (Rotation < -limiteAnguloRad)
            {
                Rotation = -limiteAnguloRad;
                velocidade = 0f;
            }
            return;
        }

        // Converte a posição global do player para o sistema de coordenadas local da gangorra
        Vector2 posPlayerLocal = ToLocal(player.GlobalPosition);
        float direcao = 0.0f;

   

        // Verifica se o player está em cima da gangorra (dentro do comprimento e de uma altura limite)
        if (Mathf.Abs(posPlayerLocal.X) < GangorraComprimento / 2 && Mathf.Abs(posPlayerLocal.Y) < 50)
        {
            // Se o player está à direita do centro, balança para a direita
            if (posPlayerLocal.X > 10)
                direcao = 1.0f;
            // Se o player está à esquerda do centro, balança para a esquerda
            else if (posPlayerLocal.X < -10)
                direcao = -1.0f;

            // Aumenta a velocidade angular conforme a direção e "gravidade"
            velocidade += Gravidade * direcao * (float)delta;
        }
        else
        {
            // Se o player não está na gangorra, desacelera gradualmente (simula atrito)
            velocidade *= 0.96f;
        }

        // Aplica a velocidade angular à rotação da gangorra
        Rotation += velocidade * (float)delta;

        // Limita a rotação para não passar do ângulo máximo para cada lado
        if (Rotation > limiteAnguloRad)
        {
            Rotation = limiteAnguloRad;
            velocidade = 0f;
        }
        else if (Rotation < -limiteAnguloRad)
        {
            Rotation = -limiteAnguloRad;
            velocidade = 0f;
        }
    }
}