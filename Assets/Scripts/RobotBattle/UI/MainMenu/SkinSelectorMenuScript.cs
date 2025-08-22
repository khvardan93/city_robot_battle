using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotBattle.Garage;

namespace RobotBattle.UI.MainMenu
{
    public class SkinSelectorMenuScript : MainMenuPageBaseScript
    {
        [SerializeField] SkinSelectorItemScript[] skinSelectorItems;

        public override void Show()
        {
            base.Show();

            GarageControllerScript.Instance.SetCurrentRobot();
            GarageControllerScript.Instance.SwitchToSkinCamera();
            refrshItems();
        }

        public void refrshItems()
        {
            for (int i = 0; i < skinSelectorItems.Length; i++)
            {
                skinSelectorItems[i].init((MaterialType)i);
            }

            GarageControllerScript.Instance.SetCurrentRobot();
        }

        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<HomeMenuScript>();
        }

        public void onBattle()
        {

        }
    }
}