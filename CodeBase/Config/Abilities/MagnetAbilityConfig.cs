using UnityEngine;

namespace CodeBase.Config
{
    [CreateAssetMenu(fileName = "Magnet Ability Config", menuName = "Ability/Abilities Config/Magnet Ability", order = 0)]
    public class MagnetAbilityConfig : ScriptableObject
    {
        public Texture2D CursorTexture;
        public AnimationCurve AnimationCurve;
        public float AttractionSpeed;
        public float AbilityDelay;
    }
}