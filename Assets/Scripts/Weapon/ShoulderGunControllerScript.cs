using System;
using System.Collections;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class ShoulderGunControllerScript : AbstractWeaponScript
    {
        [SerializeField] ProjectileActor shooter;

        private bool shootingAnimation;

        public override void Init(PoolModel poolModel, WeaponTargetScript weaponTargetScript, 
            ShotGunSettings settings, Action<float, WeaponType, bool> callback)
        {
            base.Init(poolModel, weaponTargetScript, settings, callback);

            shooter.rapidFireCooldown = 0;//(float)weaponParams.FireDuration / (float)weaponParams.Capacity;

            _audioSource.clip = RobotSoundsScript.Instance.getMiniGunSound();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        public override void Rotate(float direction, float rotateSpeed)
        {
            if (direction < 0 && (Transform.forward - StartForwardVector).y > 0.15f) return;
            if (direction > 0 && (Transform.forward - StartForwardVector).y < -0.15f) return;

            Transform.RotateAround(Transform.position, Transform.right,
                direction * rotateSpeed * Time.deltaTime);
        }

        protected override void DoShot()
        {
            if (!shootingAnimation)
            {
                StartCoroutine(wait());
            }

            _audioSource.Play();

            if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Position)
            {
                //if(robotAchievmentScript.isPlayer()) Debug.DrawLine(transform.position, weaponTarget.targetPosition, Color.red, 5f);
                shooter.Fire(WeaponTarget.targetPosition, 0);
            }
            else if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Transform)
            {
                //if (robotAchievmentScript.isPlayer()) Debug.DrawLine(transform.position, weaponTarget.targetTransform.position, Color.red, 5f);
                shooter.Fire(WeaponTarget.targetTransform.position, 0);
            }
        }

        private IEnumerator wait()
        {
            shootingAnimation = true;
            if (_audioSource) _audioSource.Play();

            yield return new WaitWhile(() => currentState == State.Shot);

            if (_audioSource) _audioSource.Stop();
            shootingAnimation = false;
        }
    }
}