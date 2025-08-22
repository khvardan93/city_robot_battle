using UnityEngine;
using System.Collections;

public enum RocketTypes {
	RocketType1,
	RocketType2,
	RocketType3,
	RocketType4,
	RocketType5,
	RocketType6,
	RocketType7,
	RocketType8,
	RocketType9,
	RocketType10
}

public enum RocketTurretModes {
	SingleFire,
	VolleyFire
}

public class RWSP_RocketTurret : MonoBehaviour {

	public bool ForceFiringAllRockets = false;

	[HideInInspector]
	public RWSP_RocketReloader ParentReloader;

	public Transform RocketCradleTransform;
	private Vector3 rocketCradleEulers = Vector3.zero;

	public RocketGuidanceTypes GuidanceType = RocketGuidanceTypes.Balistic;
	public TextureColorSchemes CurrentColorScheme = TextureColorSchemes.LevelColors;

	public RocketTypes CurrentRocketType = RocketTypes.RocketType1;
	[HideInInspector]
	public int CurrentRocketLauncherLevel = 1;
	public void ChangeLevel(int levelIn) {
		CurrentRocketLauncherLevel = levelIn;
		CurrentRocketType = (RocketTypes)CurrentRocketLauncherLevel;
	}

	// Rocket Ammo Prefabs
	public GameObject[] RocketTypesPrefabs;

	public RocketTurretModes FiringMode = RocketTurretModes.VolleyFire;

	[HideInInspector]
	public int TotalRocketTubes = 0;
	public RocketTube[] RocketTubes;

	public bool AllRocketsFired = true;
	public bool NeedsReloading = true;

	// Rocket Turret Control and Aiming
	[HideInInspector]
	public Transform myTransform;
	[HideInInspector]
	public bool CanControlSelf = true;
	[HideInInspector]
	public float RotationSpeed = 0.15f;

	public float RotationSpeedLow = 5f;
	public float RotationSpeedMedium = 20f;
	public float RotationSpeedHigh = 30f;

	private Vector3 currentRotation = Vector3.zero;
	public bool UpdateCurrentRotation() {
		currentRotation = myTransform.rotation.eulerAngles;
		if (currentRotation != Vector3.zero)
			return true;
		else
			return false;
	}

	// Auto Reloading
	public bool AutoReload = false;
	public float DelayBeforeReloading = 5.0f;

	// Rocket Volley Timers and Variables
	public bool FiringVolley = false;
	public float BalisticTimeToTarget = 5;
	public float BalisticVolleySpread = 10;
	private bool finishedFiring = true;
	private bool CanReload = true;
	private float delayBeforeReloadTimer = 0;
	private int currentVolleyTube = 0;
	private float rocketVolleyTimer = 0;
	public float VolleyTimeBetweenLaunch = 0.25f;
	[HideInInspector]
	public float BalisticAngle = 0;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;

		RocketTubes = gameObject.GetComponentsInChildren<RocketTube>();
		TotalRocketTubes = RocketTubes.Length;

		currentRotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		// Find Target
		if (Target != null) {
			//Target = GameObject.FindGameObjectWithTag("Target").transform;
			BalisticAngle = Vector3.Angle(gameObject.transform.forward, calculateBestThrowAngle(gameObject.transform.position, Target.transform.position, BalisticTimeToTarget));
		}

		if (ForceFiringAllRockets) {
			ForceFireAllRocketTubes();
			ForceFiringAllRockets = false;
		}

		if (FiringVolley) {
			CanReload = false;
			finishedFiring = false;
			if (rocketVolleyTimer < VolleyTimeBetweenLaunch) {
				rocketVolleyTimer += Time.deltaTime;
			}
			else {
				if (currentVolleyTube < RocketTubes.Length) {
					// Fire Tube
					RocketTubes[currentVolleyTube].FireTube(Target, false, GuidanceType, BalisticVolleySpread);
					currentVolleyTube++;
				}
				else {
					// End Volley Firing
					currentVolleyTube = 0;
					finishedFiring = true;
					delayBeforeReloadTimer = DelayBeforeReloading;
					FiringVolley = false;
				}
				rocketVolleyTimer = 0;
			}
		}

		if (finishedFiring) {
			if (!CanReload) {
				if (delayBeforeReloadTimer > 0) {
					delayBeforeReloadTimer -= Time.deltaTime;
				}
				else {
					CanReload = true;
				}
			}
		}

