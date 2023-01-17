using CodeBase.Data;

namespace CodeBase.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly PauseStorage _pauseStorage;

        public PauseService(PauseStorage pauseStorage) => 
            _pauseStorage = pauseStorage;
        
        public void Pause()
        {
            foreach (var pauseHandler in _pauseStorage.PauseHandlers)
            {
                pauseHandler.Pause = true;
            }
        }

        public void Unpause()
        {
            foreach (var pauseHandler in _pauseStorage.PauseHandlers)
            {
                pauseHandler.Pause = false;
            }
        }
    }

    public interface IPauseService
    {
        void Pause();
        void Unpause();
    }
}