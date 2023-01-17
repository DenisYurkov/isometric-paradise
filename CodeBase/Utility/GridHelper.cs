using System;
using CodeBase.Logic;
using UnityEngine;
using NetworkPlayer = CodeBase.Logic.NetworkPlayer;

namespace CodeBase.Utility
{
    public interface IGridHelper
    {
        Vector3Int GetGridPosition(Vector2 mousePosition);
        Vector2 GetCellPosition(Vector2 mousePosition);
        bool ColliderRaycast(Vector2 mousePosition);
        bool ObstacleRaycast(Vector2 mousePosition);
        RaycastHit2D GetRaycast();
    }

    public class GridHelper : IGridHelper
    {
        private RaycastHit2D _raycastHit;
        private readonly Grid _grid;
        private readonly Camera _camera;

        public GridHelper(Grid grid, Camera camera)
        {
            _camera = camera;
            _grid = grid;
        }

        public Vector3Int GetGridPosition(Vector2 mousePosition)
        {
            Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
            return _grid.WorldToCell(worldPos);
        }

        public Vector2 GetCellPosition(Vector2 mousePosition) =>
            _grid.GetCellCenterWorld(GetGridPosition(mousePosition));

        public bool ColliderRaycast(Vector2 mousePosition)
        {
            UpdateRaycast(Physics2D.Raycast(GetCellPosition(mousePosition), Vector2.zero));
            return _raycastHit.collider != null && !_raycastHit.collider.TryGetComponent(out NetworkPlayer networkPlayer);
        }

        public bool ObstacleRaycast(Vector2 mousePosition)
        {
            UpdateRaycast(Physics2D.Raycast(GetCellPosition(mousePosition), Vector2.zero));
            return _raycastHit.collider != null &&
                   _raycastHit.collider.TryGetComponent(out NetworkObstacle networkObstacle);
        }


        public RaycastHit2D GetRaycast()
        {
            if (!_raycastHit.collider)
                throw new NullReferenceException($"Raycast is null!");

            return _raycastHit;
        }
        
        private void UpdateRaycast(RaycastHit2D raycastHit2D) => 
            _raycastHit = raycastHit2D;
    }
}