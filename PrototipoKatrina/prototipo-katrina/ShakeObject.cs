using Godot;
using PrototipoKatrina;
using System;
using System.Diagnostics.Contracts;

public partial class ShakeObject : StaticBody2D
{
    [Export] public Area2D AreaDetectPlayer;
    [Export] public CollisionShape2D CollisionRotatePlayer;
    private float RotationSpeed = 5.0f;
    private bool isPlayerOnObject = false;
    private Katrina player;
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
        player.SetPlayerMovementBlocked(bloqueado: false);
        player.BalanceZero -= OnPlayerBalanceZero;
        player.ResetBallance();
    }

    private static bool IsPlayerBody(Node2D body)
    {
        return body.IsInGroup(EnumGroups.player.ToString());
    }
    private void SetPlayerOnObject(Node2D body)
    {
        shouldResetRotation = false;
        isPlayerFalling = false;
        isPlayerOnObject = true;
        player = body as Katrina;
        player.SetPlayerMovementBlocked(bloqueado: true);
        player.BalanceZero += OnPlayerBalanceZero;
       
    }

    private void OnPlayerBalanceZero()
    {
        player.GlobalPosition = this.GlobalPosition + new Vector2(150, -130);
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
        GD.Print("RotationDegrees: " + RotationDegrees);
        if (Mathf.Abs(RotationDegrees) < 0.1f)
            RotationDegrees = 0;
    }

    private void MoveObjectWithPlayerInput(double delta, EnumMove currentPlayerMovement)
    {
        float moveSpeed = 10f; 
        if (Input.IsActionPressed("ui_right"))
            ApplyRightMovement(delta, moveSpeed);

        else if (Input.IsActionPressed("ui_left"))
            ApplyLeftMovement(delta, moveSpeed);
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
