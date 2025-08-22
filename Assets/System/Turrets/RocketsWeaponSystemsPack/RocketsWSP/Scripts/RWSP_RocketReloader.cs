using UnityEngine;
using System.Collections;

public enum RocketReloaderStates {
	StartLoading,
	Loading,
	FinishedLoading,
	InActive
}

public class RWSP_RocketReloader : MonoBehaviour {

	public GameObject RocketTurretPrefab;
	public int CurrentLevel = 1;
	public void ChangeLevel(int levelIn) {
		if (rocketTurretScript != null) {
			rocketTurretScript.ChangeLevel(levelIn);			
		}
	}
	public Transform RocketTurretTransform;

	private GameObject currentRocketTurret;
	private RWSP_RocketTurret rocketTurretScript;

	// Reloading Variables
	public Animation ReloaderAnimation;
	public RocketReloaderStates CurrentState = RocketReloaderStates.InActive;
	private AudioSource reloadingSFX;

	private float reorientateTurretTimer = 0;
	public float ReorientateTurretTime = 3.0f;

	private float reloadTubeTimer = 0;
	public float ReloadingTubeTime = 1.0f;

	public float ReloaderTurretDiff = 0;

	// Use this for initialization
	void Awake () {
	
		// Generate Rocket Launcher Turret
		if (currentRocketTurret == null) {
			GenerateRocketLauncherTurret();
		}

		// Set Reloading SFX
		reloadingSFX = gameObject.GetComponent<AudioSource>();
	}

	// Generate Rocket Launcher Turret at Turret Transform Position
	private void GenerateRocketLauncherTurret() {
		GameObject newRocketTurret = GameObject.Instantiate(RocketTurretPrefab, RocketTurretTransform.position, gameObject.transform.rotation) as GameObject;
		newRocketTurret.name = "RocketTurret";
		rocketTurretScript = newRocketTurret.GetComponent<RWSP_RocketTurret>();
		rocketTurretScript.ParentReloader = this;
		RocketTurretTransform = newRocketTurret.transform;
		currentRocketTurret = newRocketTurret;
	}

	// Update is called once per frame
	void Update () {

		// Hold Rocket Turret In Place and Update
		if (currentRocketTurret != null) {
			currentRocketTurret.transform.position = RocketTurretTransform.position;

			// Does Rocket Turret Script Exhist?
			if (rocketTurretScript != null) {

				// Check Reload State of Reloader
				if (CurrentState == RocketReloaderStates.InActive) {
					// Begin to Reload Rocket Turret
					if (rocketTurretScript.NeedsReloading) {
						CurrentState = RocketReloaderStates.StartLoading;
					}
				}
				if (CurrentState == RocketReloaderStates.StartLoading) {
					// Reorientate Rocket Turret
					if (reorientateTurretTimer < ReorientateTurretTime) {
						// Do Reorientation

						rocketTurretScript.ReorientateCradle();
						RocketTurretTransform.rotation = Quaternion.Lerp(RocketTurretTransform.rotation, transform.rotation, 4 * Time.deltaTime);
						reorientateTurretTimer += Time.deltaTime;
					}
					else {
						// Reorientation Finished
						rocketTurretScript.UpdateCurrentRotation();
						CurrentState = RocketReloaderStates.Loading;
						if (ReloaderAnimation != null) {
							ReloaderAnimation.Play("ReloadOpen");
							if (reloadingSFX != null) {
								reloadingSFX.Play();
							}
						}
						reorientateTurretTimer = 0;
					}
				}
				if (CurrentState == RocketReloaderStates.Loading) {
					if (!ReloaderAnimation.isPlaying) {
						if (reloadTubeTimer < ReloadingTubeTime) {
							reloadTubeTimer += Time.deltaTime;
						}
						else {
							reloadTubeTimer = 0;
							if (rocketTurretScript.ReloadRocket()) {
								// Did we load a rocket?

								// Do Nothing!
							}
							else {
								// No Rocket Loaded!
								if (ReloaderAnimation != null) {
									ReloaderAnimation.Play("ReloadClose");
									if (reloadingSFX != null) {
										reloadingSFX.Play();
									}
								}
								CurrentState = RocketReloaderStates.FinishedLoading;
							}
						}
					}
				}
				if (CurrentState == RocketReloaderStates.FinishedLoading) {
					if (!ReloaderAnimation.isPlaying) {
						rocketTurretScript.NeedsReloading = false;
						CurrentState = RocketReloaderStates.InActive;
					}
				}
			}
		}

	}
}
