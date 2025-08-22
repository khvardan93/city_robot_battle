using UnityEngine;

namespace RobotBattle.Robot
{
    public partial class PlayerController
    {        
        [SerializeField] private NavMeshController _navMesh;

        [SerializeField] private float _robotSpeed = 1f;
        [SerializeField] private float _rotationSpeed = 1000f;

        private Vector2 _moveTo;

        public void SetMovementDirection(Vector2 movement)
        {
            _moveTo = movement;
        }

        private void Update()
        {
            if (_moveTo.y != 0)
            {
                _navMesh.Move(_transform.forward * (_moveTo.y * _robotSpeed * Time.deltaTime));
            }
            else
            {
                _navMesh.SetStopped();
            }

            if (_moveTo.x != 0)
            {
                var angleRad = _moveTo.x * Mathf.Deg2Rad;
                var moveDir = new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));

                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, _transform.rotation * targetRotation,
                    Time.deltaTime * _rotationSpeed);
            }

            UpdateState();
        }

        private void UpdateState()
        {
            if(_moveTo == Vector2.zero) _currentRobotMoveState = RobotMoveState.Idle;
            else if(_moveTo.y != 0) _currentRobotMoveState = _moveTo.y > 0 ? RobotMoveState.Walk : RobotMoveState.Back;
            else _currentRobotMoveState = _moveTo.x > 0 ? RobotMoveState.TurnRight : RobotMoveState.TurnLeft;

            _robot.SetWalkState(_currentRobotMoveState == RobotMoveState.Walk);   
            _robot.SetBackState(_currentRobotMoveState == RobotMoveState.Back);   
            _robot.SetIdleState(_currentRobotMoveState == RobotMoveState.Idle);   
            _robot.SetLeftState(_currentRobotMoveState == RobotMoveState.TurnLeft);   
            _robot.SetRightState(_currentRobotMoveState == RobotMoveState.TurnRight);
        }
    }
}