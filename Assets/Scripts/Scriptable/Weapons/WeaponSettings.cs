using UnityEngine;

namespace RobotBattle
{
    public abstract class WeaponSettings : ScriptableObject
    {
        public virtual WeaponType WeaponType { get; }
    }
}
