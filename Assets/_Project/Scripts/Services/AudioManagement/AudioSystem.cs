using _Project.Scripts.Core.Models;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.AudioManagement
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;

        private AudioModel _audioModel;

        [Inject]
        private void Construct(AudioModel audioModel) =>
            _audioModel = audioModel;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void SetSoundsVolume(float percentage)
        {
            var volume = Mathf.Lerp(-20f, 20f, percentage);
            if (percentage == 0)
                volume = -80;
            _audioModel.AudioMixer.SetFloat(MixerParameters.SoundsVolume.ToString(), volume);
        }

        public void SetMusicVolume(float percentage)
        {
            var volume = Mathf.Lerp(-20f, 20f, percentage);
            if (percentage == 0)
                volume = -80;
            _audioModel.AudioMixer.SetFloat(MixerParameters.MusicVolume.ToString(), volume);
        }

        public void PlayOneShotSound(AudioClip clipName, float volume = 1f, float pitch = 1f)
        {
            var clip = _audioModel.AudioClips[clipName];
            _soundSource.pitch = pitch;
            _soundSource.loop = false;
            _soundSource.PlayOneShot(clip, volume);
        }

        public void PlayBackgroundMusic(AudioClip clipName, float volume = 1f, float pitch = 1f)
        {
            var clip = _audioModel.AudioClips[clipName];
            _musicSource.volume = volume;
            _musicSource.pitch = pitch;
            _musicSource.loop = true;
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void StopBackgroundMusic(float duration = default) =>
            _musicSource.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                _musicSource.Stop();
                _musicSource.clip = null;
            });

        public void PlayClipAtPoint(AudioClip clipName, Vector3 position, float volume = 1f)
        {
            var clip = _audioModel.AudioClips[clipName];
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}