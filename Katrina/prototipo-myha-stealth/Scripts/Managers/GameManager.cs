using Godot;
using PrototipoMyha.Utilidades;
using System;

namespace PrototipoMyha.Scripts.Managers
{
    public partial class GameManager : Node
    {


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
        }

        private void OnEnemyKillMyha()
        {
            LoadGame();
        }

        public void SaveGame()
        {

            var saveData = new SaveSystem.SaveData
            {
                PlayerPosition_X = PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition().X,
                PlayerPosition_Y = PlayerManager.GetPlayerGlobalInstance().GetPlayerPosition().Y,
            };

            SaveSystem.SaveSystemInstance.SaveGame(saveData);
        }

        public void LoadGame()
        {
            var saveData = SaveSystem.SaveSystemInstance.LoadGame();

            if (saveData != null)
            {
                Vector2 loadedPosition = new Vector2(saveData.PlayerPosition_X, saveData.PlayerPosition_Y);
                SignalManager.Instance.EmitSignal(nameof(SignalManager.GameLoaded), loadedPosition);
                PlayerManager.GetPlayerGlobalInstance().UpdatePlayerPosition(loadedPosition);
            }
        }
    }


}
