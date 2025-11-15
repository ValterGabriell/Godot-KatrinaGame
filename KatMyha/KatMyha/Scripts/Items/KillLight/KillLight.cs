using Godot;
using PrototipoMyha.Utilidades;
using System;

public partial class KillLight : RigidBody2D
{
    [Export] private float TextureScale = 1.0f;
    [Export] private float EnergyOfLight = 1.0f;
    public void _on_light_detection_body_entered(Node2D node2D)
    {
        GDLogger.PrintDebug_Red(node2D);
    }
}
