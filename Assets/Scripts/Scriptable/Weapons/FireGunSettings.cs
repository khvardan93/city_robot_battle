using UnityEngine;

namespace RobotBattle.Weapon
{
    [CreateAssetMenu(fileName = "FireGunSettings", menuName = "Settings/FireGunSettings")]
    public class FireGunSettings : WeaponSettings
    {
        [SerializeField] private float _fuelSpendSpeed;
        [SerializeField] private float _fuelRestoreSpeed;
        [SerializeField] private float _minPercentToFire;
        
        public float FuelSpendSpeed => _fuelSpendSpeed;
        public float FuelRestoreSpeed => _fuelRestoreSpeed;
        public float MinPercentToFire => _minPercentToFire;
        
        public override WeaponType WeaponType => WeaponType.Fire;
    }
}
