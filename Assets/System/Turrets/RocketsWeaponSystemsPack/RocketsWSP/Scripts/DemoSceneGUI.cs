using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoSceneGUI : MonoBehaviour {

	public string packageNameLabel = "Rockets - Weapon Systems Pack ";
	public string versionLabel = " v1.0";
	public string packageCreatorLabel = "Created by, Daniel Kole Productions";

	public MouseClickController MouseController;

	public GameObject SelectionMarker;
	public RWSP_RocketTurret SelectedTurret;

	public bool GuiHidden = false;

	private int currentDemoCamera = 0;
	public List<Camera> DemoCameras;
	private void ChangeDemoCamera() {
		if (currentDemoCamera < DemoCameras.Count - 1) {
			currentDemoCamera++;
		}
		else {
			currentDemoCamera = 0;
		}
		ActivateCamera(currentDemoCamera);
	}
	private void ActivateCamera(int cameraToActivate) {
		// Activate Camera
		for (int i = 0; i < DemoCameras.Count; i++) {
			if (i == cameraToActivate) {
				// Activate Camera
				DemoCameras[i].enabled = true;
//				DemoCameras[i].gameObject.GetComponent<AudioListener>().enabled = true;
			}
		}
		for (int i = 0; i < DemoCameras.Count; i++) {
			if (i != cameraToActivate) {
				// Deactivate Camera
				DemoCameras[i].enabled = false;
//				DemoCameras[i].gameObject.GetComponent<AudioListener>().enabled = false;
			}
		}
	}
	public Camera GetActiveCamera() {
		return DemoCameras[currentDemoCamera];
	}

	// Use this for initialization
	void Start () {
		MouseController = GameObject.FindObjectOfType<MouseClickController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.C)) {
			ChangeDemoCamera();
		}
		if (Input.GetKeyUp(KeyCode.G)) {
			ChangeGuidanceType();
		}
		if (Input.GetKeyUp(KeyCode.L)) {
			ChangeRocketType();
		}
		if (Input.GetKeyUp(KeyCode.V)) {
			ChangeVolleySpread();
		}
		if (Input.GetKeyUp(KeyCode.M)) {
			ChangeRocketFiringMode();
		}
		if (Input.GetKeyUp(KeyCode.Tab)) {
			ChangeTurretSelection();
		}
		if (Input.GetKeyUp(KeyCode.Backspace)) {
			ChangeColorScheme();
		}
		if (Input.GetKeyUp(KeyCode.Escape)) {
			SelectedTurret = null;
		}
		if (Input.GetKeyUp(KeyCode.H)) {
			GuiHidden = !GuiHidden;
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			// Fire All Rocket Launchers at Target Position
			if (MouseController.TargetLocationTransform.position != Vector3.zero) {
				if (MouseController.AvailableRocketTurrets.Length > 0) {
					if (SelectedTurret == null) {
						for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
							if (!MouseController.AvailableRocketTurrets[i].NeedsReloading) {
								MouseController.AvailableRocketTurrets[i].FireRocketTurret();
							}
						}
					}
					else {
						SelectedTurret.FireRocketTurret();
					}
				}
			}
		}

		// Update Selection Marker
		if (SelectionMarker != null) {
			if (SelectedTurret != null) {
				if (!SelectionMarker.activeSelf) {
					SelectionMarker.SetActive(true);
				}
				SelectionMarker.transform.position = SelectedTurret.myTransform.position;
			}
			else {
				if (SelectionMarker.activeSelf) {
					SelectionMarker.SetActive(false);
				}
			}
		}
	}

	private int selectedTurret = 0;
	private void ChangeTurretSelection() {
		if (selectedTurret < MouseController.AvailableRocketTurrets.Length - 1) {
			selectedTurret++;
		}
		else {
			selectedTurret = 0;
		}
		SelectedTurret = MouseController.AvailableRocketTurrets[selectedTurret];
	}

	private RocketGuidanceTypes currentGuidanceType = RocketGuidanceTypes.Balistic;
	private void ChangeGuidanceType() {
		for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
			if (MouseController.AvailableRocketTurrets[i].GuidanceType == RocketGuidanceTypes.Balistic) {
				MouseController.AvailableRocketTurrets[i].GuidanceType = RocketGuidanceTypes.Homing;
				currentGuidanceType = RocketGuidanceTypes.Homing;
			}
			else if (MouseController.AvailableRocketTurrets[i].GuidanceType == RocketGuidanceTypes.Homing) {
				MouseController.AvailableRocketTurrets[i].GuidanceType = RocketGuidanceTypes.Balistic;
				currentGuidanceType = RocketGuidanceTypes.Balistic;
			}
		}
	}

	private float currentVolleySpread = 10;
	private float currentVolleySpreadMAX = 10;
	private void ChangeVolleySpread() {
		if (currentVolleySpread > 0) {
			currentVolleySpread--;
		}
		else {
			currentVolleySpread = currentVolleySpreadMAX;
		}
		for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
			MouseController.AvailableRocketTurrets[i].BalisticVolleySpread = currentVolleySpread;
		}
	}

	private TextureColorSchemes currentColorScheme = TextureColorSchemes.LevelColors;
	private void ChangeColorScheme() {
		if (currentColorScheme == TextureColorSchemes.LevelColors) {
			currentColorScheme = TextureColorSchemes.Normal;
		}
		else if (currentColorScheme == TextureColorSchemes.Normal) {
			currentColorScheme = TextureColorSchemes.Hyper;
		}
		else if (currentColorScheme == TextureColorSchemes.Hyper) {
			currentColorScheme = TextureColorSchemes.LevelColors;
		}
		
		for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
			MouseController.AvailableRocketTurrets[i].CurrentColorScheme = currentColorScheme;
		}
	}

	private int currentRocketType = 0;
	private void ChangeRocketType() {
		if (currentRocketType < 9) {
			currentRocketType++;
		}
		else {
			currentRocketType = 0;
		}

		for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
			MouseController.AvailableRocketTurrets[i].CurrentRocketType = (RocketTypes)currentRocketType;
		}
	}

	private RocketTurretModes currentRocketFireMode = RocketTurretModes.VolleyFire;
	private void ChangeRocketFiringMode() {
		if (currentRocketFireMode == RocketTurretModes.VolleyFire) {
			currentRocketFireMode = RocketTurretModes.SingleFire;
		}
		else if (currentRocketFireMode == RocketTurretModes.SingleFire) {
			currentRocketFireMode = RocketTurretModes.VolleyFire;
		}
		
		for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
			MouseController.AvailableRocketTurrets[i].FiringMode = currentRocketFireMode;
		}
	}

	void OnGUI () {
		if (!GuiHidden) {

			GUI.Label(new Rect(10, 10, 250, 50), packageNameLabel + versionLabel + "\n" + packageCreatorLabel);

			string instructionsString = "Instructions:";
			instructionsString += "\n" + "Press 'H' Key To Hide/Show Gui Information.";
			instructionsString += "\n" + "Press 'BACKSPACE' Key To Cycle Through Included Color Schemes.";
			instructionsString += "\n" + "Click Left Mouse Button On Ground To Select Target Location.";
			instructionsString += "\n" + "Press 'SPACEBAR' Key To Fire Available Rocket Launchers.";
			instructionsString += "\n" + "Press 'C' Key To Change Current Camera.";
			instructionsString += "\n" + "Press 'G' Key To Change Current Rocket Guidance Type(Homing/Balistic). Current Mode: " + currentGuidanceType.ToString();
			instructionsString += "\n" + "Press 'L' Key To Change Rocket Level (1-10). Current Level: " + currentRocketType.ToString();
			instructionsString += "\n" + "Press 'M' Key To Select Rocket Firing Mode. Current Mode: " + currentRocketFireMode.ToString();
			instructionsString += "\n" + "Press 'TAB' Key To Cycle through Individual Turret Selections.";
			if (SelectedTurret != null) {
				instructionsString += "\n" + "Press 'ESC' Key To Select All Rocket Turrets.";
			}
			GUI.Label(new Rect(10, 40, 650, 360), instructionsString);

			if (SelectedTurret != null) {
				Rect turretInfoRect = new Rect(600, 10, 450, 120);
				if (MouseController != null) {
					bool readyToFire = false;
					if (!SelectedTurret.NeedsReloading && !SelectedTurret.AllRocketsFired) {
						readyToFire = true;
					}
					else {
						readyToFire = false;
					}
					string turretInfoString = SelectedTurret.name + " Info: ";
					turretInfoString += "\n" + "Ready To Fire: " + readyToFire.ToString();
					if (SelectedTurret.GuidanceType == RocketGuidanceTypes.Balistic)
						turretInfoString += " || Volley Spread (Radius to Land Around Target): " + SelectedTurret.BalisticVolleySpread.ToString();
					turretInfoString += "\n" + "Guidance Type: " + SelectedTurret.GuidanceType.ToString();
					turretInfoString += " || Rocket Type (Level): " + SelectedTurret.CurrentRocketType.ToString();
					GUI.Label(turretInfoRect, turretInfoString);

	//				if (MouseController.AvailableRocketTurrets.Length > 0) {
	//					for (int i = 0; i < MouseController.AvailableRocketTurrets.Length; i++) {
	//						bool readyToFire = false;
	//						if (!MouseController.AvailableRocketTurrets[i].NeedsReloading && !MouseController.AvailableRocketTurrets[i].AllRocketsFired) {
	//							readyToFire = true;
	//						}
	//						else {
	//							readyToFire = false;
	//						}
	//
	//						string turretInfoString = MouseController.AvailableRocketTurrets[i].name + " #" + i.ToString() + " Info: ";
	//						turretInfoString += "\n" + "Ready To Fire: " + readyToFire.ToString();
	//						if (MouseController.AvailableRocketTurrets[i].GuidanceType == RocketGuidanceTypes.Balistic)
	//							turretInfoString += " || Volley Spread (Radius to Land Around Target): " + MouseController.AvailableRocketTurrets[i].BalisticVolleySpread.ToString();
	//						turretInfoString += "\n" + "Guidance Type: " + MouseController.AvailableRocketTurrets[i].GuidanceType.ToString();
	//						turretInfoString += " || Rocket Type (Level): " + MouseController.AvailableRocketTurrets[i].CurrentRocketType.ToString();
	//						GUI.Label(turretInfoRect, turretInfoString);
	//						turretInfoRect.y += 52;
	//					}
	//				}
				}
			}

	//		if (GUI.Button(new Rect(10, 200, 100, 30), "Change Turret Level")) {
	//			Debug.Log("Clicked the button with text");
	//		}

		}
	}
}
