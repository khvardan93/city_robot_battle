using UnityEngine;

namespace RobotBattle
{
    [CreateAssetMenu(fileName = "ShotGunSettings", menuName = "Settings/ShotGunSettings")]
    public class ShotGunSettings : WeaponSettings
    {
        [SerializeField] private BulletSettings _bulletSettings;
        
        public BulletSettings BulletSettings => _bulletSettings;
        public override WeaponType WeaponType => WeaponType.Shot;
    }
} 
