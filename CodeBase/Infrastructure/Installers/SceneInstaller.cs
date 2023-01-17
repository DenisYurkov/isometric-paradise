using System.Collections.Generic;
using CodeBase.Config;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Utility;
using Fusion;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public GameLoop GameLoop;
        
        public CursorView CursorView;
        public AbilityView AbilityView;
        public NetworkReceiver NetworkReceiver;
        
        [Header("Scene Config")]
        public Grid Grid;
        public List<Map> MapsList;
        public LevelConfig LevelConfig;

        [Header("Player Config")] 
        public PlayerConfig PlayerConfig;
        public Transform SpawnRoot;

        [Header("Abilities Config")] 
        public AbilitiesConfig AbilitiesConfig;
        
        [Header("Obstacle Config")]
        public List<Obstacle> Obstacles;
        
        [Header("Grid Helper")]
        [SerializeField] private Camera _camera;
       
        private NetworkRunner _networkRunner;
        
        [Inject]
        private void Construct(INetworkRunnerHandler networkRunnerHandler) => 
            _networkRunner = networkRunnerHandler.GetNetworkRunner();

        public override void InstallBindings()
        {
            IGridHelper gridHelper = new GridHelper(Grid, _camera);

            IAbilityFactory abilityFactory = new AbilityFactory(gridHelper, MapsList, Obstacles, AbilitiesConfig);
            ICharacterFactory characterFactory = new CharacterFactory(PlayerConfig, LevelConfig, SpawnRoot, abilityFactory, gridHelper, CursorView, AbilityView);
            ILevelFactory levelFactory = new LevelFactory(GameLoop, LevelConfig, PlayerConfig);
            
            NetworkReceiver.Init(_networkRunner, characterFactory, new InputFactory(), GameLoop, levelFactory);
            
            Container.Bind(typeof(IGridHelper)).FromInstance(gridHelper).AsSingle();
            Container.Bind<NetworkReceiver>().FromInstance(NetworkReceiver).AsSingle();
        }
    }   
}