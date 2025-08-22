using RobotBattle.Inputs;
using RobotBattle.Robot;
using RobotBattle.Robot.Behaviours;
using UnityEngine;

namespace RobotBattle
{
    public class PlayerModel : BaseModel
    {
        public PlayerModel(System system) : base(system)
        {
            var player = GameObject.FindObjectsOfType<PlayerController>();
            if(player == null || player.Length == 0) return;
            
            var behaviour = new PlayerBehaviour(player[0], system.GetModel<InputModel>());
        }
    }
}