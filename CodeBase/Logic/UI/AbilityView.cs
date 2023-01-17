using TMPro;
using UnityEngine;

namespace CodeBase.Logic
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _abilityName;
        [SerializeField] private TMP_Text _abilityDelay;

        [SerializeField] private CursorView _cursorView;
        [SerializeField] private NetworkAbility _networkAbility;
 
        public void Construct(NetworkAbility networkAbility, CursorView cursorView)
        {
            _networkAbility = networkAbility;
            _cursorView = cursorView;
            
            foreach (var ability in _networkAbility.Abilities)
                ability.AvailableEvent += _cursorView.ChangeSpriteColor;
        }

        private void OnDisable()
        {
            if (_cursorView is null || _networkAbility is null) return;
            _cursorView.UpdateCursor(null);
            
            foreach (var ability in _networkAbility.Abilities) 
                ability.AvailableEvent -= _cursorView.ChangeSpriteColor;
        }

        private void Update()
        {
            if (_networkAbility == null) return;
            UpdateView(_networkAbility.Abilities[_networkAbility.AbilityIndex].GetType().Name, _networkAbility.Abilities[_networkAbility.AbilityIndex].TickTimer.RemainingTime(_networkAbility.Runner));
        }

        private void UpdateView(string arg1, float? arg2)
        {
            _abilityName.text = arg1 + ": ";
            _abilityDelay.text = arg2 == null ? "0" : arg2.Value.ToString("f2");
            _cursorView.UpdateCursor(_networkAbility.Abilities[_networkAbility.AbilityIndex].GetCursorTexture());
        }
    }
}