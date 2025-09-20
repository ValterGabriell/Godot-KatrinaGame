using Godot;
using PrototipoKatrina;
using System;

public partial class SteamObstacle : Node2D
{
    [Export] private RayCast2D RaycastOfSteamToDetectPlayer;
    [Export] private Timer Timer;
    private float SizeOfSteamArea = 0.0f;
    private const int MAX_SIZE_OF_STEAM_AREA = 100;
    private int CurrentDirectionOfSteam = 1; // 1 para cima, -1 para baixo


    public override void _Process(double delta)
    {
        if(SizeOfSteamArea <= MAX_SIZE_OF_STEAM_AREA)
        {
            this.RaycastOfSteamToDetectPlayer.TargetPosition += new Vector2(0, CurrentDirectionOfSteam * -2);
            SizeOfSteamArea += 2;
        }

        if(SizeOfSteamArea >= MAX_SIZE_OF_STEAM_AREA)
            Timer.Start();



        if (RaycastOfSteamToDetectPlayer.IsColliding())
        {
            var collider = RaycastOfSteamToDetectPlayer.GetCollider();
            if (collider is Katrina player)
            {
                player.MovePlayerToFallPosition(new Vector2(35, 35));
            }
        }
    }


    public void _on_timer_timeout()
    {
        GD.Print("Timer timeout reached, resetting steam area");
        CurrentDirectionOfSteam = CurrentDirectionOfSteam * -1;
        SizeOfSteamArea = 0;
    }
}
