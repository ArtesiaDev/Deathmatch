using System;
using UnityEngine;

namespace Scripts.Core.GameEntity
{
    [Serializable]
    public struct AudioEntity
    {
        [field: SerializeField] public AudioClip Clip {get; private set;}
        [field: SerializeField] public AudioClipName Name {get; private set;}
    }
}