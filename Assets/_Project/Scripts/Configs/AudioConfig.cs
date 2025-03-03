using System.Collections.Generic;
using Scripts.Core.GameEntity;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig", order = 1)]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField] public List<AudioEntity> AudioClips { get; private set; }
        public AudioMixerGroup SoundsGroup { get; private set; }
        public AudioMixerGroup MusicGroup { get; private set; }
    }
}