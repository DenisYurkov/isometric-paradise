using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Logic
{
    [Serializable]
    public struct Map
    {
        [SerializeField] private Tilemap _groundMap;

        public readonly bool HasTile(Vector3Int gridPosition) => 
            _groundMap.HasTile(gridPosition);
    }
}