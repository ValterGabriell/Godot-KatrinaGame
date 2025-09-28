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
        public void ApplyDamage(float damageAmount)
        {
            GD.Print("Player took damage: " + damageAmount);
        }
    }
}