		if (NeedsReloading) {
			// Reload Rocket Turret
			CanControlSelf = false;

			// Auto Reload
			if (ParentReloader == null) {
				AutoReload = true;
				AutoReloadTubes();
			}
		}
		else {
			// Check Tubes for Load Status
			AllRocketsFired = true;
			for (int i = 0; i < RocketTubes.Length; i++) {
				if (RocketTubes[i].RocketLoaded) {
					AllRocketsFired = false;
				}
			}
			if (AllRocketsFired) {
				if (CanReload) {
					NeedsReloading = true;
				}
			}
			else {
				// Rocket Turret Has Remaining Rockets, ready to fire!
				CanControlSelf = true;
			}
		}

		if (CanControlSelf) {
			if (Target != null) {
				// Track Target
				float distanceToTarget = Vector3.Distance(Target.position, myTransform.position);
				TargetTrackOffset.y = distanceToTarget * 0.75f;
				Vector3 targetTrackPosition = Target.position + TargetTrackOffset;
				TrackPosition(targetTrackPosition);
			}
			//currentRotation.y += RotationSpeed * Time.deltaTime;
			//myTransform.rotation = Quaternion.Euler(currentRotation);
		}
	}

	private Vector3 calculateBestThrowAngle(Vector3 origin, Vector3 target, float timeToTarget) {
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;
		
		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;
		
		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;
		
		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized; // get direction of xz but with magnitude 1
		result *= v0xz; // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y; // set y to v0y (starting speed of y plane)
		
		return result;
	}

	// Reload Tubes Without External Reloader
	private float autoReloadingTimer = 0;
	public float AutoReloadTimeBetweenTubes = 0.25f;
	private void AutoReloadTubes() {
		if (autoReloadingTimer < AutoReloadTimeBetweenTubes) {
			autoReloadingTimer += Time.deltaTime;
		}
		else {
			autoReloadingTimer = 0;

			// Try Loading A Rocket
			if (ReloadRocket()) {
				// Did we load a rocket?
				
				// Do Nothing!
			}
			else {
				// No Rocket Loaded! Done Auto Reloading
				NeedsReloading = false;
			}
		}
	}

	public bool ReloadRocket() {
		for (int i = 0; i < RocketTubes.Length; i++) {
			if (!RocketTubes[i].RocketLoaded) {
//				int tubeNumber = i + 1;
//				Debug.Log("Reloaded Tube " + tubeNumber.ToString());

				GameObject newRocketType1 = GameObject.Instantiate(RocketTypesPrefabs[(int)CurrentRocketType], Vector3.zero, Quaternion.identity) as GameObject;
				newRocketType1.name = "RocketType1";
				RocketTubes[i].LoadTube(newRocketType1);

				RocketTubes[i].RocketLoaded = true;
				return true;
			}
		}
		return false;
	}

	public bool ForceReloadRockets() {
		for (int i = 0; i < RocketTubes.Length; i++) {
			if (RocketTubes[i].RocketLoaded) {
				RocketTubes[i].RemoveRocket();
			}

			if (!RocketTubes[i].RocketLoaded) {
//				int tubeNumber = i + 1;
				//				Debug.Log("Reloaded Tube " + tubeNumber.ToString());
				
				GameObject newRocketType1 = GameObject.Instantiate(RocketTypesPrefabs[(int)CurrentRocketType], Vector3.zero, Quaternion.identity) as GameObject;
				newRocketType1.name = "RocketType1";
				RocketTubes[i].LoadTube(newRocketType1);
				
				RocketTubes[i].RocketLoaded = true;
				return true;
			}
		}
		return false;
	}

	public void ForceFireAllRocketTubes() {
		// Fire All Rockets
		if (AimingAtTarget) {
			for (int i = 0; i < RocketTubes.Length; i++) {
				RocketTubes[i].FireTube(Target, false, GuidanceType, BalisticVolleySpread);
			}
		}
	}

	public void FireRocketTurret() {
		if (AimingAtTarget) {
			if (FiringMode == RocketTurretModes.VolleyFire) {
				if (!FiringVolley) {
					FiringVolley = true;
				}
			}
			else if (FiringMode == RocketTurretModes.SingleFire) {
				// Fire Next Tube
				FireNextLauncherTube();
			}
		}
	}

	public void FireNextLauncherTube() {
		// Fire Tube
		if (AimingAtTarget) {
			if (currentVolleyTube < RocketTubes.Length) {
				// Fire Tube
				RocketTubes[currentVolleyTube].FireTube(Target, false, GuidanceType, BalisticVolleySpread);
				currentVolleyTube++;
			}
			else {
				// End Volley Firing
				currentVolleyTube = 0;
				finishedFiring = true;
				delayBeforeReloadTimer = DelayBeforeReloading;
			}
		}
	}

	// Turret Target Position Tracking

	public Transform Target;
	void SetTarget(Transform targetIn) {
		Target = targetIn;
	}
	public Vector3 TargetTrackOffset = Vector3.zero;

