using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.GameMenu
{
    [RequireComponent(typeof(Image))]
    public class UICrossHair : MonoBehaviour
    {
        [SerializeField] float robotSelectedDistance = 150f;

        private Image crossHairImage;
        private Vector3 centerPosition;
        private Transform crossTransform;

        private void Start()
        {
            crossHairImage = GetComponent<Image>();
            crossTransform = crossHairImage.transform;
            centerPosition = crossTransform.position;
        }

        private void Update()
        {
            GameManagerScript.robotInFireArea = null;
            var closestItem = ScreenManager.Instance.GetClosestRobotOnScreen(centerPosition);
            
            if (
                closestItem &&
                closestItem.CheckTeam(Team.Team2) &&
                closestItem.Distance(centerPosition) < robotSelectedDistance)
            {
                SetActive(closestItem);
            }
            else
            {
                crossHairImage.color = Color.white;
                SetPosition(centerPosition);
            }
        }

        private void SetPosition(Vector3 targetPosition)
        {
            crossTransform.position = Vector3.Lerp(
                crossTransform.position,
                targetPosition,
                Time.deltaTime * 10f
            );
        }

        private void SetActive(UIRobotSelectorItem closestItem)
        {
            crossHairImage.color = Color.red;

            GameManagerScript.robotInFireArea = closestItem.getRealTransform();
            SetPosition(closestItem.transform.position);
        }
    }
}