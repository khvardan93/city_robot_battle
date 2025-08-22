using System.Collections;
using System.Collections.Generic;
using RobotBattle.Weapon;
using UnityEngine;
using RobotBattle.UI.GameMenu;

namespace RobotBattle.Robot.Behaviours
{
    public class RobotBehaviour : AbstractBehaviour
    {
        private RobotOnScreen robotOnScreen;
        private bool isShooting;
        private bool isRotateTorso = false;
        private float reloadTimer;
   

        public RobotBehaviour(PlayerController playerController) : base(playerController)
        {
            robotOnScreen = new RobotOnScreen();
            robotOnScreen.positionReal = PlayerController.Torso;
            ScreenManager.Instance.AddRobot(robotOnScreen);

            AddStatusBar();
        }

        private void Update()
        {
            if (PlayerController._currentRobotMoveState != RobotMoveState.Falling)
            {
                if (isRotateTorso) rotateTorso();

                if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Transform &&
                    WeaponTarget.targetTransform == null)
                {
                    setTarget();
                }
            }
        }
        private void setTarget()
        {
            /*WeaponTarget.targetType = WeaponTargetScript.TargetType.Transform;
            WeaponTarget.targetTransform =
                GameManagerScript.Instance.getClosestEnemyTransform(transform.position);*/
        }

        private void rotateTorso()
        {
            try
            {
                var targetPosition = WeaponTarget.targetType == WeaponTargetScript.TargetType.Transform
                    ? WeaponTarget.targetTransform.position
                    : WeaponTarget.targetPosition;

                Vector3 direction = targetPosition - PlayerController.Torso.position;
                direction.y = 0;

                Vector3 right = PlayerController.Torso.right;
                right.y = 0;

                this.direction = (direction - right).magnitude < (direction + right).magnitude ? 50 : -50;
                Debug.DrawLine(targetPosition, PlayerController.Torso.position, Color.red, 4);
                PlayerController.RotateTorso((direction - right).magnitude < (direction + right).magnitude
                    ? 50
                    : -50);
            }
            catch
            {
            }
        }

        public float direction = 0;

        /*private IEnumerator checkEnemy()
        {
            isShooting = false;
            reloadTimer = -1;

            float seconds =  Random.Range(0, 30);

            yield return new WaitForSeconds(seconds);

            while (gameObject)
            {
                setTarget();

                if (reloadingWeaponsCount >= weaponsCount)
                {
                    RobotControllerScript.SetDestination(startPosition);
                    reloadingWeaponsCount = 0;

                    isShooting = false;
                    RobotControllerScript.SetAllWeapons(false);

                    Debug.Log("RELOADING!!!!!!!");
                    yield return new WaitUntil(() => reloadTimer <= Time.time);
                }

                yield return new WaitForSeconds(1);

                if (WeaponTarget.isSet)
                {
                    var targetPosition = WeaponTarget.targetType == WeaponTargetScript.TargetType.Transform
                        ? WeaponTarget.targetTransform.position
                        : WeaponTarget.targetPosition;

                    float distance = Vector3.Distance(RobotControllerScript.Torso.position, targetPosition);

                    RobotControllerScript.SetDestination(targetPosition);

                    if (Vector3.Distance(targetPosition, RobotControllerScript.Torso.position) < 80)
                    {
                        angle = getAngle(targetPosition);

                        isRotateTorso = angle > 10;

                        if (!isRotateTorso && !isShooting &&
                            RobotControllerScript.currentState != RobotControllerScript.State.Walk && distance < 70)
                        {
                            isShooting = true;
                            //Debug.DrawLine(targetPosition, robotControllerScript.torso.position, Color.red, 4); 
                            RobotControllerScript.SetAllWeapons(true);
                        }
                        else if (isShooting && RobotControllerScript.currentState == RobotControllerScript.State.Walk ||
                                 isRotateTorso)
                        {
                            isShooting = false;
                            RobotControllerScript.SetAllWeapons(false);
                        }
                    }
                }
            }
        }*/

        private float getAngle(Vector3 targetPosition)
        {
            float angle = 0;

            Vector3 direction = targetPosition - PlayerController.Torso.position;
            direction.y = 0;

            Vector3 forward = PlayerController.Torso.forward;
            forward.y = 0;

            Vector3 right = PlayerController.Torso.right;
            right.y = 0;

            angle = Vector3.Angle(forward, direction);

            if (angle < 10 && (direction + forward).magnitude < (direction - forward).magnitude) angle += 180;

            return angle;
        }

        private void OnDestroy()
        {
            robotOnScreen.isDestroyed = true;
            RobotAchievement.isDead = true;
        }
    }
}