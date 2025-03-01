using System;
using UnityEngine;

namespace Scripts.Core.GameEntity
{
    [Serializable]
    public struct Range<T, T1>
        where T : struct
        where T1 : struct
    {
        public Range(T min, T1 max)
        {
            Min = min;
            Max = max;
        }

        [field: Tooltip("Minimum value.")]
        [field: SerializeField]
        public T Min { get; private set; }

        [field: Tooltip("Maximum value.")]
        [field: SerializeField]
        public T1 Max { get; private set; }
    }
}