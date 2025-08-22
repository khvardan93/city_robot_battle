using UnityEngine;

namespace RobotBattle
{
    [CreateAssetMenu(fileName = "NavMeshSettings", menuName = "Settings/NavMeshSettings")]
    public class NavMeshSettings : ScriptableObject
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _stoppingDistance = 5f;
        
        [SerializeField] private bool _updateRotation;
        [SerializeField] private bool _updatePosition;
        
        public float Speed => _speed;
        public float StoppingDistance => _stoppingDistance;
        public bool UpdateRotation => _updateRotation;
        public bool UpdatePosition => _updatePosition;
    }
}
