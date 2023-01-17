using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Config
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Configs/Level", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public List<SpawnPoint> SpawnPoints;
    }

    [Serializable]
    public struct SpawnPoint
    {
        public Vector2 PlayerPosition;
        public Vector2 CrystalPosition;
    }
}