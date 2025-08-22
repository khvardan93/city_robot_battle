using RobotBattle.Robot;
using UnityEngine;
using UnityEngine.AI;

namespace RobotBattle.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        protected enum State
        {
            Hold,
            Chase,
            Attack
        }

        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private NavMeshController _navMesh;

        public EnemyType EnemyType => _enemyType;
        
        private float _scrDetectionRange = float.MaxValue;
        private float _scrShootingRange = 26;
        private float _scrKeepDistance;
        
        protected State CurrentState = State.Hold;
        protected Transform Transform;
        protected Transform Player;
        protected EnemySettings Settings;

        public void Init(EnemySettings settings)
        {
            Settings = settings;
        }
        
        protected virtual void Start()
        {
            Transform = transform;
            System.Instance.ObjectRegister.Get<PlayerController>((obj) =>
            {
                Player = ((PlayerController)obj).transform;
            });
        }

        protected void Update()
        {
            var state = CheckPlayerState();
            
            if(state == CurrentState) return;
            
            CurrentState = state;

            switch (state)
            {
                case State.Hold:
                    SetHoldState();
                    break;
                case State.Chase:
                    SetChaseState();
                    break;
                case State.Attack:
                    SetAttackState();
                    break;
            }
        }

        private State CheckPlayerState()
        {
            if (!Player) return State.Hold;
            
            var player = Player;
            var tr = Transform;

            var scrDistance = (tr.position - player.position).sqrMagnitude;

            if (scrDistance > _scrDetectionRange) return State.Hold;

            return scrDistance > _scrShootingRange ? State.Chase : State.Attack;
        }

        protected virtual void SetHoldState()
        {
            _navMesh.SetStopped();
        }

        protected virtual void SetChaseState()
        {
            _navMesh.GoToTarget(Player.position);
            SetDirection();
        }

        protected virtual void SetAttackState()
        {
            var agent = _navMesh;
            var player = Player;
            var tr = Transform;
            

            // Maintain distance (stop at keepDistance instead of colliding)
            var direction = (tr.position - player.position).normalized;
            var targetPos = player.position + direction * _scrKeepDistance;
            agent.GoToTarget(targetPos);

            SetDirection();
        }

        private void SetDirection()
        {
            var tr = Transform;

            var lookDir = Player.position - tr.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
                tr.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
