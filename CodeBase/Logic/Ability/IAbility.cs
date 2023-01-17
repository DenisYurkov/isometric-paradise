using System;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public interface IAbility
    {
        public event Action<bool> AvailableEvent;
        bool Available(NetworkInputData inputData);
        void UseAbility(NetworkInputData inputData, NetworkRunner runner);
        Texture2D GetCursorTexture();
        TickTimer TickTimer { get; set; }
    }
}