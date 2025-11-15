using Godot;
using PrototipoMyha.Utilidades;
using System;

public partial class SwitchLight : Area2D
{
    public void _on_body_entered(Node2D node2D)
    {
        if (node2D.IsInGroup("player"))
        {
            PlayerManager.GetPlayerGlobalInstance().PlayerCanTurnOfTheLight = PlayerSwitchLightState.CAN_TURN_ON_LIGHT;
        }
    }

    public void _on_body_exited(Node2D node2D)
    {
        if (node2D.IsInGroup("player"))
        {
            PlayerManager.GetPlayerGlobalInstance().PlayerCanTurnOfTheLight = PlayerSwitchLightState.CANT_TOGGLE_LIGHT;
        }
    }
}
