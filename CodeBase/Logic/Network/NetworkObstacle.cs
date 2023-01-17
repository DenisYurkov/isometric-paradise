using CodeBase.Config;
using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic
{
    public class NetworkObstacle : NetworkBehaviour
    {
        [SerializeField] private MagnetAbilityConfig _magnetAbilityConfig;
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_MoveObstacle(Vector2 getCellPosition)
        {
            transform
                .DOMove(getCellPosition, _magnetAbilityConfig.AttractionSpeed)
                .SetEase(_magnetAbilityConfig.AnimationCurve);
        }
    }
}