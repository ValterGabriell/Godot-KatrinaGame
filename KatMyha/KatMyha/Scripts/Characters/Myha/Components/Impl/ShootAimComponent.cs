using Godot;
using KatrinaGame.Core;
using KatrinaGame.Players;
using PrototipoMyha;
using PrototipoMyha.Player.Components.Interfaces;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<RigidBody2D> allLightsOnRange = [];
        public void HandleInput(double delta)
        {
            if (Input.IsActionJustPressed("aim"))
            {
                this._player.SetState(PlayerState.AIMING);
                SignalManager.EmitSignal(nameof(SignalManager.PlayerAim));
            }

            if (Input.IsActionJustReleased("aim"))
            {
                this._player.SetState(PlayerState.IDLE);
                SignalManager.EmitSignal(nameof(SignalManager.PlayerRemoveAim));
            }

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
        }

        private void OnPlayerRemoveAim()
        {
           
        }

        private void OnPlayerAim()
        {
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
                GDLogger.PrintPlayerActions_Yellow("Aiming Light: " + this.currentAimLight.Name);
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
