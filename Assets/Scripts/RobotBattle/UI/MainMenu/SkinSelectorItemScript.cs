using System.Collections;
using System.Collections.Generic;
using RobotBattle.Game;
using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.MainMenu
{
    public class SkinSelectorItemScript : MonoBehaviour
    {
        [SerializeField] Image skinImage;
        [SerializeField] Text skinName;
        [Space] [SerializeField] GameObject lockedContainer;
        [SerializeField] GameObject buyContainer;
        [SerializeField] GameObject ownedContainer;
        [Space] [SerializeField] Image selectedImage;
        [Space] [SerializeField] MaterialType materialType;

        public void init()
        {
            init(materialType);
        }

        public void init(MaterialType materialType)
        {
            this.materialType = materialType;
            skinImage.sprite = Resources.Load<Sprite>("SkinTtextures/texture_" + materialType);

            bool isBought = materialType == MaterialType.Blue ||
                            Robots.instance.getRobotByType(GameManagerScript.Instance.currentRobotType)
                                .isMaterialBought(materialType);

            lockedContainer.SetActive(false);
            buyContainer.SetActive(!isBought);
            ownedContainer.SetActive(isBought);

            selectedImage.enabled =
                Robots.instance.getRobotByType(GameManagerScript.Instance.currentRobotType).materialType ==
                materialType;

            skinName.text = materialType.ToString();
        }

        public void onBuy()
        {
            if (PlayerModelScript.instance.spendGold(50))
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
                Robots.instance.getRobotByType(GameManagerScript.Instance.currentRobotType)
                    .setMaterialBought(materialType);
                init();
            }
            else
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.getFailSound());
                MainMenuScript.Instance.OnNoMoney(Currencies.Gold);
            }
        }

        public void onSelect()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            Robots.instance.getRobotByType(GameManagerScript.Instance.currentRobotType).materialType = materialType;

            GetComponentInParent<SkinSelectorMenuScript>().refrshItems();
        }
    }
}