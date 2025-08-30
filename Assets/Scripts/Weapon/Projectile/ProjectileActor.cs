using UnityEngine;

namespace RobotBattle.Weapon
{
    public class ProjectileActor : MonoBehaviour
    {
        [SerializeField] private Transform _spawnLocatorMuzzleFlare;
        [SerializeField] private Transform[] _shotgunLocator;

        [SerializeField] private bool _torque = false;
        [SerializeField] private float _torMin;
        [SerializeField] private float _torMax;

        [SerializeField] private bool _minorRotate;
        [SerializeField] private bool _majorRotate = false;

        [HideInInspector] public float rapidFireCooldown;
        [HideInInspector] public int bombType = 0;

        private int _seq;
        private float _firingTimer;
        
        private ProjectileData _bomb;
        private Transform _transform;
        private PoolGroupHolder _buttonPool;
        private PoolGroupHolder _muzzlePool;

        public ProjectileData Bomb
        {
            get
            {
                if (!_bomb)
                {
                    _bomb = Resources.Load<ProjectileData>("Data/Projectiles/" + bombType);
                }

                return _bomb;
            }
        }

        public void Init(PoolModel poolModel, PoolOwner poolOwner)
        {
            var bullet = Bomb.bombPrefab.GetComponent<Bullet>();
            _buttonPool = poolModel.AddGroup<Bullet>(bullet, poolOwner, 100);

            var muzzle = Bomb.muzzleflare.GetComponent<BulletMuzzle>();
            _muzzlePool = poolModel.AddGroup<BulletMuzzle>(muzzle, poolOwner, 15);
        }

        private void Awake()
        {
            _transform = transform;
        }

        public void Fire(Vector3 target, float damage)
        {
            if (_firingTimer != 0 && !(_firingTimer < rapidFireCooldown + Time.time)) return;

            _firingTimer = Time.time;

            //Vector3 direction = ((target - _transform.position).normalized + _transform.forward * 0.1f).normalized;

            /*Instantiate(Bomb.muzzleflare, _spawnLocatorMuzzleFlare.position,
                _spawnLocatorMuzzleFlare.rotation);*/
            /*var muzzle = _muzzlePool.Get() as BulletMuzzle;
            muzzle.transform.SetPositionAndRotation(_spawnLocatorMuzzleFlare.position,
                _spawnLocatorMuzzleFlare.rotation);
            muzzle.gameObject.SetActive(true);*/

            // Rigidbody rocketInstance = Instantiate(Bomb.bombPrefab, _transform.position, _transform.rotation);
            //var rocketInstance = _buttonPool.Get() as Bullet;
            /*rocketInstance.transform.SetPositionAndRotation(_transform.position, _transform.rotation);
            var impulse = Random.Range(Bomb.min, Bomb.max);
            rocketInstance.Rigidbody.linearVelocity = impulse * transform.forward;
            //rocketInstance.Rigidbody.AddForce(rocketInstance.transform.forward * impulse, ForceMode.VelocityChange);
            rocketInstance.Init(damage, robotAchievementScript);
            rocketInstance.gameObject.SetActive(true);*/

            /*if (Bomb.shotgunBehavior)
            {
                for (int i = 0; i < Bomb.shotgunPellets; i++)
                {
                    Rigidbody rocketInstanceShotgun =
                        Instantiate(Bomb.bombPrefab, _shotgunLocator[i].position, _shotgunLocator[i].rotation);
                    // Quaternion.Euler(0,90,0)
                    rocketInstanceShotgun.AddForce(direction * Random.Range(Bomb.min, Bomb.max));
                }
            }*/

            if (_torque)
            {
               // rocketInstance.Rigidbody.AddTorque(_transform.up * Random.Range(_torMin, _torMax));
            }

            if (_minorRotate)
            {
                RandomizeRotation();
            }

            if (_majorRotate)
            {
                MajorRandomizeRotation();
            }
        }

        private void RandomizeRotation()
        {
            switch (_seq)
            {
                case 0:
                    _seq++;
                    _transform.Rotate(0, 1, 0);
                    break;
                case 1:
                    _seq++;
                    _transform.Rotate(1, 1, 0);
                    break;
                case 2:
                    _seq++;
                    _transform.Rotate(1, -3, 0);
                    break;
                case 3:
                    _seq++;
                    _transform.Rotate(-2, 1, 0);
                    break;
                case 4:
                    _seq++;
                    _transform.Rotate(1, 1, 1);
                    break;
                case 5:
                    _seq = 0;
                    _transform.Rotate(-1, -1, -1);
                    break;
            }
        }

        private void MajorRandomizeRotation()
        {
            switch (_seq)
            {
                case 0:
                    _seq++;
                    _transform.Rotate(0, 25, 0);
                    break;
                case 1:
                    _seq++;
                    _transform.Rotate(0, -50, 0);
                    break;
                case 2:
                    _seq++;
                    _transform.Rotate(0, 25, 0);
                    break;
                case 3:
                    _seq++;
                    _transform.Rotate(25, 0, 0);
                    break;
                case 4:
                    _seq++;
                    _transform.Rotate(-50, 0, 0);
                    break;
                case 5:
                    _seq = 0;
                    _transform.Rotate(25, 0, 0);
                    break;
            }
        }
    }
}