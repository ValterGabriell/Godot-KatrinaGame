using Godot;
using PrototipoMyha;
using System;

public partial class Terminal : Node2D
{
    private Area2D SaveArea => GetNode<Area2D>("SaveArea");
    private AnimatedSprite2D AnimatedSprite2D => GetNode<AnimatedSprite2D>("Sprite2D");
    private AudioStreamPlayer2D AudioStreamPlayer2D => GetNode<AudioStreamPlayer2D>("AudioSave");

    private bool _hasBeenUsed = false;

    public override void _Ready()
    {
       SignalManager.Instance.PlayerSaveTheGame += OnPlayerSaveTheGame;
        this.AnimatedSprite2D.Play("idle");
    }

    private void OnPlayerSaveTheGame()
    {
        this._hasBeenUsed = true;
        this.AudioStreamPlayer2D.Play();
        this.AnimatedSprite2D.Play("saving");
    }

    private void _on_sprite_2d_animation_finished()
    {
        this.AnimatedSprite2D.Play("saved");
    }



    public void _on_save_area_body_entered(Node2D body)
    {
        if (!_hasBeenUsed)
        {
            var instance = PlayerManager.GetPlayerGlobalInstance();
            instance.PlayerCanSaveTheGame = true;
            instance.UpdatePlayerPosition(this.GlobalPosition);
        }
    }

    public void _on_save_area_body_exited(Node2D body)
    {
        PlayerManager.GetPlayerGlobalInstance().PlayerCanSaveTheGame = false;
    }


}
