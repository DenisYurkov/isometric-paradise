using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Services
{
    public class InputFactory : IInputFactory
    {
        public NetworkInputData Create()
        {
            var data = new NetworkInputData
            {
                MousePosition = Input.mousePosition,
                Mouse1Press = Input.GetMouseButton(0),
                Mouse2Press = Input.GetMouseButton(1),
                Mouse3Press = Input.GetMouseButton(2)
            };
            
            return data;
        }
    }

    public interface IInputFactory
    {
        NetworkInputData Create();
    }
}