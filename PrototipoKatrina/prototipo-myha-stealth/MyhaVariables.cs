using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha
{
    public sealed partial class Myha_2
    {
        // Use [Export] para definir variáveis no Inspector do Godot.
        // Convenção C# usa PascalCase para nomes de variáveis públicas.
        [Export] public RayCast2D PushRaycast;
        [Export] public PackedScene BallScene;
        [Export] public float Health = 100.0f;
        [Export] public RayCast2D AttackRaycast;
        [Export] public float Speed = 200.0f;
        [Export] public float RunSpeed = 350.0f;
        private float JumpVelocity = -400.0f;
        [Export] public float Gravity = 700.0f;
        [Export] public Sprite2D Sprite;
        private EnumMove CurrentPlayerDirectionMovement;
        private bool IsMovementBlocked = false;
        private Vector2 LastSavePointPlayerPosition = Vector2.Zero;

        private float Attack01Force = 20f;
    }
}
