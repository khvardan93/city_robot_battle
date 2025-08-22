using RobotBattle.UI.Generic;
using UnityEngine;
using RobotBattle.Utils;

namespace RobotBattle.UI.MainMenu
{
    public class MainMenuScript : PageEngineScript<MainMenuScript, MainMenuPageBaseScript>
    {
        private void Start()
        {
            Change<HomeMenuScript>();

            if (!GameManagerScript.Instance.isNoMusic)
            {
                SetMusic(true);
            }
        }

        public void GoToBattle()
        {
            Show<LoadingScript>();
            SceneLoader.LoadScene(Scenes.Island);
            SetMusic(false);
        }

        public void OpenDonatePage(Currencies currency)
        {
            DonateMenuScript donateMenuScript = (DonateMenuScript)GetPage<DonateMenuScript>();

            if (donateMenuScript)
            {
                donateMenuScript.Show();
                if (currency == Currencies.Gold)
                {
                    donateMenuScript.onGold();
                }
                else if (currency == Currencies.Silver)
                {
                    donateMenuScript.onSilver();
                }
            }

            Show<DonateMenuScript>();
        }

        public void OnNoMoney(Currencies currency)
        {
            ShowHint($"YOU HAVE NOT\nENOUGH {currency.ToString().ToUpper()}!!!");
        }

        private void ShowHint(string hint)
        {
            (GetPage<HintPopupScript>() as HintPopupScript)?.Open(hint);
        }

        public void SetMusic(bool state)
        {
            if(state)
            {
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}