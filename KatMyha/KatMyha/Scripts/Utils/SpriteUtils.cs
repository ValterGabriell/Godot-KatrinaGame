using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Scripts.Utils
{
    public static class SpriteUtils
    {
        public static void FlipSprite(float direction, AnimatedSprite2D animatedSprite2D)
        {
            if (Mathf.Abs(direction) > 0.1f)
            {
                animatedSprite2D.FlipH = direction < 0;
            }
        }
    }
}
