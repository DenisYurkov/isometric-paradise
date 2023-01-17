using System.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class NetworkRunnerHandler : INetworkRunnerHandler
    {
        private readonly NetworkRunner _networkRunner;
        
        public NetworkRunnerHandler(NetworkRunner networkRunner) => 
            _networkRunner = networkRunner;

        public async Task StartGame(string sessionName, SerializedScene sceneRef)
        {
            _networkRunner.ProvideInput = true;
            
            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = sessionName,
                PlayerCount = 2,
                Scene = sceneRef.BuildIndex,
                SceneManager = GetNetworkSceneManager(_networkRunner)
            });
        }

        public NetworkRunner GetNetworkRunner()
        {
            if (_networkRunner == null) 
                Debug.LogError("Network runner is null");

            return _networkRunner;
        }

        private NetworkSceneManagerDefault GetNetworkSceneManager(NetworkRunner networkRunner)
        {
            if (networkRunner.TryGetComponent(out NetworkSceneManagerDefault networkSceneManagerDefault))
                return networkSceneManagerDefault;

            var networkSceneManager = networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            return networkSceneManager;
        }
    }
}