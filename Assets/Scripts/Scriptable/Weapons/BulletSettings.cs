using RobotBattle.Weapon;
using UnityEngine;

namespace RobotBattle
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Settings/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletMuzzle _muzzlePrefab;
        [SerializeField] private BulletImpact _impactPrefab;
        [Space]
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _explosionTimer = 3f; 

        public Bullet BulletPrefab => _bulletPrefab;
        public BulletMuzzle MuzzlePrefab => _muzzlePrefab;
        public BulletImpact ImpactPrefab => _impactPrefab;
        public float Speed => _speed;
        public float Damage => _damage;
        public float ExplosionTimer => _explosionTimer;
    }
}
