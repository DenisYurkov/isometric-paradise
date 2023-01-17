using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Services.Pause;
using CodeBase.Services.Progress;
using Fusion;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private NetworkRunner _networkRunnerPrefab;
        
        public override void InstallBindings()
        {
            var networkRunner = Instantiate(_networkRunnerPrefab);
            var networkRunnerHandler = new NetworkRunnerHandler(networkRunner);

            var game = new Game(networkRunnerHandler);
            var pauseStorage = new PauseStorage();
            
            IPauseService pauseService = new PauseService(pauseStorage);
            IProgressRegistration progressRegistration = new ProgressRegistration(pauseStorage);

            Container.Bind(typeof(INetworkRunnerHandler)).FromInstance(networkRunnerHandler).AsSingle();
            Container.Bind<Game>().FromInstance(game).AsSingle();

            Container.Bind(typeof(IPauseService)).FromInstance(pauseService).AsSingle();
            Container.Bind(typeof(IProgressRegistration)).FromInstance(progressRegistration).AsSingle();

            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}