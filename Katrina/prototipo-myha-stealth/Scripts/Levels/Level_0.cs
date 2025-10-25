using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Scripts.Managers;
using PrototipoMyha.Utilidades;
using System;
using System.Linq;

public partial class Level_0 : Node2D
{
    public override void _Ready()
    {
        var enemiesInScene = GetTree().GetNodesInGroup("enemy");
        var data = PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition();
        GDLogger.PrintInfo(data); 
        GameManager.GetGameManagerInstance().SetCurrentLevelInitialData(
            levelNumber: 0,
            enemies: enemiesInScene.OfType<EnemyBase>().ToList(),
            PlayerPosition: data
        );
    }
}
