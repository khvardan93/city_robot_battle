using UnityEngine;
using UnityEngine.UI;
using RobotBattle.Garage;

namespace RobotBattle.UI.MainMenu
{
    public class HomeMenuScript : MainMenuPageBaseScript
    {
        [SerializeField] private Text _playerLevelText;
        [SerializeField] private Image _hasFinishedTaskImage;

        public override void Show()
        {
            base.Show();
            TaskManagerScript.instance.resetActiveTasks();

            GarageControllerScript.Instance.SetCurrentRobot();
            GarageControllerScript.Instance.SwitchToHomeCamera();

            _playerLevelText.text = PlayerModelScript.instance.level.ToString();

            _hasFinishedTaskImage.enabled = HasFinishedTask();
        }

        private bool HasFinishedTask()
        {
            bool returnValue = false;

            var activeTasks = TaskManagerScript.instance.getActiveTasks();
            foreach (var item in activeTasks)
            {
                var status = item.getStatus();
                if (status.isFinished && !status.isClaimed)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public void ToBattle()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            MainMenuScript.Instance.GoToBattle();
        }

        public void ToMagazine()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<ShopMenuScript>();
        }

        public void ToCase()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<ChestMenuScript>();
        }

        public void ToTasks()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<TaskMenuScript>();
        }

        public void ToSettings()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<SettingMenuScript>();
        }

        public void OnComingSoon()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.getFailSound());
        }
    }
}