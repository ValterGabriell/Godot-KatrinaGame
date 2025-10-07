using Godot;
using KatrinaGame.Components;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;

namespace KatrinaGame.Players
{
    public partial class MyhaPlayer : BasePlayer
    {
        [Export] public RayCast2D AttackRaycast { get; set; }
        [Export] public RayCast2D PushRaycast { get; set; }
        [Export] public PackedScene BallScene { get; set; }

        


        protected override void InitializeComponents()
        {
            // Componentes básicos
            AddComponent<IHealthComponent>(new HealthComponent());
            AddComponent<IMovementComponent>(new MovementComponent());

            var attackComponent = new AttackComponent();
            attackComponent.AttackRaycast = AttackRaycast;
            AddComponent<IAttackComponent>(attackComponent);

            // Adicionar como filhos para que funcionem no Godot
            AddChild(GetComponent<IHealthComponent>() as Node);
            AddChild(GetComponent<IMovementComponent>() as Node);
            AddChild(GetComponent<IAttackComponent>() as Node);
        }

        protected override void HandleInput(double delta)
        {
            if (!HealthComponent.IsAlive) return;

            // Input de ataque
            if (Input.IsActionJustPressed("attack"))
            {
                AttackComponent.Attack();
            }



            // Input de movimento
            Vector2 inputVector = Vector2.Zero;

            if (Input.IsActionPressed("d") && MovementComponent.IsMovementBlocked == false)
            {
                inputVector.X += 1;
                FlipRaycast(direction: 1,
                [
                    AttackRaycast,
                    PushRaycast
                ]);
            }

            if (Input.IsActionPressed("a") && MovementComponent.IsMovementBlocked == false)
            {
                inputVector.X -= 1;
                FlipRaycast(direction: -1,
               [
                   AttackRaycast,
                    PushRaycast
               ]);
            }

            if (Input.IsActionJustPressed("jump"))
            {
                MovementComponent.Jump();
            }

            bool isRunning = Input.IsActionPressed("run");
            MovementComponent.Move(inputVector, isRunning);

            base.HandleInput(delta);
        }

    }
}