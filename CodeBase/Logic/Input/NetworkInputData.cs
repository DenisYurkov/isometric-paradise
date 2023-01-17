using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector3 MousePosition;
        public NetworkBool Mouse1Press;
        public NetworkBool Mouse2Press;
        public NetworkBool Mouse3Press;
    }
}