using CodeBase.Config;
using CodeBase.Services;
using CodeBase.Services.Pause;
using DG.Tweening;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class NetworkPlayer : NetworkBehaviour, IPauseHandler
    {
        [SerializeField] private MoveAbilityConfig _moveAbilityConfig;
        [SerializeField] private SpawnAbilityConfig _spawnAbilityConfig;
        [SerializeField] private FinishWindow _finishWindow;
        
        private IAbility _currentAbility;
        private readonly IObstacleFactory _obstacleFactory = new ObstacleFactory();

        public bool Pause { get; set; }
        
        public void SetAbility(IAbility ability) => 
            _currentAbility = ability;

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_Move(Vector2 getCellPosition)
        {
            transform.DOMove(getCellPosition, _moveAbilityConfig.PlayerSpeed)
                .SetEase(_moveAbilityConfig.AnimationCurve);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_ObstacleSpawn(Vector2 spawnPoint)
        {
            _obstacleFactory.Create(Runner, _spawnAbilityConfig.DefaultObstacle, spawnPoint);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_Finish() => 
            _finishWindow.OnFinished.Invoke();

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out NetworkInputData inputData) || Pause) return;
            
            if (_currentAbility != null && _currentAbility.Available(inputData)) {
                _currentAbility.UseAbility(inputData, Runner);
            }
        }

        private void OnDisable() => 
            DOTween.KillAll(true);
    }
}