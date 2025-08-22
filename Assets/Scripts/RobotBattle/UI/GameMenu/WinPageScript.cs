using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle.UI.GameMenu
{
    public class WinPageScript : GameMenuPageBaseScript
    {
        [SerializeField] EndGameItemScript[] endGameItemScripts;

        public void open()
        {
            /*TimerUtil.pause();
            gameObject.SetActive(true);
            var result = GameManagerScript.Instance.getResults(GameManagerScript.Instance.currentPlayer.team);
            var orderDamage = GameManagerScript.Instance.orderDamage(result);

            for (int i = 0; i < endGameItemScripts.Length; i++)
            {
                endGameItemScripts[i].init(result[i], i, orderDamage.FindIndex(x => x == i));
            }*/
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