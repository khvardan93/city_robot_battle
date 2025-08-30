using System;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public abstract class AbstractWeaponScript : MonoBehaviour
    {
        [SerializeField] protected WeaponType _weaponType;
        [Space]
        [SerializeField] protected AudioSource _audioSource;
        [SerializeField] protected AudioClip _audioClip;

        public WeaponType WeaponType;
        public bool Rotatable;

        public enum State
        {
            Hold,
            Shot,
            Reload
        };

        public State currentState;
        protected bool _isShooting = false;

        protected Action<float, WeaponType, bool> OnReload;
        protected float Timer;
        protected int Ammo;

        protected WeaponTargetScript WeaponTarget;


        protected Transform Transform;
        protected Vector3 StartForwardVector;

        protected float Interval;

        private void Awake()
        {
            Transform = transform;
            StartForwardVector = Transform.forward;

            if (_audioSource)
                _audioSource.spatialBlend = 1;
        }

        protected virtual void Update()
        {
            if (currentState == State.Reload && Timer <= Time.time)
            {
                currentState = State.Hold;
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
                else if (Ammo > 0 && Timer <= Time.time)
                {
                    Ammo--;
                    Timer = Time.time + Interval;
                    DoShot();

                    if (Ammo <= 0)
                    {
                        currentState = State.Reload;
                    }
                    else
                    {
                        OnReload?.Invoke(Interval, _weaponType, false);
                    }
                }
            }
        }

        public virtual void Init(PoolModel poolModel, PoolOwner owner, WeaponTargetScript weaponTargetScript,
            ShotGunSettings settings, Action<float, WeaponType, bool> callbeck)
        {
            WeaponTarget = weaponTargetScript;
            OnReload = callbeck;
            currentState = State.Hold;
        }

        public virtual void Rotate(float direction, float rotateSpeed) { }

        protected virtual void DoShot() { }

        protected void PlayShotSound()
        {
            _audioSource.PlayOneShot(_audioClip);
        }

        public virtual void SetShooting(bool state)
        {
            _isShooting = state;
        }
    }
}