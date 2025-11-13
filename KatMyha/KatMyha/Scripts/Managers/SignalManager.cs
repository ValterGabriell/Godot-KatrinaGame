using Godot;
using PrototipoMyha.Player.StateManager;
using System;
using System.Collections.Generic;

namespace PrototipoMyha;

public partial class SignalManager : Node
{
    [Signal] public delegate void EnemyEnteredWarningAreaEventHandler(Vector2 InPositionToGo);
    [Signal] public delegate void EnemyRatStopLookForPlayerEventHandler(Vector2 InPositionToGo);
    [Signal] public delegate void PlayerStateChangedEventHandler(PlayerState NewState);
    [Signal] public delegate void EnemySpottedPlayerEventHandler();
    [Signal] public delegate void EnemySpottedPlayerShowAlertEventHandler(Vector2 positionToShowAlert);
    [Signal] public delegate void PlayerIsMovingEventHandler(float NoiseValue);
    [Signal] public delegate void PlayerHasChangedStateEventHandler(string animationToPlay);
    [Signal] public delegate void PlayerStopedEventHandler();
    [Signal] public delegate void EnemyKillMyhaEventHandler();
    [Signal] public delegate void PlayerSaveTheGameEventHandler();
    [Signal] public delegate void PlayerAimEventHandler();
    [Signal] public delegate void PlayerRemoveAimEventHandler();
    [Signal] public delegate void PlayerShootEventHandler();


    [Signal] public delegate void GameLoadedEventHandler(Vector2 position);
    



    public static SignalManager Instance { get; private set; }


    public override void _Ready()
    {
        if (Instance == null)
            Instance = this;
        else
            QueueFree(); 
    }


}

/*
 SINAL: EnemyKillMyha | EMISSOR: EnemyStateChaseAlertedBase | PARÂMETROS: Nenhum | Observador: GameManager, EnemyAnimationComponent
 SINAL: PlayerIsMoving | EMISSOR: MovementComponent | PARÂMETROS: Nenhum | Observador: MakeSoundWhileWalking
 
 
 */