using System.Collections.Generic;
using CodeBase.Config;
using CodeBase.Logic;
using CodeBase.Services.Pause;
using CodeBase.Services.Progress;
using CodeBase.Utility;
using Fusion;
using UnityEngine;
using NetworkPlayer = CodeBase.Logic.NetworkPlayer;

namespace CodeBase.Services
{
    public interface ICharacterFactory
    {
        NetworkPlayer CreatePlayer(NetworkRunner runner, PlayerRef playerRef, int playerIndex);
        List<IPauseHandler> PauseHandlers { get; }
        void CreateAbility(NetworkPlayer networkLocalPlayer, int playerIndex);
    }

    public class CharacterFactory : ICharacterFactory
    {
        private readonly PlayerConfig _playerConfig;
        private readonly LevelConfig _levelConfig;
        private readonly Transform _rootTransform;

        private readonly IAbilityFactory _abilityFactory;
        private readonly IGridHelper _gridHelper;
        private readonly CursorView _cursorView;
        private readonly AbilityView _abilityView;

        public CharacterFactory(PlayerConfig playerConfig, LevelConfig levelConfig, Transform rootTransform,
            IAbilityFactory abilityFactory, IGridHelper gridHelper, CursorView cursorView, AbilityView abilityView)
        {
            _playerConfig = playerConfig;
            _levelConfig = levelConfig;
            _rootTransform = rootTransform;
            
            _abilityFactory = abilityFactory;
            _gridHelper = gridHelper;
            _cursorView = cursorView;
            _abilityView = abilityView;
        }
        
        public List<IPauseHandler> PauseHandlers { get; private set; }
        
        public NetworkPlayer CreatePlayer(NetworkRunner runner, PlayerRef playerRef, int playerIndex)
        {
            NetworkPlayer networkPlayer =  runner.Spawn(_playerConfig.Players[playerIndex], 
                _levelConfig.SpawnPoints[playerIndex].PlayerPosition, Quaternion.identity, playerRef);
            
            networkPlayer.transform.SetParent(_rootTransform);
            return networkPlayer;
        }

        public void CreateAbility(NetworkPlayer networkLocalPlayer, int playerIndex)
        {
            var abilities = new List<IAbility>
            {
                _abilityFactory.CreateMoveAbility(networkLocalPlayer, playerIndex-1),
                _abilityFactory.CreateMagnetAbility(playerIndex-1),
                _abilityFactory.CreateSpawnAbility(networkLocalPlayer, playerIndex-1)
            };
        
            var networkAbility = networkLocalPlayer.GetComponent<NetworkAbility>();
            networkAbility.Construct(abilities);
        
            _cursorView.Construct(_gridHelper);
            _abilityView.Construct(networkAbility, _cursorView);
        
            PauseHandlers = new List<IPauseHandler>() {
                networkLocalPlayer, networkAbility
            };
        }
    }
}