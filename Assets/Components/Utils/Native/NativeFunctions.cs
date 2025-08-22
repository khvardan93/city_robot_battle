using UnityEngine;
using System.Collections;

public class NativeFunctions
{

	public static Texture2D getScreenshot ()
	{
		Texture2D screenTexture = new Texture2D (Screen.width, Screen.height);
		screenTexture.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		screenTexture.Apply ();

		return screenTexture;
	}

	public static void shareImageToFacebook (string caption, string message, Texture2D texture)
	{

	}

	//more games
	//all
	public void openAllAppsPage ()
	{
		NativeController.trackEvent ("link_clicked_all_apps");
		#if UNITY_ANDROID && !UNITY_EDITOR
		Application.OpenURL ("https://play.google.com/store/apps/dev?id=7273046100443723364");
		#elif UNITY_IOS && !UNITY_EDITOR
		//VAPP_IOS_Helper.OpenUrl("itms-apps://itunes.apple.com/us/artist/vahagn-sargsyan/id913182073");
		#else

		#endif
	}

	private static void openGamePage (string androidID)
	{
		NativeController.trackEvent ("link_clicked_" + androidID);

		#if UNITY_ANDROID && !UNITY_EDITOR
		Application.OpenURL ("market://details?id=com.vapp." + androidID);
		#elif UNITY_IOS && !UNITY_EDITOR
		//VAPP_IOS_Helper.OpenUrl("itms://itunes.apple.com/us/app/gun-camera-3d-free/id930616575?mt=8");
		#else
		#endif
	}

	public static void openSniperCamera3DApp ()
	{
		openGamePage ("snipercamera3d");
	}

	public static void openGunCamera3DApp ()
	{
		openGamePage ("guncamera3d");
	}

	public static void openOffroadApp ()
	{
		openGamePage ("offroad");
	}

	public static void openMotoApp ()
	{
		openGamePage ("moto");
	}

	public static void openMissionSniperApp ()
	{
		openGamePage ("missionsniper");
	}

	public static void openHuntApp ()
	{
		openGamePage ("hunt");
	}

	public static void openCitySniperApp ()
	{
		openGamePage ("snipershooting3d");
	}
}
