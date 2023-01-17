using System.Collections.Generic;
using CodeBase.Config;
using CodeBase.Logic;
using CodeBase.Utility;
using UnityEngine;
using NetworkPlayer = CodeBase.Logic.NetworkPlayer;

namespace CodeBase.Services
{
    public interface IAbilityFactory
    {
        IAbility CreateMoveAbility(NetworkPlayer networkPlayer, int playerIndex);
        IAbility CreateMagnetAbility(int playerIndex);
        IAbility CreateSpawnAbility(NetworkPlayer networkPlayer, int playerIndex);
    }

    public class AbilityFactory : IAbilityFactory
    {
        private readonly IGridHelper _gridHelper;
        
        private readonly List<Map> _listOfMap;
        private readonly List<Obstacle> _obstacles;
        
        private readonly AbilitiesConfig _abilitiesConfig;

        public AbilityFactory(IGridHelper gridHelper, List<Map> listOfMap, List<Obstacle> obstacles, AbilitiesConfig abilitiesConfig)
        {
            _gridHelper = gridHelper;
            _listOfMap = listOfMap;
            _obstacles = obstacles;
            _abilitiesConfig = abilitiesConfig;
        }

        public IAbility CreateMoveAbility(NetworkPlayer networkPlayer, int playerIndex) =>
            new MoveAbility(networkPlayer, _gridHelper, _listOfMap[playerIndex], _abilitiesConfig.MoveAbilityConfig);

        public IAbility CreateMagnetAbility(int playerIndex) =>
            new MagnetAbility(_gridHelper, _listOfMap[playerIndex], _abilitiesConfig.MagnetAbilityConfig);

        public IAbility CreateSpawnAbility(NetworkPlayer networkPlayer, int playerIndex) => 
            new SpawnAbility(networkPlayer, _gridHelper, _obstacles[playerIndex], _abilitiesConfig.SpawnAbilityConfig);
    }
}