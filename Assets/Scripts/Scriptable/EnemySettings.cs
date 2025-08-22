using RobotBattle.Enemy;
using UnityEngine;

namespace RobotBattle
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _health = 100f;
        [SerializeField] private NavMeshSettings _navMesh;
        [SerializeField] private ShotGunSettings _shotgun;
        [SerializeField] private EnemyBase _enemyPrefab;

        public EnemyType EnemyType => _enemyType;
        public float Health => _health;
        public NavMeshSettings NavMesh => _navMesh;
        public ShotGunSettings Shotgun => _shotgun;
        public EnemyBase EnemyPrefab => _enemyPrefab;
    }
}
