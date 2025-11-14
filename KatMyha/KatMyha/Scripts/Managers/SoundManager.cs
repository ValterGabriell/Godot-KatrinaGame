using Godot;
using PrototipoMyha;
using PrototipoMyha.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatMyha.Scripts.Managers
{

    public enum SoundExtension
    {
        mp3,wav
    }
    public partial class SoundManager : Node
    {
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SoundManager();
                }
                return _instance;
            }
        }

        private Dictionary<AudioStreamPlayer2D, double> _lastPlayTime = new Dictionary<AudioStreamPlayer2D, double>();
        private const double COOLDOWN = 0.4;

        public void PlaySound(AudioStreamPlayer2D audio, SoundExtension? soundExtension = SoundExtension.mp3)
        {
            bool isWalkSound = audio.Name.ToString().Contains("walk", StringComparison.OrdinalIgnoreCase);
            
            if (isWalkSound)
            {
                double currentTime = Time.GetTicksMsec() / 1000.0;
                
                if (_lastPlayTime.ContainsKey(audio))
                {
                    double timeSinceLastPlay = currentTime - _lastPlayTime[audio];
                    if (timeSinceLastPlay < COOLDOWN)
                        return;
                }
                
                _lastPlayTime[audio] = currentTime;
            }
            
            audio.Stream = ResourceLoader.Load<AudioStream>($"res://Assets/Sound/{audio.Name}.{soundExtension}");
            audio.Play();
        }


    }
}
