using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.MainMenu
{
    public class HintPopupScript : MainMenuPageBaseScript
    {
        [SerializeField] private Text hint;

        public void Open(string hintText)
        {
            Show();

            hint.text = hintText;
        }

        public void OnClose()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            gameObject.SetActive(false);
        }
    }
}