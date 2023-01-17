using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Logic
{
    public class CloseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _openWindow;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService) => 
            _inputService = inputService;

        private void OnEnable() => 
            _inputService.EscapeAction.performed += EscapeAction;

        private void EscapeAction(InputAction.CallbackContext obj)
        {
            _openWindow.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnDisable() => 
            _inputService.EscapeAction.performed -= EscapeAction;
    }
}