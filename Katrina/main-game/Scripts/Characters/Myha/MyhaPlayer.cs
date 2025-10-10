using Godot;
using KatrinaGame.Components;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using PrototipoMyha;
using PrototipoMyha.Player.Components.Impl;
using PrototipoMyha.Player.StateManager;
using System;

namespace KatrinaGame.Players
{
    public partial class MyhaPlayer : BasePlayer
    {
        [Export] public RayCast2D AttackRaycast { get; set; }
        [Export] public RayCast2D PushRaycast { get; set; }
        [Export] public PackedScene BallScene { get; set; }
        [Export] public Area2D SoundAreaWalkingComponent { get; set; }
        [Export] public CollisionShape2D SoundAreaWalkingColiisonComponent { get; set; }

        private IMovementComponent MovementComponent;


        protected override void InstanciateComponents()
        {
            AddComponent<IMovementComponent>(new MovementComponent());
            AddComponent<IMakeSoundWhileWalkComponent>(new MakeSoundWhileWalkComponent(this));


            MovementComponent = GetComponent<IMovementComponent>();
            SignalManager.Instance.PlayerStateChanged += PlayerStateChanged;
        }

        private void PlayerStateChanged(PlayerState NewState)
        {
            this.SetState(NewState);
        }

        protected override void HandleInput(double delta)
        {
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