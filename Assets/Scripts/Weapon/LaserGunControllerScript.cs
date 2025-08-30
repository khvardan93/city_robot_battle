using System;
using System.Collections;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class LaserGunControllerScript : AbstractWeaponScript
    {
        [SerializeField] ProjectileActor _shooter;

        private bool _shootingAnimation;

        public override void Init(PoolModel poolModel, PoolOwner owner, WeaponTargetScript weaponTargetScript, 
            ShotGunSettings settings, Action<float, WeaponType, bool> callback)
        {
            base.Init(poolModel, owner, weaponTargetScript, null, callback);

            _shooter.bombType = 48;

            _shooter.rapidFireCooldown = 0;//weaponParams.FireDuration / (float)weaponParams.Capacity;

            //TODO remove after fixing values
            Interval /= 2f;

            _audioSource.clip = RobotSoundsScript.Instance.getLaserSound();
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
            if (!_shootingAnimation)
            {
                StartCoroutine(Wait());
            }

            _audioSource.Play();

            if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Position)
            {
                _shooter.Fire(WeaponTarget.targetPosition, 0);
            }
            else if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Transform)
            {
                _shooter.Fire(WeaponTarget.targetTransform.position,0);
            }
        }

        private IEnumerator Wait()
        {
            _shootingAnimation = true;
            if (_audioSource)

                yield return new WaitWhile(() => currentState == State.Shot);

            if (_audioSource) _audioSource.Stop();
            _shootingAnimation = false;
        }
    }
}