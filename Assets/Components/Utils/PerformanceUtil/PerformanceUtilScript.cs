using UnityEngine;
using System.Collections;

public class PerformanceUtilScript : MonoBehaviour
{
	private bool showGUI = false;

	public const int defaultFPS = 45;
	public static float avgFPS = 0;
	
	string FPSstring;
	float FPSupdateTime;

	void Start ()
	{
		Invoke ("sendAnalytics", 7f);
		InvokeRepeating ("sendAnalytics", 15f, 8f);
		QualityManager.setQualitySettings ();
	}

	void OnGUI ()
	{
		if (Time.timeScale != 1) {
			return;
		}

		float fps = 1f / Time.deltaTime;

		if (showGUI) {
			if (Time.time > FPSupdateTime) {
				FPSstring = "FPS: " + fps.ToString ("#.00");
				FPSupdateTime = Time.time + 0.5f; //update every 0.5 seconds
			}
			GUI.Box (new Rect (Screen.width * 0.5f - 40f, Screen.height - 20f, 180f, 20f), FPSstring);
		}
		if (avgFPS == 0) {
			avgFPS = fps;
		} else {
			avgFPS = (avgFPS + fps) / 2f;
		}
	}

	private void sendAnalytics ()
	{
		if (avgFPS == 0) {
			return;
		}

		if (avgFPS < 25) {
			NativeController.trackEvent ("FPS 0-25");
			QualityManager.setQualitySettings (QualityManager.QualityMode.LOW);
		} else if (avgFPS < 30) {
			NativeController.trackEvent ("FPS 25-30");
			QualityManager.setQualitySettings (QualityManager.QualityMode.NORMAL);
		} else if (avgFPS < 35) {
			NativeController.trackEvent ("FPS 30-35");
			QualityManager.setQualitySettings (QualityManager.QualityMode.NORMAL);
		} else if (avgFPS < 40) {
			NativeController.trackEvent ("FPS 35-40");
			QualityManager.setQualitySettings (QualityManager.QualityMode.HIGH);
		} else if (avgFPS < 44) {
			NativeController.trackEvent ("FPS 40-44");
			QualityManager.setQualitySettings (QualityManager.QualityMode.HIGH);
		} else {
			NativeController.trackEvent ("FPS 43+");
			QualityManager.setQualitySettings (QualityManager.QualityMode.SUPER);
		}

		avgFPS = 0;
	}


}
