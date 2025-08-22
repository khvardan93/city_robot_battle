using RobotBattle.Game;
using UnityEngine;
using RobotBattle.Robot;

namespace RobotBattle.Garage
{
    public class GarageRobotScript : MonoBehaviour
    {
        public RobotType robotType;

        private MaterialChangerScript materialChangerScript;

        private void SetMaterial()
        {
            if (materialChangerScript == null) materialChangerScript = GetComponent<MaterialChangerScript>();
            materialChangerScript.SetMaterial(Robots.instance.getRobotByType(robotType).materialType);
        }

        public void SetGameobjectStatus(bool state)
        {
            gameObject.SetActive(state);
            if (state)
            {
                SetMaterial();
            }
        }
    }
}