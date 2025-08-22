using UnityEngine;

namespace RobotBattle.Garage
{
    public class GarageControllerScript : MonoBehaviour
    {
        [SerializeField] private GarageRobotScript[] _garageRobots;

        [SerializeField] private Camera _homeMenuCamera;
        [SerializeField] private Camera _skinMenuCamera;

        public static GarageControllerScript Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetCurrentRobot();
        }

        public void SetCurrentRobot()
        {
            foreach (var robot in _garageRobots)
            {
                robot.SetGameobjectStatus(robot.robotType == GameManagerScript.Instance.currentRobotType);
            }
        }

        public void SwitchToHomeCamera()
        {
            _homeMenuCamera.enabled = true;
            _skinMenuCamera.enabled = false;
        }

        public void SwitchToSkinCamera()
        {
            _homeMenuCamera.enabled = false;
            _skinMenuCamera.enabled = true;
        }
    }
}