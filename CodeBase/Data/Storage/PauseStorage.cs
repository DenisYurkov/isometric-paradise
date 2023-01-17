using System.Collections.Generic;
using CodeBase.Services.Pause;

namespace CodeBase.Data
{
    public class PauseStorage
    {
        private readonly List<IPauseHandler> _pauseHandlers = new();
        
        public IReadOnlyList<IPauseHandler> PauseHandlers => _pauseHandlers;

        public void Add(IPauseHandler pauseHandler) => 
            _pauseHandlers.Add(pauseHandler);

        public void Clear() => 
            _pauseHandlers.Clear();
    }
}