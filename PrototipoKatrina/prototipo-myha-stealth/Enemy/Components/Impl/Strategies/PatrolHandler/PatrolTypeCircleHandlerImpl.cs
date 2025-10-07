using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Components.Impl.Strategies.PatrolHandler
{
    public class PatrolTypeCircleHandlerImpl : IPatrolTypeHandler
    {
        public Vector2 GetPatrolTarget(float InPatrolRadius, Random InRandomInstance)
        {
            // Gera um ponto aleatório dentro do raio de patrulhamento
            float angle = (float)(InRandomInstance.NextDouble() * 2 * Math.PI);
            float distance = (float)(InRandomInstance.NextDouble() * InPatrolRadius);

            Vector2 randomOffset = new Vector2(
                Mathf.Cos(angle) * distance,
                Mathf.Sin(angle) * distance
            );
            return randomOffset;
        }
    }
}
