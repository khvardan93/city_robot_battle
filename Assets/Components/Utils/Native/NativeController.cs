using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;

public class NativeController : MonoBehaviour
{
	private static bool firstObject = true;
	private static AdsUtil adsUtil;

	void Awake(){
		if (firstObject) {
			DontDestroyOnLoad(transform.gameObject);
		} else {
			Destroy (transform.gameObject);
		}
	}

	void Start ()
	{
		if (firstObject) {

			if (adsUtil == null) {
				adsUtil = new AdsUtil();
			}

			firstObject = false;
		}
		adsUtil.start ();
	}

	public static void showInterstitial(int fromDay = 0){
        if (adsUtil != null)
		    adsUtil.showInterstitial(fromDay);
	}

	public static void trackEvent (string name)
	{
        Analytics.CustomEvent(name);
        Debug.Log("analytics: " + name);
    }

	public static bool purchase(float price)
	{
		return true;
	}
}