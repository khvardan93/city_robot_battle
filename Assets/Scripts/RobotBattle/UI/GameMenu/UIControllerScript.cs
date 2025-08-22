using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RobotBattle.Game;

namespace RobotBattle.UI.GameMenu
{
    public class UIControllerScript : RobotBattle.Generic.BaseMonoBehaviourSingleton<UIControllerScript>
    {
        [global::System.Serializable]
        public class FireButton
        {
            public WeaponType weaponType;
            public GameObject button;
            public Slider slider;
            public float interval;
            public float reloadInterval;

            public bool isAnimating;
        }

        [SerializeField] FireButton[] fireButtons;

        [Header("Shield Button")] 
        [SerializeField] Image shieldButtonImage;

        [SerializeField] Sprite enabledShieldImage;
        [SerializeField] Sprite disabledShieldImage;
        [Space] [SerializeField] ProgressBarScript shieldHealthProgress;
        [SerializeField] ProgressBarScript healthProgress;
        [Space] [SerializeField] GameObject[] friendsStatusItems;
        [SerializeField] GameObject[] enemiesStatusItems;

       

        /// <summary>
        /// 'x' is the health,
        /// 'y' is the shieald's health
        /// </summary>
        public static Vector2 health = Vector2.zero;

        private void Start()
        {
            setWeaponButtons();
            StartCoroutine(setProgresses());
            shieldButtonImage.sprite = disabledShieldImage;
        }

        private IEnumerator setProgresses()
        {
            while (gameObject)
            {
                var robotParams = Robots.instance.getRobotByType(GameManagerScript.Instance.currentRobotType);
                healthProgress.setProgressStatus(health.x, robotParams.health);
                shieldHealthProgress.setProgressStatus(health.y, robotParams.shieldHealth);

                var counts = ScreenManager.Instance.GetLeftRobotsCount();
                setStatus(friendsStatusItems, counts.friends);
                setStatus(enemiesStatusItems, counts.enemies);

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void setStatus(GameObject[] statusObjects, int count)
        {
            for (int i = 0; i < statusObjects.Length; i++)
            {
                statusObjects[i].SetActive(i < count);
            }
        }

        public void setWeaponButtons()
        {
            var weapons = Robots.instance.getRobotByType(GameManagerScript.Instance.currentRobotType).weapons;

            foreach (var item in fireButtons)
            {
                item.button.SetActive(false);
                foreach (var weapon in weapons)
                {
                    if (weapon.WeaponType == item.weaponType)
                    {
                        item.reloadInterval = weapon.ReloadTime;
                        item.interval = (float)weapon.FireDuration / weapon.Capacity;
                        item.slider.value = 0;
                        item.button.SetActive(true);
                        break;
                    }
                }
            }
        }

        public void reload(float duration, WeaponType weaponType)
        {
            var item = getFireButton(weaponType);
            if (!item.isAnimating)
            {
                item.isAnimating = true;
                item.slider.value = 1;
                item.slider.DOValue(0, duration).OnComplete(() => { item.isAnimating = false; });
            }
        }

        private FireButton getFireButton(WeaponType weaponType)
        {
            foreach (var item in fireButtons)
            {
                if (item.weaponType == weaponType)
                {
                    return item;
                }
            }

            return null;
        }

        #region CLICK EVENTS

        public void onMiniGun(bool state)
        {
            InputUtil.WeaponInputs[WeaponType.Shot] = state;
        }

        public void onFire(bool state)
        {
            InputUtil.WeaponInputs[WeaponType.Fire] = state;
        }

        public void onLaser(bool state)
        {    
            InputUtil.WeaponInputs[WeaponType.Laser] = state;
        }

        public void onMachineGun(bool state)
        {
            InputUtil.WeaponInputs[WeaponType.Shot] = state;
        }

        public void onRocket(bool state)
        {
            InputUtil.WeaponInputs[WeaponType.Rocket] = state;
        }

        public void onBigRocket(bool state)
        {
            InputUtil.WeaponInputs[WeaponType.Rocket] = state;
        }

        public void onShootAll(bool state)
        {
            foreach (var wType in (WeaponType[])Enum.GetValues(typeof(WeaponType)))
            {
                InputUtil.WeaponInputs[wType] = state;
            }
        }

        public void onPause()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            TimerUtil.pause();
            GameMenuScript.Instance.openPausePage();
        }

        public void onShield()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            if (GameManagerScript.Instance.currentPlayer.ShieldStatus)
            {
                GameManagerScript.Instance.currentPlayer.ShieldStatus = false;
                shieldButtonImage.sprite = disabledShieldImage;
            }
            else
            {
                GameManagerScript.Instance.currentPlayer.ShieldStatus = true;
                shieldButtonImage.sprite = enabledShieldImage;
            }
        }

        #endregion
    }
}