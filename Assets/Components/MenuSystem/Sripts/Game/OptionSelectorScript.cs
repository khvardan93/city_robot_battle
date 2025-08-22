using UnityEngine;
using UnityEngine.UI;

public class OptionSelectorScript : MonoBehaviour {
	/*[SerializeField]
	private GameObject mphSelector;
	[SerializeField]
	private GameObject kmhSelector;
	[SerializeField]
	private Slider seneitivitySlider;

	void Start() {
		this.mphSelector.SetActive(GameSettings.getSpeedUnits() == GameSettings.speedUnits.MPH);
		this.kmhSelector.SetActive(GameSettings.getSpeedUnits() == GameSettings.speedUnits.KMH);
		this.seneitivitySlider.value = (PlayerModel.instance.sensitivity - GameSettings.SENSITIVITY_MIN_VALUE) / 
			(GameSettings.SENSITIVITY_MAX_VALUE - GameSettings.SENSITIVITY_MIN_VALUE);
	}

	public void mphEvent(){
		GameSettings.setSpeedUnits (GameSettings.speedUnits.MPH);
		this.mphSelector.SetActive (true);
		this.kmhSelector.SetActive (false);
		PlayerPrefs.SetString ("speed_unit", "MPH");
		SoundEffects.instance.playSound (SoundEffects.instance.buttonClick2);
	}

	public void kmhEvent(){
		GameSettings.setSpeedUnits (GameSettings.speedUnits.KMH);
		this.mphSelector.SetActive (false);
		this.kmhSelector.SetActive (true);
		PlayerPrefs.SetString ("speed_unit", "KMH");
		SoundEffects.instance.playSound (SoundEffects.instance.buttonClick2);
	}

	public void onSensitivityValueChange(){
		PlayerModel.instance.sensitivity = GameSettings.SENSITIVITY_MIN_VALUE + this.seneitivitySlider.value * (GameSettings.SENSITIVITY_MAX_VALUE - GameSettings.SENSITIVITY_MIN_VALUE);
	}*/
}
