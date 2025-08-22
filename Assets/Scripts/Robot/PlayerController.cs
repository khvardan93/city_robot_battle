using System;
using RobotBattle.Game;
using UnityEngine;
using RobotBattle.Weapon;

namespace RobotBattle.Robot
{
    public partial class PlayerController : SingleObjectBase<PlayerController>
    {
        [SerializeField] private RobotType _robotType;
        [SerializeField] private RobotInstancesScript _robot;

        [SerializeField] private SerializableGuid _guid;

        private float _shieldHealth = 100;
        private float _maxShieldHealth = 100;
        private float _health = 100;
        private float _maxHealth = 100;

        private Transform _transform;

        private Transform _target;
        private Vector3 _torsoDirection;
        
        private PlayerSettings _settings;

        public RobotType RobotType => _robotType;
        public Transform Torso => _robot.Torso;
        public float Health => _health;
        public float ShieldHealth => _shieldHealth;
        
        public Guid Id => _guid.Guid;

        public RobotMoveState _currentRobotMoveState = RobotMoveState.Idle;

        private bool _isFixingLegs;

        private RobotAchievementScript _lastKilledRobotScript;

        public void Init(PlayerSettings settings)
        {
            _navMesh.Init(settings.NavMesh);
        }
        
        private void Awake()
        {
            _robot.Shield.SetupShield(Id, OnShieldHit);
            _robot.CollisionDetectorScript.Setup(this);

            _transform = transform;

            _target = new GameObject("Target").transform;
            _torsoDirection = _robot.Torso.forward;

        }

        public float GetHealthInPercents()
        {
            return 100f * Mathf.Clamp01(_health / _maxHealth);
        }

        public float GetShieldHealthInPercents()
        {
            return 100f * Mathf.Clamp01(_shieldHealth / _maxShieldHealth);
        }

        private void SetTargetPosition()
        {
            _target.position = _torsoDirection * 10 + _robot.Torso.position;
            _robot.Torso.LookAt(_target, _robot.Torso.up);
        }

        private void FixLegs()
        {
            if (Vector3.Angle(_transform.forward, _robot.Torso.forward) > 3)
            {

                RotateRobot((_transform.forward - _robot.Torso.right).magnitude >
                            (_transform.forward + _robot.Torso.right).magnitude);
                SetTargetPosition();
            }
        }

        private void RotateRobot(bool axis)
        {
            _transform.Rotate((axis ? _transform.up : -_transform.up), Time.deltaTime * 150f);
        }

        public void RotateTorso(float direction)
        {
            if (direction != 0)
            {
                _robot.Torso.Rotate(_robot.Torso.up, direction * Time.deltaTime);
                _torsoDirection = _robot.Torso.forward;
            }
        }

        public GameObject GetShield()
        {
            return _robot.Shield.gameObject;
        }

        public int SetParams(WeaponTargetScript weaponTargetScript, Action<float, WeaponType, bool> action)
        {
            var robotParams = Robots.instance.getRobotByType(_robotType);

            _health = robotParams.health;
            _maxHealth = robotParams.health;
            _shieldHealth = robotParams.shieldHealth;
            _maxShieldHealth = robotParams.shieldHealth;
            //set weapons
            var colliders = gameObject.GetComponentsInChildren<Collider>();
            var pool = System.Instance.GetModel<PoolModel>();

            _robot.SetupWeapons(pool, robotParams, weaponTargetScript, action);

            return 0;//_shoulderWeapons.Length;
        }

        #region WEAPONS

        public void SetAllButtons(bool state)
        {
            _robot.SetAllWeapons(state);
        }

        public void SetWeapons(WeaponType type, bool state)
        {
            _robot.SetWeapons(type, state);
        }

        public void SetWeapons(bool state)
        {
            _robot.SetAllWeapons(state);
        }

        public void OnShieldHit(float damage)
        {
            _shieldHealth -= damage;

            if (_shieldHealth <= 0)
            {
                _robot.Shield.gameObject.SetActive(false);
            }
        }

        public void OnHit(float damage, RobotAchievementScript robotAchievementScript)
        {
            _health -= damage;
            robotAchievementScript.damage += damage;
            if (robotAchievementScript.isPlayer())
                TaskManagerScript.instance.increaseDamageSize(robotAchievementScript.robotType, damage);

            if (_health <= 0)
            {
                Destroy(gameObject, 2);
                if (_lastKilledRobotScript != robotAchievementScript)
                {
                    robotAchievementScript.killedCount++;
                    _lastKilledRobotScript = robotAchievementScript;
                }

                TaskManagerScript.instance.increaseDestroyedRobotCount(robotAchievementScript.robotType);

                StopAllCoroutines();
                GetComponent<AbstractBehaviour>().OnDeath();

                _robot.SetAllWeapons(false);
            }
        }

        #endregion
    }
}