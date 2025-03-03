using System.Collections.Generic;
using _Project.Scripts.Configs;
using Scripts.Core.GameEntity;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace _Project.Scripts.Core.Models
{
    public class AudioModel
    {
        [Inject]
        public AudioModel(AudioConfig config)
        {
            foreach (var audioEntity in config.AudioClips)
                AudioClips.TryAdd(audioEntity.Name, audioEntity.Clip);
            
            SoundsGroup = config.SoundsGroup;
            MusicGroup = config.MusicGroup;
        }

        public Dictionary<AudioClipName, AudioClip> AudioClips { get; private set; } = new();
        public AudioMixerGroup SoundsGroup { get; private set; }
        public AudioMixerGroup MusicGroup { get; private set; }
    }
}