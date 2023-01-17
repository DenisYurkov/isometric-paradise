using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class FinishWindow : MonoBehaviour
    {
        public Action OnFinished;

        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private List<Button> _buttons;

        private void ShowScreen() => 
            _gameOverScreen.SetActive(true);

        private void OnEnable()
        {
            OnFinished += ShowScreen;

            foreach (var button in _buttons) 
                button.onClick.AddListener(() => SceneManager.LoadScene("Scenes/Level 1"));
        }

        private void OnDisable()
        {
            OnFinished -= ShowScreen;
            
            foreach (var button in _buttons) 
                button.onClick.AddListener(() => SceneManager.LoadScene("Scenes/Level 1"));
        }
    }
}