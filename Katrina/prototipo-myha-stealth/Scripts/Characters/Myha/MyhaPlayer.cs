using Godot;
using KatrinaGame.Components;
using KatrinaGame.Core;
using KatrinaGame.Core.Interfaces;
using PrototipoMyha;
using PrototipoMyha.Player.Components.Impl;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Scripts.Characters.Myha.Components.Impl;
using System;

namespace KatrinaGame.Players
{
    public partial class MyhaPlayer : BasePlayer
    {
        [Export] public RayCast2D AttackRaycast { get; set; }
        [Export] public RayCast2D PushRaycast { get; set; }
        [Export] public PackedScene BallScene { get; set; }
        [Export] public Area2D SoundAreaWalkingComponent { get; set; }
        private CircleShape2D SoundAreaWalkingColiisonComponent { get; set; }

        private IMovementComponent MovementComponent;
        private float CurrentPlayerSpeed = 0f;



        protected override void InstanciateComponents()
        {
            AddComponent<IMovementComponent>(new MovementComponent());
            AddComponent<IMakeSoundWhileWalkComponent>(new MakeSoundWhileWalkComponent(this));
            AddComponent<IAnimationComponents>(new AnimationComponents(this));


            MovementComponent = GetComponent<IMovementComponent>();
            SubscribeSignals();
            SoundAreaWalkingColiisonComponent = SoundAreaWalkingComponent
            .GetNode<CollisionShape2D>("CollisionShape2D")
            .Shape as CircleShape2D;
        }

        private void SubscribeSignals()
        {
            SignalManager.Instance.PlayerStateChanged += PlayerStateChanged;
            SignalManager.Instance.GameLoaded += UpdatePlayerPosition;
   

        }



        private void UpdatePlayerPosition(Vector2 position)
        {
            this.GlobalPosition = position;
        }

        public void AlterRadiusCollisionSoundArea(float newRadius)
        {
            SoundAreaWalkingColiisonComponent.Radius = newRadius;
        }

        private void PlayerStateChanged(PlayerState NewState)
        {
            this.SetState(NewState);
        }


        protected override void HandleInput(double delta)
        {
            // Input de movimento
            Vector2 inputVector = Vector2.Zero;
            if(inputVector == Vector2.Zero) CurrentPlayerSpeed = 0f;


            if (Input.IsActionPressed("d") && this.IsMovementBlocked == false)
            {
                inputVector.X += 1;
                FlipRaycast(direction: 1,
                [
                    AttackRaycast,
                    PushRaycast
                ]);
                CurrentPlayerSpeed = Speed ;
            }

            if (Input.IsActionPressed("a") && this.IsMovementBlocked == false)
            {
                inputVector.X -= 1;
                FlipRaycast(direction: -1,
               [
                   AttackRaycast,
                    PushRaycast
               ]);
                CurrentPlayerSpeed = Speed;
            }

            if (Input.IsActionJustPressed("jump"))
            {
                inputVector.Y -= 1;
                MovementComponent.Jump();
            }

            if (Input.IsKeyPressed(Key.Ctrl))
            {
               
                CurrentPlayerSpeed = SneakSpeed;
            }


            MovementComponent.Move(inputVector, CurrentPlayerSpeed);

            base.HandleInput(delta);
        }

    }
}