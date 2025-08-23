using RobotBattle.Weapon;
using UnityEngine;

namespace RobotBattle.Enemy
{
    public class SentryEnemy : EnemyBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MachineGunControllerScript _machineGun;

        public virtual void Init(EnemySettings settings, PoolModel poolModel)
        {
            base.Init(settings);
            _machineGun.Init(poolModel, null, settings.Shotgun, null);
        }
        
        protected override void SetHoldState()
        {
            base.SetHoldState();
            _machineGun.SetShooting(false);
            SetMoving(false);
        }
        
        protected override void SetChaseState()
        {
            base.SetChaseState();
            _machineGun.SetShooting(false);
            SetMoving(true);
        }
        
        protected override void SetAttackState()
        {
            base.SetAttackState();
            _machineGun.SetShooting(true);
            SetMoving(false);
        }

        private void SetMoving(bool state)
        {
             const string animatorState = "Walk";
            _animator.SetBool(animatorState, state);
        } 
    }
}
