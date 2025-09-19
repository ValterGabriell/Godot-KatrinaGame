using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoKatrina
{
    public partial class Katrina : CharacterBody2D
    {
        private const float INITAL_MAX_BALLANCE = 25.0f;
        private float Equilibrio = INITAL_MAX_BALLANCE;
        private float BallanceZero = 0.0f;
        private float BallanceMax = INITAL_MAX_BALLANCE;

        [Signal]
        public delegate void BalanceZeroEventHandler();

        public void AffectBalance(float intensidade)
        {
            if(Equilibrio > BallanceZero)
            {
                Equilibrio -= intensidade;
                GD.Print("Equilibrio: " + Equilibrio);
            }

            if(Equilibrio <= BallanceZero)
            {
                FallDown();
            }
            
        }

        private void FallDown()
        {
            // Move o jogador levemente para baixo para garantir que saia da colisão
            this.Position += new Vector2(0, 505);

            // Define a velocidade para baixo
            Velocity = new Vector2(Velocity.X, 500);

            EmitSignal(nameof(BalanceZero));
        }

        public void DecreaseBallance(float delta)
        {            
            if(Equilibrio < BallanceMax)
            {
                Equilibrio += 30f * delta;
                Equilibrio = Mathf.Max(Equilibrio, 0);
            }
        }


        public EnumMove GetCurrentPlayerMovement()
        {
            return this.CurrentPlayerMovement;
        }

        public void ResetBallance()
        {
            Equilibrio = INITAL_MAX_BALLANCE;
        }

        public void SlowKatrinaVelocity()
        {
            Speed = 50.0f; // Velocidade mínima
        }

        public void NormalizeKatrinaVelocity()
        {
            Speed = 200.0f; // Velocidade mínima
        }

    }
}
