using UnityEngine;
using UnityEngine.UI;
using RobotBattle.Game;

namespace RobotBattle.UI.MainMenu
{
    public class RobotSelectorItemScript : AbstractSlotItemScript
    {
        [SerializeField] Image image;
        [SerializeField] Image lockImage;

        private RobotType robotType;

        public override void init(int index)
        {
            robotType = (RobotType)index;

            image.sprite = Resources.Load<Sprite>("RobotsSmallImages/" + robotType);
            lockImage.enabled = !Robots.instance.getRobotByType(robotType).isBought;
            if (image.sprite == null) Debug.Log(robotType);
        }

        public void updateItem()
        {
            lockImage.enabled = !Robots.instance.getRobotByType(robotType).isBought;
        }
    }
}