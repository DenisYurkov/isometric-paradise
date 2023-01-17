using CodeBase.Config;
using CodeBase.Logic;
using Fusion;
using UnityEngine;
using NetworkPlayer = CodeBase.Logic.NetworkPlayer;

namespace CodeBase.Services
{
    public interface ILevelFactory
    {
        void Create(NetworkRunner runner, NetworkPlayer networkPlayer, PlayerRef playerRef, int playerIndex);
    }

    public class LevelFactory : ILevelFactory
    {
        private readonly GameLoop _gameLoop;
        private readonly LevelConfig _levelConfig;
        private readonly PlayerConfig _playerConfig;

        public LevelFactory(GameLoop gameLoop, LevelConfig levelConfig, PlayerConfig playerConfig)
        {
            _gameLoop = gameLoop;
            _levelConfig = levelConfig;
            _playerConfig = playerConfig;
        }

        public void Create(NetworkRunner runner, NetworkPlayer networkPlayer, PlayerRef playerRef, int playerIndex)
        {
            FinishLevel finishLevel = runner.Spawn(_playerConfig.CrystalObject,
                _levelConfig.SpawnPoints[playerIndex].CrystalPosition, Quaternion.identity, playerRef);
            
            finishLevel.RPC_Setup(_gameLoop, networkPlayer);
        }
    }
}