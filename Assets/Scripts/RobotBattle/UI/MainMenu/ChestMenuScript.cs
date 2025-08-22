using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle.UI.MainMenu
{
    public class ChestMenuScript : MainMenuPageBaseScript
    {
        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<HomeMenuScript>();
        }

        public void onOpenBlueCase()
        {
            if (PlayerModelScript.instance.spendDiamonds(10))
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
                GameData.currentCase = GameManagerScript.Instance.GameData.firstCase;

                ChangePage<SlotMenuScript>();
            }
            else
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.getFailSound());
                MainMenuScript.Instance.OnNoMoney(Currencies.Diamonds);
            }
        }

        public void onOpenYellowCase()
        {
            if (PlayerModelScript.instance.spendDiamonds(50))
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
                GameData.currentCase = GameManagerScript.Instance.GameData.secondCase;

                ChangePage<SlotMenuScript>();
            }
            else
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.getFailSound());
                MainMenuScript.Instance.OnNoMoney(Currencies.Diamonds);
            }
        }
    }
}