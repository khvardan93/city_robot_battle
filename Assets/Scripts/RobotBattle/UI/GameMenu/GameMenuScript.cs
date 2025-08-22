using System.Collections;
using UnityEngine;
using RobotBattle.Utils;
using RobotBattle.UI.Generic;

namespace RobotBattle.UI.GameMenu
{
    public class GameMenuScript : PageEngineScript<GameMenuScript, GameMenuPageBaseScript>
    {
        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(checkGame());
        }

        public void openLoading()
        {
            Change<LoadingScript>();
        }

        public void openPausePage()
        {
            Change<PausePageScript>();
        }

        public void openWinPage()
        {            
            Change<WinPageScript>();
        }

        public void openLosePage()
        {
            Change<LosePageScript>();
        }

        public void goToGarage()
        {
            openLoading();
            SceneLoader.LoadScene(Scenes.Garage);
            TimerUtil.release();
        }

        private IEnumerator checkGame()
        {
            while (gameObject)
            {
                GameManagerScript.Instance.checkGameStatus();
                yield return new WaitForSeconds(3);
            }
        }
    }
}