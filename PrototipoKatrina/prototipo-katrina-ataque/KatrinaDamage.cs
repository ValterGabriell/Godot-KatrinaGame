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
        public void ApplyDamage(float damageAmount, Vector2 force)
        {
            AttackUtils.PushWhenReceiveDamage(force, this);
            SetPlayerMovementBlocked(bloqueado: true);
            this.Health -= damageAmount;
            SetPlayerMovementBlocked(bloqueado: false);
            if (this.Health <= 0)
            {
                GD.Print("Katrina morreu");
                this.Health = 0;
                //EMITIR UM SINAL DE MORTE AQUI, QUEM VAI ASSINAR ESSE SINAL VAI SER O GAME MANAGER E O JOGO VAI REINICIAR,
                //OU VAI CARREGAR A ULTIMA CHECKPOINT
            }
        }
        
    }
}
