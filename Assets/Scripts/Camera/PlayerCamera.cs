using RobotBattle.Robot;
using Unity.Cinemachine;
using UnityEngine;

namespace RobotBattle
{
    public class PlayerCamera : SingleObjectBase<PlayerCamera>
    {
       [SerializeField] private CinemachineCamera _cinemachineCamera;

       protected override void Start()
       {
           base.Start();
           
           System.Instance.ObjectRegister.Get<PlayerController>((obj) =>
           {
               _cinemachineCamera.Target.TrackingTarget = ((PlayerController)obj).transform;
           });
       }
    }
}