//	private Vector3 LookDirection = Vector3.zero;
	private float DirectionAngle = 0;
	public float TargetAngle = 0;
	public float TargetXAngle = 0;
	public bool AimingAtTarget = false;
	public float DiffFromTargetAngle = 0;

	public float AngleMAXVector = 12;
	public float AngleMINVector = 6;
	public float AngleToTargetDiff = 0;
	public float TargetDirectionDiff = 0;

	public void ReorientateCradle() {
		rocketCradleEulers = Vector3.Lerp(rocketCradleEulers, Vector3.zero, RotationSpeed * Time.deltaTime);
		RocketCradleTransform.localEulerAngles = rocketCradleEulers;
	}

	private void TrackPosition(Vector3 positionToTrack) {
		// Track Target
		Vector3 targetPosition = positionToTrack;		
		Vector3 v = targetPosition - myTransform.forward;
		DirectionAngle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
		
		// Difference From Target Angle
		Vector3 angleToTarget = targetPosition - myTransform.position;
		float DifferenceFromTargetAngle = Vector3.Angle(myTransform.forward, angleToTarget);
		Vector3 cross = Vector3.Cross(myTransform.forward, angleToTarget);
		if (cross.y < 0) DifferenceFromTargetAngle = -DifferenceFromTargetAngle;
				
		DirectionAngle = Vector3.Angle(angleToTarget, myTransform.forward);
		cross = Vector3.Cross(angleToTarget, myTransform.forward);
		if (cross.y < 0) DirectionAngle = -DirectionAngle;

		AngleToTargetDiff = Mathf.Abs(Vector3.Angle(myTransform.forward, angleToTarget));
		TargetAngle = Mathf.Lerp(TargetAngle, DirectionAngle, (RotationSpeed / 4) * Time.deltaTime);

		if (TargetAngle < -AngleMAXVector) {
			myTransform.RotateAround(myTransform.position, myTransform.up, (RotationSpeed / 4) * Time.deltaTime);
			AimingAtTarget = false;
		}
		if (TargetAngle > AngleMAXVector) {
			myTransform.RotateAround(myTransform.position, myTransform.up, -(RotationSpeed / 4) * Time.deltaTime);
			AimingAtTarget = false;
		}

		if (TargetAngle > -AngleMAXVector && TargetAngle < AngleMAXVector) {
			AimingAtTarget = true;
		}
		
		float DifferenceFromTargetXAngle = Vector3.Angle(myTransform.up, angleToTarget);
		cross = Vector3.Cross(myTransform.up, angleToTarget);
		if (cross.y < 0) {
			DifferenceFromTargetXAngle = -DifferenceFromTargetXAngle;
		}

		float fixedXAngle = DifferenceFromTargetXAngle - 90;
		TargetXAngle = Mathf.LerpAngle(TargetXAngle, Mathf.Abs(fixedXAngle), RotationSpeed * Time.deltaTime);

		// Difference from Target Y Angle
		DiffFromTargetAngle = Mathf.Abs(TargetAngle);
		if (DiffFromTargetAngle > 15) {
			RotationSpeed = RotationSpeedHigh;
		}
		else if (DiffFromTargetAngle <= 15 && DiffFromTargetAngle > 5) {
			RotationSpeed = RotationSpeedMedium;
		}
		else if (DiffFromTargetAngle < 5) {
			RotationSpeed = RotationSpeedLow;
		}

//		if (GuidanceType == RocketGuidanceTypes.Homing) {
//			rocketCradleEulers.x = Mathf.Lerp(rocketCradleEulers.x, -TargetXAngle, RotationSpeed * Time.deltaTime);
//			RocketCradleTransform.localEulerAngles = rocketCradleEulers;
//		}
//		else if (GuidanceType == RocketGuidanceTypes.Balistic) {
			// Use Balistic Angle
		rocketCradleEulers.x = Mathf.Lerp(rocketCradleEulers.x, -BalisticAngle, RotationSpeed * Time.deltaTime);
		RocketCradleTransform.localEulerAngles = rocketCradleEulers;
//		}

		//CurrentMARSManager.CurrentXAngle = fixedXAngle;
	}
}
