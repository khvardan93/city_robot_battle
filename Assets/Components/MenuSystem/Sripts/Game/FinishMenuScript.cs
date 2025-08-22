using UnityEngine;
using UnityEngine.UI;

public class FinishMenuScript : MonoBehaviour {

    public Text killsText;
    public Text moneyText;

    void OnEnable () {

        TimerUtil.pause();
        //int earnedMoney = GameManager.gameTypeHandler.currentKillsCount * GameSettings.onlineArenaRewardPerKill;

        //this.killsText.text = "x " + GameManager.gameTypeHandler.currentKillsCount;
        //this.moneyText.text = "x $" + earnedMoney;

        //PlayerModel.instance.earnMoney(earnedMoney);

        RateUsUtil.showDialog ();

        NativeController.showInterstitial(3);
    }
}
