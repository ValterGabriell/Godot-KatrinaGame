using Godot;
using PrototipoMyha;
using PrototipoMyha.Player.StateManager;
using System;

public partial class ShadowArea : Area2D
{
    [Export] private CollisionShape2D ShadowCollisionShape2D;
    [Export] private Vector2 CollisionShadowScale;
    private SignalManager _Instance;



    public override void _Ready()
    {
        ShadowCollisionShape2D.Scale = CollisionShadowScale;
        _Instance = SignalManager.Instance;
    }

    public void _on_body_entered(Node2D node2D)
    {
        if (node2D.IsInGroup(EnumGroups.player.ToString()))
        {
            GD.Print("Player entered shadow area");
            _Instance.EmitSignal(nameof(SignalManager.StateChanged), (int)PlayerState.HIDDEN);
        }
    }

    public void _on_body_exited(Node2D node2D)
    {
        if (node2D.IsInGroup(EnumGroups.player.ToString()))
        {
            GD.Print("Player exited shadow area");
            _Instance.EmitSignal(nameof(SignalManager.StateChanged), (int)PlayerState.APPEAR);
        }
    }
}
