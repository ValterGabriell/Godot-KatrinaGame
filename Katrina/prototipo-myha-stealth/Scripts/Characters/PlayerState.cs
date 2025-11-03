using System;

namespace PrototipoMyha.Player.StateManager;

public enum PlayerState
{
    IDLE,
    WALK,
    RUN,
    ATTACK,
    THROW,
    HURT,
    DEAD,
    SNEAK,
    HIDDEN,
    APPEAR,
    JUMPING,
    WALL_WALK_STOP,
    FALLING_FOR_DEATH
}
