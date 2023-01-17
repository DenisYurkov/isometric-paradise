using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Logic
{
    public class PlayWindow : MonoBehaviour
    {
        [SerializeField] private SerializedScene _sceneRef;
        
        [SerializeField] private Button _button;
        [SerializeField] private TMP_InputField _inputField;
        
        private Game _game;
        private PlayerInput _playerInput;
        private IInputService _inputService;

        [Inject]
        private void Construct(Game game, IInputService inputService)
        {
            _game = game;
            _inputService = inputService;
        }

        private void OnEnable()
        {
            _inputService.EnterAction.performed += EnterAction;
            _button.onClick.AddListener(Enter);
        }

        private void OnDisable()
        {
            _inputService.EnterAction.performed -= EnterAction;
            _button.onClick.RemoveListener(Enter);
        }

        private void Enter()
        {
            if (_inputField.text != string.Empty)
            {
                _game.CreateRoom(_inputField.text, _sceneRef);
                _button.interactable = false;
            }
        }
        
        private void EnterAction(InputAction.CallbackContext obj) => Enter();
    }
}