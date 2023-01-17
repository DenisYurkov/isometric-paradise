using UnityEngine;

namespace CodeBase.Config
{
    [CreateAssetMenu(fileName = "Abilities Config", menuName = "Ability/Create Ability Config", order = 0)]
    public class AbilitiesConfig : ScriptableObject
    {
        public MoveAbilityConfig MoveAbilityConfig;
        public MagnetAbilityConfig MagnetAbilityConfig;
        public SpawnAbilityConfig SpawnAbilityConfig;
    }
}