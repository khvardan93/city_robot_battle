using System.Collections;
using System.Collections.Generic;
using RobotBattle.Generic;
using RobotBattle.Game;
using UnityEngine;

namespace RobotBattle.UI.GameMenu
{
    public class ScreenManager : BaseMonoBehaviourSingleton<ScreenManager>
    {
        [SerializeField] private RectTransform robotSelector;
        
        private List<UIRobotSelectorItem> robotSelectorList = new();
        public List<RobotOnScreen> robotsOnScreen = new();
        
        //private Camera mainCamera;
        private RectTransform rootCanvasRect;
        private  UIRobotSelectorItem selectorExample;
        
        private void Start()
        {
            //mainCamera = CameraController.Instance.GetComponent<Camera>();
            rootCanvasRect = GetComponent<RectTransform>();
            selectorExample = Resources.Load<UIRobotSelectorItem>("UI/UIRobotSelectorItem");

            StartCoroutine(CheckRobots());
        }

        private void Update()
        {
            foreach (var item in robotsOnScreen)
            {
                if (!item.isDestroyed && item.isVisible)
                {
                    //item.positionUI = mainCamera.WorldToViewportPoint(item.positionReal.position);
                }
            }
        }
        
        private IEnumerator CheckRobots()
        {
            var updateRate = new WaitForSeconds(0.25f);

            while (gameObject)
            {
                yield return updateRate;

                foreach (var item in robotsOnScreen)
                {
                    //create if new
                    if (!item.isInited)
                    {
                        var newSelector = Instantiate(selectorExample, robotSelector);
                        newSelector.init(item, rootCanvasRect);
                        robotSelectorList.Add(newSelector);
                    }

                    //check visibility
                    if (!item.isDestroyed)
                    {
                        CheckVisibility(item);
                    }
                }

                GameManagerScript.robotInFireArea = null;
            }
        }

        public void AddRobot(RobotOnScreen newRobot)
        {
            robotsOnScreen.Add(newRobot);
        }

        private void CheckVisibility(RobotOnScreen item)
        {
            /*item.positionUI = mainCamera.WorldToViewportPoint(item.positionReal.position);
            var direction =
                (mainCamera.transform.forward + (item.positionReal.position - mainCamera.transform.position).normalized)
                .magnitude;

            if (direction >= 1 && item.positionUI.z > 0 && item.positionUI.x > 0 && item.positionUI.x < 1 &&
                item.positionUI.y > 0 && item.positionUI.y < 1)
            {
                item.isVisible = true;
            }
            else
            {
                item.SetItemDirection(direction);
                item.isVisible = false;
            }*/
        }

        public (int friends, int enemies) GetLeftRobotsCount()
        {
            var counts = (friends: 1, enemies: 0);

            foreach (var item in robotsOnScreen)
            {
                if (item.team == Team.Team1 && !item.isDestroyed)
                {
                    counts.friends++;
                }
                else if (item.team == Team.Team2 && !item.isDestroyed)
                {
                    counts.enemies++;
                }
            }

            return counts;
        }
        
        public UIRobotSelectorItem GetClosestRobotOnScreen(Vector3 cPosition)
        {
            UIRobotSelectorItem closestItem = null;
            float distance = float.MaxValue;
            
            foreach (var item in robotSelectorList)
            {
                if (
                    item &&
                    item.isActive() &&
                    item.CheckTeam(Team.Team2) &&
                    item.Distance(cPosition) < distance
                )
                {
                    closestItem = item;
                    distance = item.Distance(cPosition);
                }
            }

            return closestItem;
        }
    }
}