using Godot;
using KatrinaGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Utilidades
{
    public class RaycastUtils
    {
        public static void FlipRaycast(float direction, List<RayCast2D> Rasycast)
        {
            foreach (var current in Rasycast)
            {
                current.TargetPosition = new Vector2(direction * Mathf.Abs(current.TargetPosition.X), current.TargetPosition.Y);
            }
        }

        public static (T, bool) IsColliding<T>(RayCast2D rayCast2D)
        {
            if(rayCast2D.IsColliding() && rayCast2D.GetCollider() is T t)
            {
                return (t, true);
            }
            return (default, false);
        }
    }
}
