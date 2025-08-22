using UnityEngine;
using RobotBattle.Weapon;

namespace RobotBattle.Robot
{
    public class AbstractBehaviour
    {
        protected PlayerController PlayerController;
        protected WeaponTargetScript WeaponTarget;
        protected RobotAchievementScript RobotAchievement;

        public Transform Position => PlayerController.Torso;
        
        public AbstractBehaviour(PlayerController playerController)
        {
            PlayerController = playerController;

            WeaponTarget = new WeaponTargetScript();

            SetAchievementScript();
        }

        protected void SetAchievementScript()
        {
            RobotAchievement = new RobotAchievementScript(PlayerController.RobotType, this);
            GameManagerScript.Instance.robotAchievmentList.Add(RobotAchievement);
        }

        public void Warp(Vector3 position)
        {
        }

        protected void AddStatusBar()
        {
            /*Transform barParent = null;
            foreach (Transform child in transform)
            {
                if (child.CompareTag("StatusBar"))
                {
                    barParent = child;
                    break;
                }
            }

            if (barParent == null) Debug.LogError(name + " has not status bar parent");

            Instantiate(Resources.Load<RobotStatusBarScript>("RobotStatusBar"), barParent);*/
        }

        public virtual void OnDeath()
        {
        }
    }
}