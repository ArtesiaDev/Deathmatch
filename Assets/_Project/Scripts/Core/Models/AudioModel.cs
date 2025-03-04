using System.Collections.Generic;
using _Project.Scripts.Configs;
using UnityEngine.Audio;
using Zenject;
using AudioClip = _Project.Scripts.Services.AudioManagement.AudioClip;

namespace _Project.Scripts.Core.Models
{
    public class AudioModel
    {
        [Inject]
        public AudioModel(AudioConfig config)
        {
            foreach (var audioEntity in config.AudioClips)
                AudioClips.TryAdd(audioEntity.Name, audioEntity.Clip);

            AudioMixer = config.AudioMixer;
        }

        public Dictionary<AudioClip, UnityEngine.AudioClip> AudioClips { get; private set; } = new();
        public AudioMixer AudioMixer { get; private set; }
    }
}