using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Enemy.Components.Impl.EnemyMovement.Strategies.PatrolHandler;
using PrototipoMyha.Enemy.States;
using System.Collections.Generic;

namespace PrototipoMyha.Scripts.Utils.Objetos
{
    public class LevelSaveData
    {
        public int LevelNumber { get; set; }
        public float PlayerPosition_X_OnLevel { get; set; }
        public float PlayerPosition_Y_OnLevel { get; set; }
        public List<EnemySaveData> Enemies { get; set; }
    }

    public class EnemySaveData
    {
        public ulong InstanceID { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public EnemyState EnemyState { get; set; }
    }
}
