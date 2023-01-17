using UnityEngine;

namespace CodeBase.Config
{
    [CreateAssetMenu(fileName = "Spawn Ability Config", menuName = "Ability/Abilities Config/Spawn Ability", order = 0)]
    public class SpawnAbilityConfig : ScriptableObject
    {
        public Texture2D CursorTexture;
        public GameObject DefaultObstacle;
        public float AbilityDelay;
    }
}