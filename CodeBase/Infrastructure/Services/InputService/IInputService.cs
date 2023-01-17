using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services
{
    public interface IInputService
    {
        public InputAction EnterAction { get;}
        public InputAction EscapeAction { get; }
    }
}