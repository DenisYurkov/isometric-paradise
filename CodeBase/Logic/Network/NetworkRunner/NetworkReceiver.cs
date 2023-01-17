using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using Fusion;
using Fusion.Sockets;

namespace CodeBase.Logic
{
    public class NetworkReceiver : NetworkBehaviour, INetworkRunnerCallbacks, IDisposable
    {
        private NetworkRunner _networkRunner;
        private ICharacterFactory _characterFactory;
        private IInputFactory _inputFactory;
        private GameLoop _gameLoop;
        private ILevelFactory _levelFactory;

        private readonly Dictionary<PlayerRef, NetworkPlayer> _players = new();

        [Networked(OnChanged = nameof(OnPlayersListChanged)), Capacity(4), UnitySerializeField]
        private NetworkDictionary<PlayerRef, NetworkPlayer> NetworkPlayers { get; }

        public void Init(NetworkRunner networkRunner, ICharacterFactory characterFactory, IInputFactory inputFactory, GameLoop gameLoop, ILevelFactory levelFactory)
        {
            _networkRunner = networkRunner;
            _characterFactory = characterFactory;
            _inputFactory = inputFactory;
            
            _gameLoop = gameLoop;
            _levelFactory = levelFactory;

            _networkRunner.AddCallbacks(this);
        }
        
        public void Dispose() => 
            _networkRunner.RemoveCallbacks(this);

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef playerRef)
        {
            if (runner.IsServer)
            {
                NetworkPlayer networkPlayer = _characterFactory.CreatePlayer(runner, playerRef, _players.Count);
                
                _levelFactory.Create(runner, networkPlayer, playerRef, _players.Count);
                _players.Add(playerRef, networkPlayer);
                
                if (!NetworkPlayers.ContainsKey(playerRef))
                {
                    NetworkPlayers.Add(playerRef, networkPlayer);
                }
            }
        }

        public static void OnPlayersListChanged(Changed<NetworkReceiver> changed)
        {
            KeyValuePair<PlayerRef, NetworkPlayer> newPlayer = changed.Behaviour.NetworkPlayers.ElementAt(0);
            changed.Behaviour.RPC_InitPlayerLocally(newPlayer.Key);
        }

        [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
        private void RPC_InitPlayerLocally([RpcTarget] PlayerRef playerRef)
        {
            NetworkPlayer networkPlayer = NetworkPlayers.Get(playerRef);
            InitPlayer(networkPlayer, NetworkPlayers.Count);
        }

        private void InitPlayer(NetworkPlayer networkPlayer, int amount)
        {
            _characterFactory.CreateAbility(networkPlayer, amount); 
            _gameLoop.Enter(_characterFactory.PauseHandlers);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_players.TryGetValue(player, out NetworkPlayer networkPlayer))
            {
                _players.Remove(player);
                runner.Despawn(networkPlayer.Object);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input) => 
            input.Set(_inputFactory.Create());

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
    }
}
