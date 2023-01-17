using System.Collections.Generic;
using CodeBase.Logic;
using UnityEngine;
using NetworkPlayer = CodeBase.Logic.NetworkPlayer;

namespace CodeBase.Config
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public List<NetworkPlayer> Players;
        public FinishLevel CrystalObject;
    }
}