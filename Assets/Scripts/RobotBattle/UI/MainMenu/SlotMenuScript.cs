using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.MainMenu
{
    public class SlotMenuScript : MainMenuPageBaseScript
    {
        [Space] [SerializeField] Text slotName;
        [SerializeField] LoopingSpinnerExample loopingSpinnerExample;
        [SerializeField] GameObject backButton;

        private Snapper8 snapper;

        private void OnEnable()
        {
            snapper = loopingSpinnerExample.GetComponent<Snapper8>();
            StartCoroutine(init());
        }

        private IEnumerator init()
        {
            backButton.SetActive(false);

            yield return new WaitForEndOfFrame();

            loopingSpinnerExample.ChangeItemsCountWithChecks(15);

            float spinDuration = Random.Range(3, 5);
            float timer = spinDuration;

            while (timer > 0)
            {
                loopingSpinnerExample.Velocity = new Vector2(4000f * timer / spinDuration, 0);

                yield return new WaitForEndOfFrame();

                timer -= Time.deltaTime;
            }

            yield return new WaitUntil(() => !snapper.SnappingInProgress);

            backButton.SetActive(true);

            var lastItem = GameData.currentCase[loopingSpinnerExample.getLastIndex()];
            PlayerModelScript.instance.addCurrency(lastItem.count, lastItem.currency);
        }

        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<HomeMenuScript>();
        }
    }
}