using Godot;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Scripts.Utils
{
    public static class PolyngUtils
    {
        public static void Flip(float direction, Polygon2D polygon2D)
        {
            polygon2D.Scale = new Vector2(direction > 0 ? 1 : -1, 1);
            polygon2D.RotationDegrees = 0;
        }
    }
}
