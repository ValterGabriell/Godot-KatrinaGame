using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Interfaces;
using PrototipoMyha.Scripts.Utils;
using PrototipoMyha.Utilidades;
using System;
using static PrototipoMyha.SignalManager;

namespace PrototipoMyha.Scripts.Enemies.BaseGuard.Components.Impl
{
    public partial class EnemyAnimationComponent : Node, IEnemyBaseComponents
    {
        private EnemyBase _Enemy;
        private bool IsWalkingToAlert = false;
        private SignalManager SignalManager;
        private PackedScene spriteScene;
        private bool _alertShown = false;
        private AnimatedSprite2D _currentAlertSprite;
        private Timer _currentAlertTimer;
        public EnemyAnimationComponent(EnemyBase enemy)
        {
            _Enemy = enemy;
        }

        public void Initialize()
        {
            spriteScene = (PackedScene)GD.Load("res://Scenes/Items/Itens/ItemAlertSound.tscn");

            this.SignalManager = SignalManager.Instance;
            this.SignalManager.EnemySpottedPlayer += OnEnemySpottedPlayer;
            this.SignalManager.EnemySpottedPlayerShowAlert += OnEnemySpottedPlayerShowAlert;

        }


        private void OnEnemySpottedPlayerShowAlert(Vector2 positionToShowAlert)
        {
            if (_alertShown)
                return; 

            _alertShown = true;
            
            AnimatedSprite2D sprite = (AnimatedSprite2D)spriteScene.Instantiate();
            sprite.Position = positionToShowAlert;
            sprite.AddToGroup(EnumGroups.AlertSprite.ToString());
            AddChild(sprite);

            if (!sprite.IsPlaying())
                sprite.Play("default");

            Timer timer = new Timer();
            timer.WaitTime = 2.5f;
            timer.OneShot = true;
            AddChild(timer);

            timer.Timeout += () =>
            {
                if (IsInstanceValid(sprite))
                {
                    sprite.QueueFree();
                
                }
                timer.QueueFree();
                _alertShown = false;
            };

            timer.Start();
        }

        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {

            if (this._Enemy.CurrentEnemyState != Enemy.States.EnemyState.Alerted)
                IsWalkingToAlert = false;

            var currentAnimation = this._Enemy.CurrentEnemyState switch
            {
                Enemy.States.EnemyState.Roaming => EnumGuardMove.roaming.ToString(),
                Enemy.States.EnemyState.Alerted => EnumGuardMove.start_warning.ToString(),
                Enemy.States.EnemyState.Waiting => EnumGuardMove.end_warning.ToString(),
                Enemy.States.EnemyState.Investigating => EnumGuardMove.end_warning.ToString(),
                Enemy.States.EnemyState.Chasing => EnumGuardMove.shoot.ToString(),
                _ => EnumGuardMove.roaming.ToString(),
            };

            if (!IsWalkingToAlert)
                this._Enemy.AnimatedSprite2DEnemy.Play(currentAnimation);
            else
                this._Enemy.AnimatedSprite2DEnemy.Play(EnumGuardMove.walk_alert.ToString());
        }

        private void OnEnemySpottedPlayer()
        {
            IsWalkingToAlert = true;

        }
    }
}
