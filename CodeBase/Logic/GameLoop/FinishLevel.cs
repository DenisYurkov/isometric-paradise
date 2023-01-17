using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class FinishLevel : NetworkBehaviour
    {
        private GameLoop _gameLoop;
        private NetworkPlayer _networkPlayer;
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_Setup(GameLoop gameLoop, NetworkPlayer networkPlayer)
        {
            _gameLoop = gameLoop;
            _networkPlayer = networkPlayer;
        }
        
        public override void FixedUpdateNetwork()
        {
            if (_networkPlayer == null) return;
            
            if ((Vector2) _networkPlayer.transform.position == (Vector2) transform.position)
            {
                _gameLoop.End(_networkPlayer);
                Destroy(gameObject);
            }
        }
    }
}