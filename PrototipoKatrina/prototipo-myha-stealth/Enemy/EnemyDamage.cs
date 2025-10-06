using System;
using Godot;

namespace PrototipoMyha.Enemy;

public partial class EnemyBase : CharacterBody2D
{
    public void ApplyDamage(float damageAmount, Vector2 force)
    {
        if (this.CurrentState != State.Chase)
            this.CurrentState = State.Chase;
        
        AttackUtils.PushWhenReceiveDamage(force, this);
        this.EnemyResource.Health -= damageAmount;
        if (this.EnemyResource.Health <= 0)
        {
            GD.Print("Inimigo morreu");
            this.EnemyResource.Health = 0;
            this.CurrentState = State.Dead;
        
            //Animacao inimigo morto


            //EMITIR UM SINAL DE MORTE AQUI
        }
    }

}
