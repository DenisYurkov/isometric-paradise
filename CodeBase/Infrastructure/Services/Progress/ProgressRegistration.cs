using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Services.Pause;

namespace CodeBase.Services.Progress
{
    public interface IProgressRegistration
    {
        void RegisterPauseHandlers(List<IPauseHandler> pauseHandlers);
        void Unregister();
    }

    public class ProgressRegistration : IProgressRegistration
    {
        private readonly PauseStorage _pauseStorage;

        public ProgressRegistration(PauseStorage pauseStorage) => 
            _pauseStorage = pauseStorage;

        public void RegisterPauseHandlers(List<IPauseHandler> pauseHandlers)
        {
            foreach (IPauseHandler pauseHandler in pauseHandlers)
                Register(pauseHandler);
        }

        private void Register(IPauseHandler pauseHandler) =>
            _pauseStorage.Add(pauseHandler);

        public void Unregister() => 
            _pauseStorage.Clear();
    }
}