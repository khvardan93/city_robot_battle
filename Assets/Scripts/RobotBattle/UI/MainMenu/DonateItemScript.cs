using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.MainMenu
{
    public class DonateItemScript : MonoBehaviour
    {
        [SerializeField] Currencies currencyType;
        [SerializeField] Text countText;
        [SerializeField] Text priceText;

        private GameData.DonateItem data;

        private void OnEnable()
        {
            data = currencyType == Currencies.Gold
                ? GameManagerScript.Instance.GameData.goldItems[transform.GetSiblingIndex()]
                : GameManagerScript.Instance.GameData.silverItems[transform.GetSiblingIndex()];

            countText.text = data.count.ToString();
            priceText.text = currencyType == Currencies.Gold ? "$" + data.price : data.price.ToString();
        }

        public void onClick()
        {
            if (currencyType == Currencies.Silver)
            {
                if (PlayerModelScript.instance.spendGold((int)data.price))
                {
                    RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
                    PlayerModelScript.instance.silver += data.count;
                }
                else
                {
                    RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.getFailSound());
                    MainMenuScript.Instance.OnNoMoney(currencyType);
                }
            }
            else
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            }
        }
    }
}