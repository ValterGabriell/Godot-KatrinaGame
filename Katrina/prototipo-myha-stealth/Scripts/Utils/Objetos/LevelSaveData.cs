using PrototipoMyha.Enemy;
using System.Collections.Generic;

namespace PrototipoMyha.Scripts.Utils.Objetos
{
    public class LevelSaveData
    {
        public int LevelNumber { get; set; }
        public float PlayerPosition_X_OnLevel { get; set; }
        public float PlayerPosition_Y_OnLevel { get; set; }
        public List<EnemyBase> Enemies { get; set; }
    }
}
