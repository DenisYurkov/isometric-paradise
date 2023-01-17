using Fusion;
using UnityEngine;

namespace CodeBase.Services
{
    public interface IObstacleFactory
    {
        void Create(NetworkRunner runner, GameObject obstaclePrefab, Vector2 spawnPosition);
    }

    public class ObstacleFactory : IObstacleFactory
    {
        public void Create(NetworkRunner runner, GameObject obstaclePrefab, Vector2 spawnPosition)
        {
            NetworkObject networkObject = runner.Spawn(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }
}