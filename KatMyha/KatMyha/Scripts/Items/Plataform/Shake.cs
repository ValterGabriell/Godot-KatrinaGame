using Godot;
using KatrinaGame.Core;
using PrototipoMyha;
using PrototipoMyha.Player.StateManager;
using PrototipoMyha.Utilidades;
using System;

public partial class Shake : StaticBody2D
{
    [Export] public Area2D AreaDetectPlayer;
    [Export] public CollisionShape2D CollisionRotatePlayer;
    private float RotationSpeed = 5.0f;
    private BasePlayer player;
    private bool isPlayerOnObject = false;
    private int direction = 1;
    private bool isPlayerFalling = false;
    private double delta;
    private bool shouldResetRotation = false;


    public override void _Ready()
    {
        AreaDetectPlayer.BodyEntered += OnBodyEntered;
        AreaDetectPlayer.BodyExited += OnBodyExited;

    }

    private void OnBodyExited(Node2D body)
    {
        if (IsPlayerBody(body))
            ResetPlayerState();
    }



    private void OnBodyEntered(Node2D body)
    {
        if (IsPlayerBody(body))
            SetPlayerOnObject(body);
    }

    private void ResetPlayerState()
    {
        isPlayerOnObject = false;
        isPlayerFalling = false;
        shouldResetRotation = true;
        player.UnblockMovement();
        player.BalanceZero -= OnPlayerBalanceZero;
        player.ResetBallance();
    }

    private static bool IsPlayerBody(Node2D body)
    {
        return body.IsInGroup(EnumGroups.player.ToString());
    }
    private void SetPlayerOnObject(Node2D body)
    {
        player = body as BasePlayer;
        shouldResetRotation = false;
        isPlayerFalling = false;
        isPlayerOnObject = true;
        player.BlockMovement();
        player.BalanceZero += OnPlayerBalanceZero;

    }

    private void OnPlayerBalanceZero()
    {
        player.MovePlayerToFallPosition(new Vector2(35, 35));
        ResetPlayerState();
    }



    public override void _PhysicsProcess(double delta)
    {
        if (shouldResetRotation)
            ResetObjectRotation(delta);

        if (player != null)
        {
            var currentPlayerMovement = player.GetCurrentPlayerMovement();
            if (isPlayerOnObject)
                MoveObjectWithPlayerInput(delta, currentPlayerMovement);
        }

    }

    private void ResetObjectRotation(double delta)
    {
        RotationDegrees = Mathf.Lerp(RotationDegrees, 0, 8f * (float)delta);
        if (Mathf.Abs(RotationDegrees) < 0.1f)
            RotationDegrees = 0;
    }

    private void MoveObjectWithPlayerInput(double delta, PlayerState currentPlayerMovement)
    {
        float moveSpeed = 10f;
        if (Input.IsActionPressed("d"))
            ApplyRightMovement(delta, moveSpeed);

        else if (Input.IsActionPressed("a"))
            ApplyLeftMovement(delta, moveSpeed);
        else if (Input.IsKeyPressed(Key.Space))
        {
            player.Velocity = new Vector2(player.Velocity.X, -300);
        }
        else
            player.DecreaseBallance((float)delta);


    }

    private void ApplyLeftMovement(double delta, float moveSpeed)
    {
        ApplyLeftRotation(delta);

        if (!isPlayerFalling)
        {
            this.Position -= new Vector2(moveSpeed * (float)delta, 0);
            player.GlobalPosition = this.GlobalPosition + new Vector2(0, -30);
        }

    }

    private void ApplyRightMovement(double delta, float moveSpeed)
    {
        ApplyRightRotation(delta);

        if (!isPlayerFalling)
        {
            this.Position += new Vector2(moveSpeed * (float)delta, 0);
            player.GlobalPosition = this.GlobalPosition + new Vector2(0, -30);
        }

    }

    private void ApplyLeftRotation(double delta)
    {
        direction = -1;
        this.RotationDegrees += RotationSpeed * direction * (float)delta;
        player.AffectBalance(RotationDegrees * direction * (float)delta);
    }

    private void ApplyRightRotation(double delta)
    {
        direction = 1;
        this.RotationDegrees += RotationSpeed * (float)delta;
        player.AffectBalance(RotationDegrees * direction * (float)delta);
    }
}
