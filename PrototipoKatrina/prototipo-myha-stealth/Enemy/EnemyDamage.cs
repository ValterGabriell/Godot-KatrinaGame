//using System;
//using Godot;
//using PrototipoMyha.Enemy.States;

//namespace PrototipoMyha.Enemy;

//public partial class EnemyBase : CharacterBody2D
//{
//    public void ApplyDamage(float damageAmount, Vector2 force)
//    {
//        if (this.CurrentState != EnemyState.Chase)
//            this.CurrentState = EnemyState.Chase;
        
//        AttackUtils.PushWhenReceiveDamage(force, this);
//        this.EnemyResource.Health -= damageAmount;
//        if (this.EnemyResource.Health <= 0)
//        {
//            GD.Print("Inimigo morreu");
//            this.EnemyResource.Health = 0;
//            this.CurrentState = EnemyState.Dead;
        
//            //Animacao inimigo morto


//            //EMITIR UM SINAL DE MORTE AQUI
//        }
//    }

//}
