using Godot;
using PrototipoMyha.Utilidades;
using System.Text.Json;
// NÃO USE: using System.IO;

public partial class SaveSystem : Node
{
    private const string SAVE_PATH = "user://savegame.json";

    public static SaveSystem SaveSystemInstance = null;
    public class SaveData
    {
        public float PlayerPosition_X { get; set; }
        public float PlayerPosition_Y { get; set; }
    }

    public override void _Ready()
    {
        if (SaveSystemInstance == null)
        {
            SaveSystemInstance = this;
        }
        else
        {
            QueueFree();
        }
    }

    public void SaveGame(SaveData data)
    {
        GDLogger.PrintObject(data);
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        using var file = Godot.FileAccess.Open(SAVE_PATH, Godot.FileAccess.ModeFlags.Write);

        if (file != null)
        {
            GDLogger.PrintObject(json);
            file.StoreString(json);
            GD.Print("Jogo salvo com sucesso!");
        }
        else
        {
            GD.PrintErr("Erro ao salvar o jogo!");
        }
    }

    public SaveData LoadGame()
    {
        if (!Godot.FileAccess.FileExists(SAVE_PATH))
        {
            GD.Print("Arquivo de save não encontrado!");
            return null;
        }

        using var file = Godot.FileAccess.Open(SAVE_PATH, Godot.FileAccess.ModeFlags.Read);

        if (file != null)
        {
            string json = file.GetAsText();
            SaveData data = JsonSerializer.Deserialize<SaveData>(json);
            
            return data;
        }

        GD.PrintErr("Erro ao carregar o jogo!");
        return null;
    }

    public bool SaveExists()
    {
        return Godot.FileAccess.FileExists(SAVE_PATH);
    }

    public void DeleteSave()
    {
        if (Godot.FileAccess.FileExists(SAVE_PATH))
        {
            Godot.DirAccess.RemoveAbsolute(SAVE_PATH);
            GD.Print("Save deletado!");
        }
    }
}
