using Godot;
using PrototipoMyha;
using PrototipoMyha.Player.StateManager;
using System;

public partial class ShadowArea : Area2D
{
    [Export] private CollisionShape2D ShadowCollisionShape2D;
    [Export] private Vector2 CollisionShadowScale;




    public override void _Ready()
    {
        ShadowCollisionShape2D.Scale = CollisionShadowScale;
    }

    public void _on_body_entered(Node2D node2D)
    {
        if (node2D.IsInGroup(EnumGroups.player.ToString()))
        {
            SignalManager.Instance.EmitSignal(nameof(SignalManager.PlayerStateChanged), (int)PlayerState.HIDDEN);
        }
    }

    public void _on_body_exited(Node2D node2D)
    {
        if (node2D.IsInGroup(EnumGroups.player.ToString()))
        {
            SignalManager.Instance.EmitSignal(nameof(SignalManager.PlayerStateChanged), (int)PlayerState.APPEAR);
        }
    }
}
