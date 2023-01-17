using System.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public interface INetworkRunnerHandler
    { 
        Task StartGame(string sessionName, SerializedScene sceneRef);
        NetworkRunner GetNetworkRunner();
    }
}