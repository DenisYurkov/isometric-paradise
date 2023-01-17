using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly INetworkRunnerHandler _networkRunnerHandler;

        public Game(INetworkRunnerHandler networkRunnerHandler) => 
            _networkRunnerHandler = networkRunnerHandler;
        
        public void CreateRoom(string roomName, SerializedScene sceneRef) => 
            _networkRunnerHandler.StartGame(roomName, sceneRef);
    }
}
