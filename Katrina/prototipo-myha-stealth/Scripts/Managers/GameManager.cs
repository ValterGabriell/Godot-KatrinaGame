using Godot;
using PrototipoMyha.Enemy;
using PrototipoMyha.Scripts.Utils.Objetos;
using PrototipoMyha.Utilidades;
using System.Collections.Generic;
using System.Linq;
using static Godot.Control;

namespace PrototipoMyha.Scripts.Managers
{
    public partial class GameManager : Node
    {
        private static GameManager _instance;
        private CanvasLayer _fadeLayer;
        private ColorRect _fadeRect;
        private int TIME_TO_LOAD_GAME = TIME_TO_LOAD_GAME_CONST;
        private const  int TIME_TO_LOAD_GAME_CONST = 150;
        private bool playerHasBeenKilled = false;

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

           
            _fadeLayer = new CanvasLayer();
            AddChild(_fadeLayer);

  
            _fadeRect = new ColorRect
            {
                Color = new Color(0, 0, 0, 0), // Preto transparente
                AnchorLeft = 0,
                AnchorTop = 0,
                AnchorRight = 1,
                AnchorBottom = 1,
                Position = Vector2.Zero,
                Size = Vector2.Zero,
                MouseFilter = MouseFilterEnum.Ignore
            };
            _fadeLayer.AddChild(_fadeRect);
        }

        public override void _Process(double delta)
        {
            if(playerHasBeenKilled)
            {
                FadeScreen();
                TIME_TO_LOAD_GAME -= 1;
                if (TIME_TO_LOAD_GAME == 0)
                {
                    LoadGame();
                }
            }
        }

        private void FadeScreen()
        {
            var tween = CreateTween();
            tween.TweenProperty(_fadeRect, "color:a", 1.0f, 1.0f); // Fade para preto em 1 segundo
        }

        public static GameManager GetGameManagerInstance()
        {
            return _instance;
        }

        public void SetCurrentLevelInitialData(int levelNumber, List<EnemyBase> enemies, Vector2 PlayerPosition)
        {
            
            CurrentLevel = new LevelSaveData
            {
                LevelNumber = levelNumber,
                Enemies = enemies.Select(e => e.ToSaveData()).ToList(),
                PlayerPosition_X_OnLevel = PlayerPosition.X,
                PlayerPosition_Y_OnLevel = PlayerPosition.Y
            };
        }


        private void OnEnemyKillMyha()
        {
            playerHasBeenKilled = true;
            SetGameSpeed(0.1f);
        }

        public void SetGameSpeed(float timeScale)
        {
            Engine.TimeScale = timeScale;
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
            playerHasBeenKilled = false;
            TIME_TO_LOAD_GAME = TIME_TO_LOAD_GAME_CONST;
            SetGameSpeed(1.0f);

            var tween = CreateTween();
            tween.TweenProperty(_fadeRect, "color:a", 0.0f, 1.0f);

            var saveData = SaveSystem.SaveSystemInstance.LoadGame();
            if (saveData != null)
            {
                Vector2 loadedPosition = new Vector2(saveData.PlayerPosition_X_OnLevel, saveData.PlayerPosition_Y_OnLevel);
                SignalManager.Instance.EmitSignal(nameof(SignalManager.GameLoaded), loadedPosition);
                var instanceManager = PlayerManager.GetPlayerGlobalInstance();
                instanceManager.UpdatePlayerPosition(loadedPosition);
                instanceManager.BasePlayer.UnblockMovement();

                var enemiesInScene = GetTree().GetNodesInGroup("enemy"); 

                foreach (var enemySave in saveData.Enemies)
                {
                    // Encontre o inimigo correspondente pelo Id ou outro identificador
                    var enemy = enemiesInScene
                        .OfType<EnemyBase>()
                        .FirstOrDefault(e => e.GetInstanceId() == enemySave.InstanceID); 

                    if (enemy != null)
                    {
                        enemy.GlobalPosition = new Vector2(enemySave.PositionX, enemySave.PositionY);
                        enemy.SetState(enemySave.EnemyState);
                        enemy.JustLoaded = true;
                    }
                }
            }

          
        }
    }


}
