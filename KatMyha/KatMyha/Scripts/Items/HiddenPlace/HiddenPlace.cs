using Godot;
using KatrinaGame.Players;

public partial class HiddenPlace : Area2D
{
    public void _on_body_entered(Node2D body)
    {
        if (body is MyhaPlayer player)
        {
            player.EnterHiddenPlace();
        }
    }
    
}
