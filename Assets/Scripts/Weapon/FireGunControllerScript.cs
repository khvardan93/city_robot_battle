using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class FireGunControllerScript : AbstractWeaponScript
    {
        [SerializeField] private Transform _firePosition;
        private bool _isFlameActive = false;
        private List<ParticleSystem> _particleSystems = new();

        public int GetDamagePerSecond()
        {
            return 0;//WeaponParams.Capacity * WeaponParams.Damage / WeaponParams.FireDuration;
        }

        public override void Init(PoolModel poolModel, WeaponTargetScript weaponTargetScript,
            ShotGunSettings settings, Action<float, WeaponType, bool> callback)
        {
            base.Init(poolModel, weaponTargetScript, settings, callback);
            
            Timer = 10f;
            _firePosition.gameObject.SetActive(true);
            FindParticles(_firePosition);

            /*audioSource.clip = RobotSoundsScript.Instance.getFireSound();
            audioSource.loop = true;
            audioSource.playOnAwake = false;*/
        }

        public override void SetShooting(bool state)
        {
            base.SetShooting(state);
            SetParticles(state);
        }

        /*protected virtual void Update()
        {
            if (currentState == State.Reload && Timer <= Time.time)
            {
                currentState = State.Hold;
                Timer = WeaponParams.FireDuration;
            }

            if (currentState == State.Hold && _isShooting)
            {
                currentState = State.Shot;
            }

            if (currentState == State.Shot)
            {
                if (!_isShooting)
                {
                    currentState = State.Hold;
                }
                else if (Timer > 0 && !isFlameActive)
                {
                    DoShot();
                }
                else if (Timer <= 0)
                {
                    currentState = State.Reload;
                    Timer = Time.time + WeaponParams.ReloadTime;
                    OnReload?.Invoke(WeaponParams.ReloadTime, weaponType, true);
                }
            }
        }*/

        public override void Rotate(float direction, float rotateSpeed)
        {
            if (direction < 0 && (Transform.forward - StartForwardVector).y > 0.15f) return;
            if (direction > 0 && (Transform.forward - StartForwardVector).y < -0.15f) return;

            Transform.RotateAround(Transform.position, Transform.right,
                direction * rotateSpeed * Time.deltaTime);
        }

        protected override void DoShot()
        {
            //StartCoroutine(shooting());
        }

        private IEnumerator shooting()
        {
            _firePosition.gameObject.SetActive(true);

            SetParticles(true);
            setAudio(true);

            _isFlameActive = true;

            while (currentState == State.Shot)
            {
                yield return new WaitForEndOfFrame();
                Timer -= Time.deltaTime;
            }

            _isFlameActive = false;
            SetParticles(false);
            setAudio(false);
        }

        private void SetParticles(bool state)
        {
            foreach (var item in _particleSystems)
            {
                if (state)
                {
                    item?.Play();
                }
                else
                {
                    item?.Stop();
                }

                item.enableEmission = state;
            }
        }

        private void setAudio(bool state)
        {

            if (state)
            {
                _audioSource?.Play();
            }
            else
            {
                _audioSource?.Stop();
            }
        }

        private void FindParticles(Transform child)
        {
            var particle = child.GetComponent<ParticleSystem>();

            if (particle)
            {
                _particleSystems.Add(particle);
            }

            foreach (Transform item in child)
            {
                FindParticles(item);
            }
        }
    }
}