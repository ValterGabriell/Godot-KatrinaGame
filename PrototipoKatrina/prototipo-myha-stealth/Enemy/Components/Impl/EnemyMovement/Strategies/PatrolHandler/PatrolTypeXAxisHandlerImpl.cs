using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler
{
    public class PatrolTypeXAxisHandlerImpl : IPatrolTypeHandler
    {
        public Vector2 GetPatrolTarget(float InPatrolRadius, Random InRandomInstance)
        {
            // Gera um ponto aleatório apenas no eixo X (horizontal)
            float offsetX = (float)(InRandomInstance.NextDouble() * 2 - 1) * InPatrolRadius;
            
            if(offsetX > 350)
                offsetX = 350;
            var randomOffset = new Vector2(offsetX, 0);
            return randomOffset;
        }
    }
}
