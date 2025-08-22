using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.GameMenu
{
    public class UIRobotSelectorItem : MonoBehaviour
    {
        private RobotOnScreen robotPosition;
        private RectTransform rectTransform;
        private RectTransform rootCanvasRect;
        private Image selectorImage;
        [SerializeField] Sprite redSelector;
        [SerializeField] Sprite greenSelector;
        [Space] [SerializeField] Sprite redArrow;
        [SerializeField] Sprite greenArrow;
        [SerializeField] RectTransform arrow;

        public bool isActive()
        {
            return !robotPosition.isDestroyed && robotPosition.isVisible;
        }

        public Transform getRealTransform()
        {
            return robotPosition.positionReal;
        }

        public bool CheckTeam(Team cTeam)
        {
            return robotPosition.team == cTeam;
        }

        public float Distance(Vector3 cPosition)
        {
            return Vector3.Distance(cPosition, transform.position);
        }

        public void init(RobotOnScreen robotPosition, RectTransform rootCanvasRect)
        {
            this.rootCanvasRect = rootCanvasRect;
            this.robotPosition = robotPosition;
            robotPosition.isInited = true;

            rectTransform = GetComponent<RectTransform>();
            selectorImage = GetComponent<Image>();
            arrow.GetComponent<Image>().sprite = robotPosition.team == Team.Team1 ? greenArrow : redArrow;

            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (robotPosition.isDestroyed)
            {
                Destroy(gameObject);
            }
            else if (robotPosition.isVisible)
            {
                selectorImage.enabled = true;
                selectorImage.sprite = robotPosition.team == Team.Team1 ? greenSelector : redSelector;
                arrow.gameObject.SetActive(false);
                Vector2 screenPos = robotPosition.positionUI;
                screenPos.x *= rootCanvasRect.rect.width;
                screenPos.y *= rootCanvasRect.rect.height;

                SetPosition(screenPos);
            }
            else
            {
                arrow.gameObject.SetActive(true);
                selectorImage.enabled = false;

                setArrowPosition();
            }
        }

        private void setArrowPosition()
        {
            Vector2 screenPos = robotPosition.positionUI;

            if (robotPosition.direction == Vector2.up)
            {
                screenPos.x *= rootCanvasRect.rect.width;
                screenPos.y = rootCanvasRect.rect.height - 20;
                arrow.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (robotPosition.direction == Vector2.down)
            {
                screenPos.x *= rootCanvasRect.rect.width;
                screenPos.y = 20;
                arrow.localRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (robotPosition.direction == Vector2.right)
            {
                screenPos.x = rootCanvasRect.rect.width - 20;
                screenPos.y *= rootCanvasRect.rect.height;
                arrow.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (robotPosition.direction == Vector2.left)
            {
                screenPos.x = 20;
                screenPos.y *= rootCanvasRect.rect.height;
                arrow.localRotation = Quaternion.Euler(0, 0, 90);
            }

            SetPosition(screenPos);
        }

        private void SetPosition(Vector2 newPosition)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                newPosition,
                Time.deltaTime * 10f
            );
        }
    }
}