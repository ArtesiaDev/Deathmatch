using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.View
{
    public class TestView : MonoBehaviour
    {
        [SerializeField] Button _createButton;


        private void Awake()
        {
            _createButton.onClick.AddListener(() => Debug.LogWarning("GameScene"));
        }
    }
}