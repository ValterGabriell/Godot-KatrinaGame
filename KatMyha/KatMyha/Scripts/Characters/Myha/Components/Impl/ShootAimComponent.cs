using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Utilidades;
using System.Collections.Generic;
using System.Linq;

namespace KatMyha.Scripts.Characters.Myha.Components.Impl
{
    public interface IShootAimComponent : IPlayerBaseComponent
    {
    }
    public partial class ShootAimComponent : Node, IShootAimComponent
    {
        private MyhaPlayer _player;
        public SignalManager SignalManager { get; private set; } = SignalManager.Instance;

        private RigidBody2D currentAimLight = null;
        private RigidBody2D lastAimLightShooted = null;
        private List<RigidBody2D> allLightsOnRange = [];
        public void HandleInput(double delta)
        {
            if (Input.IsActionJustPressed("aim")) SignalManager.EmitSignal(nameof(SignalManager.PlayerAim));
            if (Input.IsActionJustReleased("aim")) SignalManager.EmitSignal(nameof(SignalManager.PlayerRemoveAim));
            if (Input.IsActionJustPressed("shoot")) SignalManager.EmitSignal(nameof(SignalManager.PlayerShoot));

        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent)
            {
                if (mouseEvent.ButtonIndex == MouseButton.WheelUp 
                    && mouseEvent.Pressed 
                    && this.currentAimLight != null)
                {
                    SwitchToNextLight();
                }
                else if (mouseEvent.ButtonIndex == MouseButton.WheelDown 
                    && mouseEvent.Pressed 
                    && this.currentAimLight != null)
                {
                    SwitchToNextLight();
                }
            }
        }

        private void ChangeAimLightToAim()
        {
            this.currentAimLight = this.allLightsOnRange[
                   (this.allLightsOnRange.IndexOf(this.currentAimLight) + 1) % this.allLightsOnRange.Count
               ];
            if (this.currentAimLight != null)
            {
                this.currentAimLight.GetNode<Sprite2D>("Sprite2D").Texture = 
                    GD.Load<Texture2D>("res://Assets/Sprites/Itens/LampadaMirada.png");
            }
        }



        private void SwitchToNextLight()
        {
            if (this.allLightsOnRange.Count > 0)
            {
                ChangeAimLightToAim();
                ResetOtherLightsTexture();
            }
        }

        private void ResetOtherLightsTexture()
        {
            this.allLightsOnRange.Where(e => e.Name != this.currentAimLight.Name).Select(e =>
            {
                e.GetNode<Sprite2D>("Sprite2D").Texture =
                    GD.Load<Texture2D>("res://Assets/Sprites/Itens/Lampada.png");
                return e;
            }).ToList();
        }

        private void BackToPreviusLight()
        {
            if (this.allLightsOnRange.Count > 0)
            {
                this.currentAimLight = this.allLightsOnRange[
                    (this.allLightsOnRange.IndexOf(this.currentAimLight) + 1) % this.allLightsOnRange.Count
                ];
                this.allLightsOnRange.Where(e => e.Name != this.currentAimLight.Name).Select(e =>
                {
                    e.GetNode<Sprite2D>("Sprite2D").Texture =
                        GD.Load<Texture2D>("res://Assets/Sprites/Itens/Lampada.png");
                    return e;
                }).ToList();
                ChangeAimLightToAim();
            }
        }

        public void Initialize(BasePlayer player)
        {
            _player = player as MyhaPlayer;
            SignalManager.Instance.PlayerAim += OnPlayerAim;
            SignalManager.Instance.PlayerRemoveAim += OnPlayerRemoveAim;
            SignalManager.Instance.PlayerShoot += OnPlayerShoot;
        }

        private void OnPlayerShoot()
        {

            GDLogger.PrintPlayerActions_Yellow("OnPlayerStopShooting");
            GDLogger.PrintPlayerActions_Yellow(this._player.CurrentPlayerState);
            if (this._player.CurrentPlayerState == PlayerState.AIMING)
            {
                this._player.SetState(PlayerState.SHOOTING);
                this.currentAimLight.GetNode<Sprite2D>("Sprite2D").Texture =
                    GD.Load<Texture2D>("res://Assets/Sprites/Itens/LampadaQuebrada.png");
                this.currentAimLight.GravityScale = 1;

                this.lastAimLightShooted = this.currentAimLight;
                this.allLightsOnRange.Remove(this.currentAimLight);
                var timer = GetTree().CreateTimer(3.0);
                timer.Timeout += () => this.lastAimLightShooted?.QueueFree();
                this.lastAimLightShooted = null;

                if (this.allLightsOnRange.Count > 0)
                {
                    this._player.SetState(PlayerState.AIMING);
                    BackToPreviusLight();
                }
                else
                {
                    this.currentAimLight = null;
                    this._player.SetState(PlayerState.IDLE);
                }
            }
        }

      
        private void OnPlayerRemoveAim()
        {
            this.allLightsOnRange.Select(e =>
            {
                e.GetNode<Sprite2D>("Sprite2D").Texture =
                    GD.Load<Texture2D>("res://Assets/Sprites/Itens/Lampada.png");
                return e;
            }).ToList();
            this.currentAimLight = null;
            this._player.SetState(PlayerState.IDLE);

        }

        private void OnPlayerAim()
        {
            this._player.SetState(PlayerState.AIMING);
            var allLights = GetTree().GetNodesInGroup("kill_light");
            var playerPos = _player.GlobalPosition;
            float nearestDist = 300f;

            foreach (var node in allLights)
            {
                if (node is not RigidBody2D light)
                    continue;

             
                float dist = light.GlobalPosition.DistanceTo(playerPos);
                if (dist < nearestDist)
                {
                    this.allLightsOnRange.Add(light);
                }
            }

            if (this.allLightsOnRange.Count > 0)
            {
                this.currentAimLight = this.allLightsOnRange[0];
                ChangeAimLightToAim();
            }
        }




        public void PhysicsProcess(double delta)
        {
            
        }

        public void Process(double delta)
        {
            
        }
    }
}
