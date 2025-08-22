using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle.UI.GameMenu
{
    public class LosePageScript : GameMenuPageBaseScript
    {
        [SerializeField] EndGameItemScript[] endGameItemScripts;
        [SerializeField] List<int> orderDamage;

        public void open()
        {
            /*TimerUtil.pause();

            gameObject.SetActive(true);
            var result = GameManagerScript.Instance.getResults(GameManagerScript.Instance.currentPlayer.team);
            orderDamage = GameManagerScript.Instance.orderDamage(result);

            for (int i = 0; i < endGameItemScripts.Length; i++)
            {
                endGameItemScripts[i].init(result[i], i, orderDamage.FindIndex(x => x == i));
            }*/
        }

        private int findIndex(List<int> list, int value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                return i;
            }

            return -1;
        }

        public void close()
        {
            gameObject.SetActive(false);
        }

        public void onBackToGarage()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            GameMenuScript.Instance.goToGarage();
        }
    }
}