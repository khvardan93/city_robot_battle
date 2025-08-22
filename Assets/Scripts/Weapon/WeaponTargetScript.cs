using UnityEngine;

namespace RobotBattle.Weapon
{
    [global::System.Serializable]
    public class WeaponTargetScript
    {
        public enum TargetType
        {
            Position,
            Transform,
            Direction
        };

        public TargetType targetType;

        public Vector3 targetPosition;
        public Transform targetTransform;
        public Vector3 targetDirection;

        public bool isSet => targetType == TargetType.Transform && targetTransform != null ||
                             targetType == TargetType.Position && targetPosition != Vector3.zero ||
                             targetType == TargetType.Direction && targetDirection != Vector3.zero;
    }
}