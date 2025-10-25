using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Scripts.Utils.Objetos;
using PrototipoMyha.Utilidades;
using System.Collections.Generic;
using System.Linq;

namespace PrototipoMyha.Scripts.Managers
{
    public partial class GameManager : Node
    {
        private static GameManager _instance;
        public LevelSaveData CurrentLevel { get; private set; }

        // Salvar quando o jogador apertar uma tecla
        public override void _Input(InputEvent @event)
        {
            // F5 para salvar
            if (@event.IsActionPressed("save_game"))
            {
                SaveGame();
            }

            // F9 para carregar
            if (@event.IsActionPressed("load_game"))
            {
                LoadGame();
            }
        }

        public override void _Ready()
        {
            SignalManager.Instance.EnemyKillMyha += OnEnemyKillMyha;
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                QueueFree();
            }
        }

        public static GameManager GetGameManagerInstance()
        {
            return _instance;
        }

        public void SetCurrentLevelInitialData(int levelNumber, List<EnemyBase> enemies, Vector2 PlayerPosition)
        {
            GDLogger.PrintInfo($"Level Number: {levelNumber}, Player Position: {PlayerPosition}, Enemies Count: {enemies.Count}");
            CurrentLevel = new LevelSaveData
            {
                LevelNumber = levelNumber,
                Enemies = enemies,
                PlayerPosition_X_OnLevel = PlayerPosition.X,
                PlayerPosition_Y_OnLevel = PlayerPosition.Y
            };
        }


        private void OnEnemyKillMyha()
        {
            LoadGame();
        }

        public void SaveGame()
        {
            var saveData = new LevelSaveData()
            {
                LevelNumber = this.CurrentLevel.LevelNumber,
                PlayerPosition_X_OnLevel = CurrentLevel.PlayerPosition_X_OnLevel,
                PlayerPosition_Y_OnLevel = CurrentLevel.PlayerPosition_Y_OnLevel,
                Enemies = this.CurrentLevel.Enemies
            };

            SaveSystem.SaveSystemInstance.SaveGame(saveData);
        }

        public void LoadGame()
        {
            var saveData = SaveSystem.SaveSystemInstance.LoadGame();
            if (saveData != null)
            {
                Vector2 loadedPosition = new Vector2(saveData.PlayerPosition_X_OnLevel, saveData.PlayerPosition_Y_OnLevel);
                SignalManager.Instance.EmitSignal(nameof(SignalManager.GameLoaded), loadedPosition);
                PlayerManager.GetPlayerGlobalInstance().UpdatePlayerPosition(loadedPosition);

                var enemiesInScene = GetTree().GetNodesInGroup("enemy"); 

                foreach (var enemySave in saveData.Enemies)
                {
                    // Encontre o inimigo correspondente pelo Id ou outro identificador
                    var enemy = enemiesInScene
                        .OfType<EnemyBase>()
                        .FirstOrDefault(e => e.GetInstanceId() == enemySave.GetInstanceId()); 

                    if (enemy != null)
                    {
                        enemy.GlobalPosition = new Vector2(enemySave.Position.X, enemySave.Position.Y);
                    }
                }
            }
        }
    }


}
