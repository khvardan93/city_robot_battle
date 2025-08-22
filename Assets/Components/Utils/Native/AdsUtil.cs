using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public class AdsUtil {

    int currentDay = 0;

	public AdsUtil() {
        //DateTime installDate = PlayerModel.instance.getInstallDate();

        //this.currentDay = (DateTime.Now - installDate).Days;
    }

	public void start(){
		
	}

	public void showInterstitial(int fromDay)
    {
        if(fromDay >= this.currentDay)
        {
            return;
        }

        /*if (Advertisement.IsReady ()) {
			Advertisement.Show ();
			NativeController.trackEvent ("unity_ad_shown");
		} else {
			NativeController.trackEvent("unity_ad_failed");
		}*/
	}
		
	private void OnInterstisialsOpen() {
		//pausing the game
		TimerUtil.pause();
	}

	private void OnInterstisialsClosed() {
		//un-pausing the game
		TimerUtil.release();
	}
}
