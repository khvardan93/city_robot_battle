using UnityEngine;
using System.Collections.Generic;

public class FreeMenuScript : MonoBehaviour
{
	public TextMesh[] worldNames;

	void Start ()
	{
		for (int i = 0; i < worldNames.Length; i++) {
			//this.worldNames [i].text = GameSettings.scenes [i].Replace("battle-","");
		}
	}

	/*void Update ()
	{		
		if (InputManager.clickedButton.StartsWith ("FreeMenu_StartButton")) {
			int sceneIndex = int.Parse (InputManager.clickedButton.Split (new string[] { "_" }, System.StringSplitOptions.RemoveEmptyEntries) [2]);

            Level newLevel = Level.generateLevel("Free", Level.GameType.Free, GameSettings.scenes[sceneIndex]);
			newLevel.vehicleName = Vehicles.instance.getByIndex (GameManager.instance.currentVehicle).name;
			newLevel.description = "Train your tank driving\nand shooting skills";
			GameManager.instance.setCurrentLevel (newLevel);

			MainMenuController.instance.openNextMenu ("Load");

			NativeController.trackEvent("free world " + newLevel.scene);
			NativeController.trackEvent("free start " + newLevel.vehicleName);

			SoundEffects.instance.playSound(SoundEffects.instance.buttonClick);

			SceneLoaderScript.loadScene (GameSettings.scenes [sceneIndex].Replace("battle-",""));
		}
	}*/
}