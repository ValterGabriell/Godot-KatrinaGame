using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha.Enemy.Enemies.Guard
{
    public partial class GuardResource : Resource
    {
        [ExportGroup("Base")]
        [Export] public float Health = 100;

        [Export] public int DamageAmount = 10;

        [Export] public float MoveSpeed = 900f;

        [Export] public float ChaseSpeed = 100f;
        [Export] public float ForcePushDamage = 200f;
    }
}
