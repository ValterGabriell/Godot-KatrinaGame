
using Godot;

namespace PrototipoMyha;

public static class AttackUtils
{
    public static void PushWhenReceiveDamage(Vector2 force, CharacterBody2D character)
    {
        character.Velocity += force;
    }




}
