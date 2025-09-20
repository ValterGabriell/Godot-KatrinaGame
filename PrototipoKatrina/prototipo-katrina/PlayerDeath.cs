using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoKatrina
{
    public sealed partial class Katrina
    {
        public override void _Ready()
        {
            this.LastSavePointPlayerPosition = this.Position;
        }

        public void _on_death_collision_body_entered(Node2D node2D)
        {
            if (node2D.IsInGroup(EnumGroups.death_water_area.ToString()))
            {
                GD.Print("Player morreu");
                this.Position = this.LastSavePointPlayerPosition;
            }
        }
    }
}
