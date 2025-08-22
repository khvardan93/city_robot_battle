using UnityEngine;
using System.Collections;

public class RocketTube : MonoBehaviour {

	public RWSP_RocketTurret ParentTurret;
	public Transform TubeTransform;

	// Rocket Launch Sound
	private AudioSource RocketLaunchSFX;

	public GameObject RocketLoadSFXPrefab;
	private AudioSource RocketLoadSFX;
	private void CreateRocketLoadSFXGO() {
		if (RocketLoadSFX == null) {
			GameObject newRocketLoadSFX = GameObject.Instantiate(RocketLoadSFXPrefab, gameObject.transform.localPosition, Quaternion.identity) as GameObject;
			newRocketLoadSFX.name = "RocketLoadingSFX";
			newRocketLoadSFX.transform.parent = this.gameObject.transform;

			RocketLoadSFX = newRocketLoadSFX.GetComponent<AudioSource>();
		}
	}

	// Rocket Loaded
	public bool RocketLoaded = false;
	public RWSP_Rocket CurrentRocket;

	public ParticleSystem[] FiringParticleSystems;

	public void LoadTube(GameObject rocket) {
		if (TubeTransform != null) {
			if (rocket != null) {
				// Play Loading SoundFX
				if (RocketLoadSFX != null) {
					RocketLoadSFX.Play();
				}

				CurrentRocket = rocket.GetComponent<RWSP_Rocket>();
				CurrentRocket.transform.position = TubeTransform.position;
				CurrentRocket.transform.rotation = TubeTransform.rotation;
				RocketLoaded = true;
			}
		}
	}

	public void RemoveRocket() {
		if (CurrentRocket != null) {
			Destroy(CurrentRocket.gameObject);
			CurrentRocket = null;
			RocketLoaded = false;
		}
	}

	// Firing Variables
	public void FireTube(Transform targetIn, bool canFollowTarget, RocketGuidanceTypes guidanceType, float volleySpread) {
		if (targetIn != null) {
			// Set Rocket Target
			// Fire Rocket!
			Fire(targetIn, canFollowTarget, TubeTransform.position, guidanceType, volleySpread);
		}
		else {
			// Fire Rocket!
			Fire(null, canFollowTarget, TubeTransform.position, guidanceType, volleySpread);
		}
	}

	// Use this for initialization
	void Start () {
		TubeTransform = gameObject.transform;

		// Setup Rocket Launch SFX
		RocketLaunchSFX = gameObject.GetComponent<AudioSource>();

		// Create RocketLoadSFX GO
		CreateRocketLoadSFXGO();

		// Find Firing Particle Systems
		FiringParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentRocket != null) {
			// Hold Rocket in Tube
			CurrentRocket.transform.position = TubeTransform.position;
			CurrentRocket.transform.rotation = TubeTransform.rotation;
		}
	}

	public void DestroyRocket() {
		if (RocketLoaded) {
			CurrentRocket.Detonate(null);
		}
	}

	private void Fire(Transform targetIn, bool canFollowTarget, Vector3 startingPos, RocketGuidanceTypes guidanceType, float volleySpread) {
		if (RocketLoaded) {

			// Play Launch SFX
			if (RocketLaunchSFX != null) {
				RocketLaunchSFX.Play();
			}

			// Fire Particle Systems
			if (FiringParticleSystems.Length > 0) {
				foreach (ParticleSystem part in FiringParticleSystems) {
					part.Play(true);
				}
			}

			// Launch Rocket!
			if (CurrentRocket != null) {
				CurrentRocket.GuidanceType = guidanceType;
				CurrentRocket.transform.parent = null;
				CurrentRocket.Fire(gameObject, targetIn, canFollowTarget, startingPos, volleySpread);
				CurrentRocket = null;
			}

			RocketLoaded = false;
		}
	}
}
