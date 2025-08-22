using System.Collections;
using RobotBattle.Robot;
using RobotBattle.Robot.Behaviours;
using UnityEngine;

namespace RobotBattle.Game
{
    public class BornPositionScript : MonoBehaviour
    {
        //[SerializeField] Team team;
        [SerializeField] RobotType[] robotTypes;
        [SerializeField] RobotBehaviour currentRobot;

        private void Start()
        {
            CreateRobot();
        }

        private void CreateRobot()
        {
            var newRobot =
                Instantiate(Resources.Load<PlayerController>("Robots/" 
                                                                  + (RobotType)Random.RandomRange(0, 8)));
            currentRobot = new RobotBehaviour(newRobot);
            currentRobot.Warp(transform.position);
            //currentRobot.startPosition = transform.position;
            //currentRobot.team = team;
            //currentRobot.name = team.ToString();
            newRobot.gameObject.SetActive(true);
        }
    }
}
