using RobotBattle.Inputs;
using RobotBattle.Weapon;
using UnityEngine;
using RobotBattle.UI.GameMenu;

namespace RobotBattle.Robot
{
    public class PlayerBehaviour : AbstractBehaviour
    {
        private RobotSettings.Camera _cameraParamsScript;
        private Coroutine _isWalkingSound;

        public bool ShieldStatus
        {
            get => PlayerController.GetShield().activeSelf; 
            set => PlayerController.GetShield().SetActive(value); 
        }

        public PlayerBehaviour(PlayerController playerController, InputModel inputModel) : base(playerController)
        {
            inputModel.MoveInputs.OnAction += OnMove;
            inputModel.AttackInputs.OnAction += OnAttack;
            inputModel.ShotInputs.OnAction += OnShot;
            inputModel.FireInputs.OnAction += OnFire;
            inputModel.RocketInputs.OnAction += OnRocket;

            PlayerController.SetParams(WeaponTarget, (duration, weaponType, isFullReload) => {/* UIControllerScript.Instance.reload(duration, weaponType);*/ });

            ShieldStatus = false;;
        }

        private void OnAttack(InputPhase phase, float attack)
        {
            PlayerController.SetAllButtons(attack > 0);
        }

        private void OnShot(InputPhase phase, float attack)
        {
            PlayerController.SetWeapons(WeaponType.Shot, attack > 0);
        }
        
        private void OnFire(InputPhase phase, float attack)
        {
            PlayerController.SetWeapons(WeaponType.Fire, attack > 0);
        }
        
        private void OnRocket(InputPhase phase, float attack)
        {
            PlayerController.SetWeapons(WeaponType.Rocket, attack > 0);
        }
        
        private void OnMove(InputPhase phase, Vector2 direction)
        {
            PlayerController.SetMovementDirection(direction);
        }

        private void Update()
        {
            SetTarget();

            PlayerController.RotateTorso(
                Mathf.Abs(InputUtil.LookChangeVector.x) > 0.05f
                    ? (InputUtil.LookChangeVector.x + InputUtil.MoveChangeVector.x) *
                      _cameraParamsScript.HorizontalRotationSensitivity
                    : 0
            );

            SetCameraDirection();

            UIControllerScript.health = new Vector2(PlayerController.Health, PlayerController.ShieldHealth);
        }

        private void OnDestroy()
        {
            if (PlayerController.Health <= 0)
            {
                RobotAchievement.isDead = true;
                GameManagerScript.Instance.doLoseAction();
                TaskManagerScript.instance.dieCount++;
            }
        }

        private void SetTarget()
        {
            if (GameManagerScript.robotInFireArea != null)
            {
                WeaponTarget.targetType = WeaponTargetScript.TargetType.Transform;
                WeaponTarget.targetTransform = GameManagerScript.robotInFireArea;
            }
            else
            {
                WeaponTarget.targetType = WeaponTargetScript.TargetType.Position;
                //WeaponTarget.targetPosition = cameraTransform.forward * 100 + cameraTransform.position;
            }
        }

        private void SetCameraDirection()
        {
            GameManagerScript.Instance.cameraTarget =
                PlayerController.Torso.position + PlayerController.Torso.forward * 1000f;
        }
    }
}