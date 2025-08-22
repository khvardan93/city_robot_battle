using System;
using UnityEngine;

namespace RobotBattle.Weapon
{
    [Serializable]
    public struct WeaponParams
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private  int _damage;
        [SerializeField] private  int _fireDuration;
        [SerializeField] private  int _capacity;
        [SerializeField] private  int _reloadTime;
        [SerializeField] private  int _range;
        
        public WeaponParams(WeaponType weaponType, int damage, int fireDuration, int capacity, int reloadTime,
            int range)
        {
            _weaponType = weaponType;
            _damage = damage * 4;
            _fireDuration = fireDuration;
            _capacity = capacity;
            _reloadTime = reloadTime;
            _range = range;
        }

        public WeaponType WeaponType => _weaponType;
        public int Damage => _damage;
        public int FireDuration => _fireDuration;
        public int Capacity => _capacity;
        public int ReloadTime => _reloadTime;
        public int Range  => _range;
    }
}