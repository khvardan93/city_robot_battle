using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle.UI.MainMenu
{
    public class DonateMenuScript : MainMenuPageBaseScript
    {
        [SerializeField] GameObject silverContainer;
        [SerializeField] DonateItemScript[] silverItems;
        [Space] [SerializeField] GameObject goldContainer;
        [SerializeField] DonateItemScript[] goldItems;

        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            Hide();
        }

        public void onGold()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            silverContainer.SetActive(false);
            goldContainer.SetActive(true);
        }

        public void onSilver()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            silverContainer.SetActive(true);
            goldContainer.SetActive(false);
        }
    }
}