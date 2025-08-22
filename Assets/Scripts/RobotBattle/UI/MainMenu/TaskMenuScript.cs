using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.MainMenu
{
    public class TaskMenuScript : MainMenuPageBaseScript
    {
        [SerializeField] RectTransform taskContent;
        [SerializeField] TaskItemScript taskExample;
        [Space] [SerializeField] Text goldCountText;
        [SerializeField] Text silverCountText;

        private void OnEnable()
        {
            open();
        }

        public void open()
        {
            gameObject.SetActive(true);

            foreach (Transform item in taskContent)
            {
                Destroy(item.gameObject);
            }

            foreach (var task in TaskManagerScript.instance.getActiveTasks())
            {
                Instantiate(taskExample, taskContent).init(task);
            }
        }

        public void close()
        {
            gameObject.SetActive(false);
        }

        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<HomeMenuScript>();
        }

        public void onBonus()
        {

        }

        public void onDaily()
        {

        }
    }
}