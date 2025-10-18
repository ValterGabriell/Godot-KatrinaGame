using Godot;
using KatrinaGame.Core;
using PrototipoMyha.Enemy.States;
using PrototipoMyha.Utilidades;
using System;
using static Godot.TextServer;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.StatesHandler.Chase_Alerted
{
    public class EnemyStateChasingHandler : EnemyStateChaseAlertedBase
    {
        public EnemyStateChasingHandler(Vector2 inTargetMovement) : base(inTargetMovement)
        {

        }

        public override float ExecuteState(double delta, EnemyBase InEnemy, Vector2? InPositionToChase = null)
        {
            return base.ExecuteState(delta, InEnemy, InPositionToChase);
        }
    }
}