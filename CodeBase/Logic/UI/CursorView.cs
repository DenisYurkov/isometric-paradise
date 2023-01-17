using CodeBase.Utility;
using UnityEngine;

namespace CodeBase.Logic
{
    public class CursorView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _cursorSprite;
        [SerializeField] private SpriteRenderer _selectSprite;

        private IGridHelper _gridHelper;

        public void Construct(IGridHelper gridHelper)
        {
            _gridHelper = gridHelper;
        }

        public void UpdateCursor(Texture2D texture2D) => 
            Cursor.SetCursor(texture2D, Vector2.down, CursorMode.Auto);

        public void ChangeSpriteColor(bool available) => 
            _selectSprite.color = available ? Color.white : Color.red;
        
        private void Update()
        {
            if (_gridHelper == null) return;
            Vector2 cellPosition = _gridHelper.GetCellPosition(Input.mousePosition);
            _cursorSprite.transform.position = cellPosition;
        }
    }
}