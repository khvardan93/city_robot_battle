using System.Collections;
using UnityEngine;

namespace RobotBattle.Weapon
{
    public class Bullet : BasePoolObject
    {
        [SerializeField] private Rigidbody _rigidbody;

        private PoolGroupHolder _impactPool;
        private BulletSettings _settings;
        
        public Rigidbody Rigidbody => _rigidbody;
        public float Damage => _settings.Damage;
        
        public void Init(PoolGroupHolder impactPool, BulletSettings settings)
        {
            _impactPool = impactPool;
            _settings = settings;
        }

        private void OnEnable()
        {
            if (_settings && _settings.ExplosionTimer > 0) StartCoroutine(Release(_settings.ExplosionTimer));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.LogError(collision.gameObject.layer);
            //if (collision.gameObject.tag == "FX") return;
            var contact = collision.contacts[0];
            var rot = Quaternion.FromToRotation(transform.forward, contact.normal);
            SetImpact(contact.point, rot);
            
            Release();
        }

        private void SetImpact(Vector3 pos, Quaternion rot)
        {
            var impact = _impactPool.Get<BulletImpact>();
            impact.transform.SetPositionAndRotation(pos, rot * Quaternion.Euler(0f, 180f, 0f));
            impact.gameObject.SetActive(true);
        }

        private IEnumerator Release(float delay)
        {
            yield return new WaitForSeconds(delay);

            Release();
        }

        public override void Release()
        {
            base.Release();
            StopAllCoroutines();
        }
    }
}