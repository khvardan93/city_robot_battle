using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using RobotBattle.Game;
using RobotBattle.Robot;

namespace RobotBattle.UI.MainMenu
{
    public class ShopMenuScript : MainMenuPageBaseScript
    {
        [SerializeField] MenuWeaponItemScript menuWeaponItemExample;
        [SerializeField] RectTransform weaponContainer;
        [Space] 
        [SerializeField] Text lifeText;
        [SerializeField] Text speedText;
        [SerializeField] Text weightText;
        [SerializeField] Text defenceText;
        [Space] 
        [SerializeField] Image robotImage;
        [SerializeField] Text robotName;
        [SerializeField] ShopWeaponItemScript[] shopWeaponItems;
        [Space] 
        [SerializeField] GameObject jumpSelected;
        [SerializeField] GameObject jumpUnselected;
        [SerializeField] GameObject flySelected;
        [SerializeField] GameObject flyUnselected;

        [Space] [SerializeField] GameObject toSkinButton;

        [Header("Buy Robot")] [SerializeField] GameObject buyButton;
        [SerializeField] Text priceText;
        [SerializeField] Image currencyImage;
        [SerializeField] Sprite goldSprite;
        [SerializeField] Sprite silverSprite;

        [Header("Robot selector")] [SerializeField]
        LoopingSpinnerExample loopingSpinnerExample;

        [SerializeField] RectTransform content;

        private RobotData robot;

        private void OnEnable()
        {
            StartCoroutine(init());
        }

        private IEnumerator init()
        {
            yield return new WaitForEndOfFrame();

            loopingSpinnerExample.ChangeItemsCountWithChecks(Robots.instance.robots.Count);
            loopingSpinnerExample.onChangeAction = (index) => setParams((RobotType)index);
            loopingSpinnerExample.GetComponent<Snapper8>()
                ?.setByIndex((int)GameManagerScript.Instance.currentRobotType);
        }

        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<HomeMenuScript>();
        }

        public void onOpenSkinMenu()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<SkinSelectorMenuScript>();
        }

        public void onBuy()
        {
            if (PlayerModelScript.instance.spendCurrency(robot.price, robot.currencyType))
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
                robot.isBought = true;
                setParams(robot.robotType);

                foreach (Transform item in content)
                {
                    item.GetComponent<RobotSelectorItemScript>()?.updateItem();
                }
            }
            else
            {
                RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.getFailSound());
                MainMenuScript.Instance.OnNoMoney(robot.currencyType);
            }
        }

        private void setParams(RobotType robotType)
        {
            robot = Robots.instance.getRobotByType(robotType);

            toSkinButton.SetActive(robot.isBought);

            if (robot.isBought)
            {
                GameManagerScript.Instance.currentRobotType = robotType;
            }

            robotImage.sprite = Resources.Load<Sprite>("RobotsSmallImages/" + robotType);

            string[] split = global::System.Text.RegularExpressions.Regex.Split(robotType.ToString(), @"(?<!^)(?=[A-Z])");
            string nameString = "";
            foreach (var word in split)
            {
                nameString += word + " ";
            }

            nameString = nameString.Remove(nameString.Length - 1, 1);

            robotName.text = nameString.ToUpper();
            lifeText.text = robot.health.ToString();
            speedText.text = robot.speed.ToString();
            defenceText.text = robot.shieldHealth.ToString();
            weightText.text = robot.weight.ToString();

            jumpSelected.SetActive(robot.jump);
            jumpUnselected.SetActive(!robot.jump);
            flySelected.SetActive(robot.fly);
            flyUnselected.SetActive(!robot.fly);

            for (int i = 0; i < shopWeaponItems.Length; i++)
            {
                if (i < robot.weapons.Length)
                {
                    shopWeaponItems[i].show(robotType, robot.weapons[i].WeaponType);
                }
                else
                {
                    shopWeaponItems[i].hide();
                }
            }

            buyButton.SetActive(!robot.isBought);
            priceText.text = robot.price.ToString();
            currencyImage.sprite = robot.currencyType == Currencies.Silver ? silverSprite : goldSprite;
        }
    }
}