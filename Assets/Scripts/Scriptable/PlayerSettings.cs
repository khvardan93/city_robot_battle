using RobotBattle.Robot;
using UnityEngine;

namespace RobotBattle
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private ShotGunSettings _shotGunSettings;
        [SerializeField] private NavMeshSettings _navMesh;
        [SerializeField] private PlayerController _playerPrefab;

        public ShotGunSettings ShotGunSettings => _shotGunSettings;
        public NavMeshSettings NavMesh => _navMesh;
        public PlayerController PlayerPrefab => _playerPrefab;
    }
}
