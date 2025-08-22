using System.Collections;
using RobotBattle.Robot;
using RobotBattle.Robot.Behaviours;
using UnityEngine;

namespace RobotBattle.Game
{
    public class StartPositionScript : MonoBehaviour
    {
        public static StartPositionScript instance { internal set; get; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            /*var newRobot =
                Instantiate(Resources.Load<RobotControllerScript>("Robots/" + GameManagerScript.Instance.currentRobotType));

            var behaviour = new PlayerBehaviourScript(newRobot);
            newRobot.Init(behaviour);
            behaviour.Warp(transform.position);
            newRobot.gameObject.SetActive(true);*/
        }
    }
}