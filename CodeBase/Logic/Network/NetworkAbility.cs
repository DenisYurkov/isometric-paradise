using System.Collections.Generic;
using CodeBase.Services.Pause;
using Fusion;
using UnityEngine;

namespace CodeBase.Logic
{
    public class NetworkAbility : NetworkBehaviour, IPauseHandler
    {
        public int AbilityIndex;
        public List<IAbility> Abilities;
        
        [SerializeField] private NetworkPlayer _networkPlayer;
        private TickTimer _inputDelay;
        
        public bool Pause { get; set; }

        public void Construct(List<IAbility> abilities)
        {
            Abilities = abilities;
            _networkPlayer.SetAbility(abilities[0]);
        }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out NetworkInputData inputData) || Pause) return;
            
            if (inputData.Mouse3Press && Abilities != null && _inputDelay.ExpiredOrNotRunning(Runner))
            {
                TryChangeAbility();
                _inputDelay = TickTimer.CreateFromSeconds(Runner, 0.5f);
            }
        }
        
        private void TryChangeAbility()
        {
            AbilityIndex++;
            
            if (AbilityIndex >= Abilities.Count) {
                AbilityIndex = 0;
            }
            
            UpdateAbility();
        }

        private void UpdateAbility()
        {
            _networkPlayer.SetAbility(Abilities[AbilityIndex]);
        }
    }
}