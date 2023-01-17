using System;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services
{
    public class InputService : IDisposable, IInputService
    {
        private readonly PlayerInput _playerInput;

        public InputAction EnterAction { get; private set; }
        public InputAction EscapeAction { get; private set; }

        public InputService()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            
            EnterAction = _playerInput.PlayerMap.Enter;
            EscapeAction = _playerInput.PlayerMap.Escape;
        }

        public void Dispose() => 
            _playerInput?.Dispose();
    }
}