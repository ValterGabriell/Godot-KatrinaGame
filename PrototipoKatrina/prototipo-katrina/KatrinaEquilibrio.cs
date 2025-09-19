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
        private float Equilibrio = 0.0f;
        private float EquilibrioMax = 100.0f;

        public void AfetaEquilibrio(float intensidade)
        {
            Equilibrio += intensidade;
            GD.Print("Equilibrio: " + Equilibrio);
            if (Equilibrio > EquilibrioMax)
            {
                Velocity = new Vector2(Velocity.X, -100);
            }
        }

        public EnumMove GetCurrentPlayerMovement()
        {
            return this.CurrentPlayerMovement;
        }

        public void ResetEquilibrio()
        {
            Equilibrio = 0.0f;
        }

        public void DiminuiVelocidadeKatrina()
        {
            Speed = 50.0f; // Velocidade mínima
        }

        public void NormalizaVelocidadeKatrina()
        {
            Speed = 200.0f; // Velocidade mínima
        }

    }
}
