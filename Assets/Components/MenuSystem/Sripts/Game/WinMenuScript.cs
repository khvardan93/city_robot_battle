using UnityEngine;
using UnityEngine.UI;

public class WinMenuScript : MonoBehaviour {

    public Text starsText;
    public Text moneyText;

    void OnEnable()
    {
        TimerUtil.pause();

        //this.starsText.text = "x " + GameManager.instance.getCurrentLevel().starAward;
        //this.moneyText.text = "x $" + GameManager.instance.getCurrentLevel().moneyAward;

        RateUsUtil.showDialog();
        NativeController.showInterstitial(2);
    }
}
