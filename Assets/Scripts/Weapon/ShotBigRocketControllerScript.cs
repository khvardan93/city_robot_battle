using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class ShotBigRocketControllerScript : ShotRocketControllerScript
    {
        protected override void SetAudio()
        {
            _audioSource.clip = RobotSoundsScript.Instance.getBigRocketSound();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
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
            //rocket.GetComponent<Bullet>().Init(null, WeaponParams.Damage, _robotAchievementScript);
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
                rocket.Fire(gameObject, WeaponTarget.targetTransform, true, Transform.position, 0.1f);
            }
        }
    }
}