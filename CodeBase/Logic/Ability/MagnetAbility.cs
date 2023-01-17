using System;
using CodeBase.Config;
using CodeBase.Utility;
using DG.Tweening;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class MagnetAbility : IAbility
    {
        public event Action<bool> AvailableEvent;
        
        private readonly IGridHelper _gridHelper;
        private readonly Map _map;

        private NetworkObstacle _currentObstacle;
        private readonly MagnetAbilityConfig _abilityConfig;

        public TickTimer TickTimer { get; set; }
        public Texture2D GetCursorTexture() => _abilityConfig.CursorTexture;

        public MagnetAbility(IGridHelper gridHelper, Map map, MagnetAbilityConfig abilityConfig)
        {
            _gridHelper = gridHelper;
            _map = map;
            _abilityConfig = abilityConfig;
        }

        public bool Available(NetworkInputData inputData)
        {
            Vector3Int gridPosition = _gridHelper.GetGridPosition(inputData.MousePosition);
            AvailableEvent?.Invoke(TrySelect(inputData, gridPosition));
            
            if (_currentObstacle != null) return true;

            if (inputData.Mouse1Press && TrySelect(inputData, gridPosition))
            {
                SelectObstacle();
                return true;
            }
            
            return false;
        }
        
        private bool TrySelect(NetworkInputData inputData, Vector3Int gridPosition) => 
            _gridHelper.ObstacleRaycast(inputData.MousePosition) && _map.HasTile(gridPosition);

        private bool TryUse(NetworkInputData inputData, Vector3Int gridPosition) => 
            !_gridHelper.ColliderRaycast(inputData.MousePosition) && _map.HasTile(gridPosition);
        
        public void UseAbility(NetworkInputData inputData, NetworkRunner runner)
        {
            Vector3Int gridPosition = _gridHelper.GetGridPosition(inputData.MousePosition);
            AvailableEvent?.Invoke(TryUse(inputData, gridPosition));
            
            if (inputData.Mouse2Press && TryUse(inputData, gridPosition) && TickTimer.ExpiredOrNotRunning(runner))
            {
                MoveObstacle(inputData.MousePosition);
                TickTimer = TickTimer.CreateFromSeconds(runner, _abilityConfig.AbilityDelay);
                _currentObstacle = null;
            }
        }

        private void SelectObstacle() => 
            _currentObstacle = _gridHelper.GetRaycast().collider.GetComponent<NetworkObstacle>();

        private void MoveObstacle(Vector2 mousePosition)
        {
            Vector2 cellPosition = _gridHelper.GetCellPosition(mousePosition);
            _currentObstacle.RPC_MoveObstacle(cellPosition);
        }
    }
}