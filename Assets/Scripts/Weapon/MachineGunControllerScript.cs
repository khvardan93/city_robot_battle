using System;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class MachineGunControllerScript : AbstractWeaponScript
    {
        [SerializeField] private Transform _spawnLocatorMuzzleFlare;
        [SerializeField] private Transform[] _shotgunLocator;

        [SerializeField] private float _shotInterval = 0.1f;
        [SerializeField] private bool _isShotgun;

        private bool _shootingAnimation;
        private BulletSettings _bulletSettings;
        
        private PoolGroupHolder _buttonPool;
        private PoolGroupHolder _muzzlePool;
        private PoolGroupHolder _impactPool;

        private void Start()
        {
            Ammo = int.MaxValue;
            Interval = _shotInterval;
        }

        public override void Init(PoolModel poolModel, PoolOwner owner, WeaponTargetScript weaponTargetScript,
            ShotGunSettings settings, Action<float, WeaponType, bool> callback)
        {
            base.Init(poolModel, owner, weaponTargetScript, settings, callback);
            
            _bulletSettings = settings.BulletSettings;

            _buttonPool = poolModel.AddGroup(_bulletSettings.BulletPrefab, owner, 100);
            _muzzlePool = poolModel.AddGroup(_bulletSettings.MuzzlePrefab, owner, 50);
            _impactPool = poolModel.AddGroup(_bulletSettings.ImpactPrefab, owner, 50);
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
            Fire();
        }

        private void Fire()
        {
            if (_isShotgun)
            {
                foreach (var locator in _shotgunLocator)
                {
                    ShotBullet(locator);
                }
            }
            else
            {
                ShotBullet(_spawnLocatorMuzzleFlare);
            }
        }

        private void ShotBullet(Transform point)
        {
            var muzzle = _muzzlePool.Get<BulletMuzzle>();
            muzzle.transform.SetPositionAndRotation(point.position, point.rotation * 
                                                                    Quaternion.Euler(0f, -90f, 0f));
            muzzle.gameObject.SetActive(true);
            
            var bullet = _buttonPool.Get<Bullet>();
            bullet.transform.SetPositionAndRotation(point.position, point.rotation);
            bullet.Rigidbody.linearVelocity = _bulletSettings.Speed * point.forward;
            bullet.Init(_impactPool, _bulletSettings);
            bullet.gameObject.SetActive(true);
            
            PlayShotSound();
        }
    }
}