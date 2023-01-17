using System.Collections.Generic;
using CodeBase.Services.Pause;
using CodeBase.Services.Progress;
using EasyButtons;
using Fusion;
using Zenject;

namespace CodeBase.Logic
{
    public class GameLoop : NetworkBehaviour
    {
        private IPauseService _pauseService;
        private IProgressRegistration _progressRegistration;

        [Inject]
        private void Construct(IPauseService pauseService, IProgressRegistration progressRegistration)
        {
            _pauseService = pauseService;
            _progressRegistration = progressRegistration;
        }

        [Button]
        public void Enter(List<IPauseHandler> pauseHandlers) => 
            _progressRegistration.RegisterPauseHandlers(pauseHandlers);


        [Button]
        public void End(NetworkPlayer player)
        {
            _pauseService.Pause();
            _progressRegistration.Unregister();
            
            player.RPC_Finish();
        }
    }
}