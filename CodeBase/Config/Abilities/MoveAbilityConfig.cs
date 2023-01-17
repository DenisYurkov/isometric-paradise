using UnityEngine;

namespace CodeBase.Config
{
    [CreateAssetMenu(fileName = "Move Ability Config", menuName = "Ability/Abilities Config/Move Ability")]
    public class MoveAbilityConfig : ScriptableObject
    {
        public Texture2D CursorTexture;
        public AnimationCurve AnimationCurve;
        public float MaxDistance;
        [Range(0.01f, 1f)] public float PlayerSpeed;
        public float AbilityDelay;
    }
}