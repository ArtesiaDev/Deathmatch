using Scripts.Core.GameEntity;
using UnityEngine;

namespace _Project.Scripts.Core.Models
{
    public class SettingsModel
    {
        public int MaxPlayers { get; private set; } = 4;
        public Range<int, int> MaxPlayersRange { get; private set; } = new(1, 10);
        [field: Range(0, 100)] public float SoundsVolume { get; private set; } = 60;
        [field: Range(0, 100)] public float MusicVolume { get; private set; } = 60;

        public void SetMaxPlayers(int maxPlayers) =>
            MaxPlayers = maxPlayers;
        
        public void SetSoundsVolume(float newValue) =>
            SoundsVolume = newValue;
        
        public void SetMusicVolume(float newValue) =>
            MusicVolume = newValue;
    }
}