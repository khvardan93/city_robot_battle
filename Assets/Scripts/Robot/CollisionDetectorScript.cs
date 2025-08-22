using RobotBattle.Weapon;
using UnityEngine;

namespace RobotBattle.Robot
{
    public class CollisionDetectorScript : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        private bool _isFireDamage;
        private float _fireDamage;
        private RobotAchievementScript robotAchievementScript;
        private PlayerController _playerController;

        public void Setup(PlayerController playerController)
        {
            _playerController = playerController;
            enabled = true;
            _collider.enabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bulletDamageScript = collision.gameObject.GetComponent<Bullet>();
            if (bulletDamageScript)
            {
                _playerController.OnHit(bulletDamageScript.Damage, bulletDamageScript.RobotAchievmentScript);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.GetComponentInParent<PlayerController>().GetInstanceID() ==
                GetComponentInParent<PlayerController>().GetInstanceID()) return;
            
            _isFireDamage = true;
            var fireGunControllerScript = other.GetComponentInParent<FireGunControllerScript>();
            _fireDamage = fireGunControllerScript.GetDamagePerSecond();
            //robotAchievementScript = fireGunControllerScript._robotAchievementScript;
        }

        private void Update()
        {
            if (!_isFireDamage) return;
            
            _playerController.OnHit(Time.deltaTime * _fireDamage, robotAchievementScript);
            _isFireDamage = false;
        }
    }
}