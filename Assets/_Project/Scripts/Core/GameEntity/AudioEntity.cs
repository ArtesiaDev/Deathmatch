using System;
using UnityEngine;
using AudioClip = _Project.Scripts.Services.AudioManagement.AudioClip;

namespace Scripts.Core.GameEntity
{
    [Serializable]
    public struct AudioEntity
    {
        [field: SerializeField] public UnityEngine.AudioClip Clip {get; private set;}
        [field: SerializeField] public AudioClip Name {get; private set;}
    }
}