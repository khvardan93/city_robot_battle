using UnityEngine;
using UnityEngine.UI;
using RobotBattle.Utils;

public class GameMenuController : MonoBehaviour
{
	[System.Serializable]
	public struct Pages
	{
		public GameObject gameUI;
		public GameObject optionsPage;
		public GameObject winPage;
		public GameObject finishPage;
		public GameObject deathPage;
		public GameObject loadingPage;
	}

	public Pages pages;

	public GameObject fireButton;
	public GameObject lockedButton;
	public GameObject refreshButton;
	public UITextColorChangeScript hintMessageText;
	public Image aimImage;

	public PlayerUIMessageScript playerUIMessage;

	[Header("Zoom")] public GameObject zoomIcon;
	public Sprite toZoomSprite;
	public Sprite fromZoomSprite;

	#region singleton

	private static GameMenuController _instance;

	public static GameMenuController instance
	{
		get { return _instance; }
	}

	#endregion

	#region navive functions

	void Awake()
	{
		_instance = this;
	}

	#endregion

	#region click events

	public void openMenuEvent()
	{
		//only exit from finish menu
		/*if (GameManager.isOnline() && !this.pages.finishPage.activeSelf)
		{
		    this.pages.optionsPage.SetActive(false);
		    openFinishMenu();
		    return;
		}*/

		//normal gameplay
		this.startLoading();

		//OnlineBaseController.forceReset();
		SoundEffects.instance.playSound(SoundEffects.instance.buttonClick);
		//NativeController.showInterstitial ();
		//SceneLoader.LoadScene(Scenes.menu);
	}

	public void continueEvent()
	{
		TimerUtil.release();
		MusicScript.pause();
		this.pages.optionsPage.SetActive(false);
		SoundEffects.instance.playSound(SoundEffects.instance.buttonClick2);
	}

	public void restartEvent()
	{
		this.pages.optionsPage.SetActive(false);

		// OnlineBaseController.forceReset();

		//TanksManager.instance.reset ();
		SoundEffects.instance.playSound(SoundEffects.instance.buttonClick2);
		NativeController.showInterstitial(1);
		SceneLoader.ReloadScene();
	}

	#endregion

	#region UI click events

	public void pauseEvent()
	{
		/*if (!GameManager.isOnline())
		{
		    TimerUtil.pause();
		    NativeController.showInterstitial(3);
		}*/
		MusicScript.play();
		this.pages.optionsPage.SetActive(true);
		SoundEffects.instance.playSound(SoundEffects.instance.pauseSound);
	}

	public void fireEvent()
	{
		//GameManager.instance.playerScript.doShootCommand();

		this.fireButton.SetActive(false);
		this.lockedButton.SetActive(true);

		//Invoke ("activateFireButton", GameManager.instance.playerScript.nextShootTime);
	}

	public void refreshEvent()
	{
		// GameManager.instance.playerScript.refreshTankCommand();

		this.refreshButton.SetActive(false);

		Invoke("activateRefreshButton", 3f);
	}

	public void lockedEvent()
	{
		SoundEffects.instance.playSound(SoundEffects.instance.buttonClickFail, 0.1f);
	}

	public void zoomEvent()
	{
		this.zoomIcon.SetActive(!this.zoomIcon.activeSelf);
		this.aimImage.enabled = !this.aimImage.enabled;
		//TankCameraScript.instance.setZoom();
	}

	public void changeTextureEvent(Image image)
	{
		image.sprite = image.sprite == this.toZoomSprite ? this.fromZoomSprite : this.toZoomSprite;
	}

	#endregion

	#region callback functions

	public void onZoomValueChange(Slider slider)
	{
		//GameManager.zoom = GameSettings.ZOOM_MAX_VALUE - slider.value * (GameSettings.ZOOM_MAX_VALUE - GameSettings.ZOOM_MIN_VALUE);
	}

	#endregion

	#region game pages cklick events

	#endregion

	public void showHint(string hint)
	{
		this.playerUIMessage.show(hint, new Color(0.65f, 0.5f, 0.05f));
	}

	public void showHint(string hint, float time)
	{
		this.playerUIMessage.show(hint, time);
	}

	public void invokeMethod(string method, float invokeTime)
	{
		Invoke(method, invokeTime);
	}

	public void addAim(Sprite aim)
	{
		this.aimImage.enabled = true;
		this.aimImage.sprite = aim;
	}

	public void showDamageHint(string hintMessage)
	{
		this.hintMessageText.setHint(hintMessage, Color.grey);
	}

	public void showDamageHint(string hintMessage, Color color)
	{
		this.hintMessageText.setHint(hintMessage, color);
	}

	#region private functions

	private void activateFireButton()
	{
		this.fireButton.SetActive(true);
		this.lockedButton.SetActive(false);
	}

	private void activateRefreshButton()
	{
		this.refreshButton.SetActive(true);
	}

	private void startLoading()
	{
		this.pages.winPage.SetActive(false);
		this.pages.optionsPage.SetActive(false);
		this.pages.deathPage.SetActive(false);
		this.pages.loadingPage.SetActive(true);
	}

	private void openWinMenu()
	{
		this.pages.winPage.SetActive(true);
	}

	private void openDeathMenu()
	{
		this.pages.deathPage.SetActive(true);
	}

	private void openFinishMenu()
	{
		this.pages.finishPage.SetActive(true);
	}

	#endregion
}
