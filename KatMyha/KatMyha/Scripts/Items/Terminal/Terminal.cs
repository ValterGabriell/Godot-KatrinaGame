using Godot;

public partial class Terminal : Node2D
{
    private Area2D SaveArea => GetNode<Area2D>("SaveArea");

    public void _on_save_area_body_entered(Node2D body)
    {
        var instance = PlayerManager.GetPlayerGlobalInstance();
        instance.PlayerCanSaveTheGame = true;
        instance.UpdatePlayerPosition(this.GlobalPosition);
    }

    public void _on_save_area_body_exited(Node2D body)
    {
        PlayerManager.GetPlayerGlobalInstance().PlayerCanSaveTheGame = false;
    }


}
