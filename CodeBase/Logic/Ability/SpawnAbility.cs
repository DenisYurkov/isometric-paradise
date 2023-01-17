using System;
using CodeBase.Config;
using CodeBase.Services;
using CodeBase.Utility;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SpawnAbility : IAbility
    {
        public event Action<bool> AvailableEvent;

        private readonly IGridHelper _gridHelper;
        private readonly Obstacle _obstacle;
        private readonly SpawnAbilityConfig _abilityConfig;
        private readonly NetworkPlayer _networkPlayer;

        public TickTimer TickTimer { get; set; }
        public Texture2D GetCursorTexture() => _abilityConfig.CursorTexture;

        public SpawnAbility(NetworkPlayer networkPlayer, IGridHelper gridHelper, Obstacle obstacle, SpawnAbilityConfig abilityConfig)
        {
            _networkPlayer = networkPlayer;
            _gridHelper = gridHelper;
            _obstacle = obstacle;
            _abilityConfig = abilityConfig;
        }
        
        public bool Available(NetworkInputData inputData)
        {
            Vector3Int gridPosition = _gridHelper.GetGridPosition(inputData.MousePosition);
            
            AvailableEvent?.Invoke(OnAvailable(inputData, gridPosition));
            return inputData.Mouse1Press && OnAvailable(inputData, gridPosition);
        }

        private bool OnAvailable(NetworkInputData inputData, Vector3Int gridPosition) => 
            !_gridHelper.ColliderRaycast(inputData.MousePosition) && _obstacle.ObstacleMap.HasTile(gridPosition);

        public void UseAbility(NetworkInputData inputData, NetworkRunner runner)
        {
            if (TickTimer.ExpiredOrNotRunning(runner))
            {
                _networkPlayer.RPC_ObstacleSpawn(_gridHelper.GetCellPosition(inputData.MousePosition)); 
                TickTimer = TickTimer.CreateFromSeconds(runner, _abilityConfig.AbilityDelay);
            }
        }
    }
}