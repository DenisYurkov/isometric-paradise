using System;
using CodeBase.Config;
using CodeBase.Utility;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class MoveAbility : IAbility
    {
        public event Action<bool> AvailableEvent;

        private readonly NetworkPlayer _networkPlayer;
        private readonly IGridHelper _gridHelper;
        private readonly Map _map;
        private readonly MoveAbilityConfig _abilityConfig;

        public TickTimer TickTimer { get; set; }
        public Texture2D GetCursorTexture() => _abilityConfig.CursorTexture;

        public MoveAbility(NetworkPlayer networkPlayer, IGridHelper gridHelper, Map map, MoveAbilityConfig abilityConfig)
        {
            _networkPlayer = networkPlayer;
            _gridHelper = gridHelper;
            _map = map;
            _abilityConfig = abilityConfig;
        }

        public bool Available(NetworkInputData inputData)
        {
            Vector3Int gridPosition = _gridHelper.GetGridPosition(inputData.MousePosition);
            Vector2 cellPosition = _gridHelper.GetCellPosition(inputData.MousePosition);
            
            AvailableEvent?.Invoke(OnAvailable(inputData.MousePosition, gridPosition, cellPosition));
            return inputData.Mouse1Press && OnAvailable(inputData.MousePosition, gridPosition, cellPosition);
        }

        private bool OnAvailable(Vector2 mousePosition, Vector3Int gridPosition, Vector2 cellPosition)
        {
            return _map.HasTile(gridPosition) && !_gridHelper.ColliderRaycast(mousePosition) && 
                   Vector2.Distance(_networkPlayer.transform.position, cellPosition) <= _abilityConfig.MaxDistance;
        }

        public void UseAbility(NetworkInputData inputData, NetworkRunner runner)
        {
            if (TickTimer.ExpiredOrNotRunning(runner))
            {
                _networkPlayer.RPC_Move(_gridHelper.GetCellPosition(inputData.MousePosition));
                TickTimer = TickTimer.CreateFromSeconds(runner, _abilityConfig.AbilityDelay);
            }
        }
    }
}