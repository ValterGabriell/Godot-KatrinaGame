using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Resources.Player
{
    public partial class PlayerResource : Resource
    {
        [Export] public float Health = 100f;
        [Export] public float Stamina = 100f;
        [Export] public float MaxHealth = 100f;
        [Export] public float MaxStamina = 100f;
      
    }
}
