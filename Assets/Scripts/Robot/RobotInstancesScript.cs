using System;
using RobotBattle.Weapon;
using UnityEngine;

namespace RobotBattle.Robot
{
    public class RobotInstancesScript : MonoBehaviour
    {
        [SerializeField] private Transform _torso;
        [SerializeField] private Forge3D.Forcefield_Mobile _shield;
        [SerializeField] private CollisionDetectorScript _collisionDetectorScript;
        [SerializeField] private Animator _animator;
        [SerializeField] private AbstractWeaponScript[] _weapons;
        
        public Transform Torso => _torso;
        public Forge3D.Forcefield_Mobile Shield => _shield;
        public CollisionDetectorScript CollisionDetectorScript => _collisionDetectorScript;

        #region Weapons
        public void SetupWeapons(PoolModel poolModel, ShotGunSettings weaponSettings,  
            WeaponTargetScript weaponTargetScript, Action<float,WeaponType,bool> action)
        {
            foreach (var weapon in _weapons)
            {
                weapon.Init(poolModel, weaponTargetScript, weaponSettings, action);
            }
        }
        
        public void SetAllWeapons(bool state)
        {
            foreach (var item in _weapons)
            {
                item.SetShooting(state);
            }
        }
        
        public void RotateWeapons(float direction)
        {
            foreach (var item in _weapons)
            {
                if (item.Rotatable) item.Rotate(direction, Time.deltaTime);
            }
        }
        
        public void SetWeapons(WeaponType type, bool state)
        {
            foreach (var item in _weapons)
            {
                if(item.WeaponType == type)
                    item.SetShooting(state); 
            }
        }
        #endregion
        
        #region Animator
        public float GetAnimationStateDuration()
        {
            var currInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return currInfo.length;
        }

        public void SetWalkState(bool state)
        {
            const string stateName = "Walk";
            _animator.SetBool(stateName, state);
        }  
        
        public void SetBackState(bool state)
        {
            const string stateName = "Back";
            _animator.SetBool(stateName, state);
        }  
        
        public void SetIdleState(bool state)
        {
            const string stateName = "Idle";
            _animator.SetBool(stateName, state);
        }
        
        public void SetLeftState(bool state)
        {
            const string stateName = "Left";
            _animator.SetBool(stateName, state);
        }
        
        public void SetRightState(bool state)
        {
            const string stateName = "Right";
            _animator.SetBool(stateName, state);
        }
        
        public void SetTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void ResetTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }
        #endregion
    }
}