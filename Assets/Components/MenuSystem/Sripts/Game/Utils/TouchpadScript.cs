using UnityEngine;
using UnityEngine.EventSystems;

namespace RobotBattle.UI.GameMenu
{
    public class TouchpadScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        private bool isActive;

        public void OnPointerDown(PointerEventData eventData)
        {
            isActive = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isActive = false;
            InputUtil.LookChangeVector = Vector2.zero;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (isActive)
            {
                InputUtil.LookChangeVector = eventData.delta;
            }
        }
    }
}