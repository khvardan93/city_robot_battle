using System.Collections;
using System;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class ShotRocketControllerScript : AbstractWeaponScript
    {
        [SerializeField] protected Transform[] startPositions;
        protected int CurrentPositionIndex;
        protected bool ShootingAnimation;

        public override void Init(PoolModel poolModel, WeaponTargetScript weaponTargetScript, 
            ShotGunSettings settings, Action<float, WeaponType, bool> callback)
        {
            base.Init(poolModel, weaponTargetScript, settings, callback);

            //TODO remove after fixing values
            Interval /= 2f;

            SetAudio();
        }

        protected virtual void SetAudio()
        {
            /*audioSource.clip = RobotSoundsScript.Instance.getRocketSound();
            audioSource.playOnAwake = false;
            audioSource.loop = false;*/
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
            _audioSource.Play();

            if (!ShootingAnimation)
            {
                StartCoroutine(Wait());
            }

            var rocket = Instantiate(Resources.Load<RWSP_Rocket>("Rocket"));
            rocket.transform.parent = null;
            rocket.transform.position = startPositions[CurrentPositionIndex].position;
            rocket.SetStartingPosition(startPositions[CurrentPositionIndex].position);
            rocket.transform.LookAt(startPositions[CurrentPositionIndex].forward * 100 +
                                    startPositions[CurrentPositionIndex].position);
            rocket.GuidanceType = RocketGuidanceTypes.Homing;

            CurrentPositionIndex++;
            if (CurrentPositionIndex >= startPositions.Length) CurrentPositionIndex = 0;

            if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Position)
            {
                rocket.Fire(gameObject, null, true, Transform.position, 0.1f);
            }
            else if (WeaponTarget.targetType == WeaponTargetScript.TargetType.Transform)
            {
                rocket.transform.LookAt(WeaponTarget.targetTransform.position);
                rocket.Fire(gameObject, null, true, Transform.position, 0.1f);
            }
        }

        protected IEnumerator Wait()
        {
            ShootingAnimation = true;
            if (_audioSource) _audioSource.Play();

            yield return new WaitWhile(() => currentState == State.Shot);

            if (_audioSource) _audioSource.Stop();
            ShootingAnimation = false;
        }
    }
}