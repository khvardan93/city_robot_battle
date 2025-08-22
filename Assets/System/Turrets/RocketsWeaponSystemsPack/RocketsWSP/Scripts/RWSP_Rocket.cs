using UnityEngine;
using System.Collections;

public enum RocketGuidanceTypes {
	Balistic,
	Homing
}

public class RWSP_Rocket : MonoBehaviour {

	// Rocket Type or Rocket Level
	public RocketTypes RocketType = RocketTypes.RocketType1;

	// External Prefabs
	public Transform explosionPrefab;

	public RocketGuidanceTypes GuidanceType = RocketGuidanceTypes.Balistic;

	// Damage Variables
	public float RocketDamage = 10;

	// Targeting Variables
	public Transform target;
	public bool GotTarget = false;
	public float targetAquisitionDelay = 6.0f;
	private float targetAquTimer = 0;

	// LifeTime Variables
	private float currentLifeTime = 0;
	/// <summary>
	/// The total life time of the rocket.
	/// </summary>
	public float LifeTime = 160;

	// Rocket Balalistic Variables
	public float BalisticAngle = 45;
	private float BalisticTimeToTarget = 5.0f;
	private Vector3 throwSpeed = Vector3.zero;
	private Vector3 startingPosition = Vector3.zero;
	public void SetStartingPosition(Vector3 startingPos) {
		startingPosition = startingPos;
	}

	// Rocket Guidance Variables
	private Transform meshTransform;

	public float MaxTurnRate = 180.0f;	// turn rate in degrees per second
	public float MaxThrust = 10.0f;		// maximum acceleration
	public float ThrustRamp = 3.0f;		// how fast full thrust activates
	public float TurnRamp = 3.0f;		// how fast full turning activates
	public float NC = 3.0f;				// set between 3 and 5
	public float IgnitionStartDelay = 1.0f;	// How long after launch before thrusting
	public bool DestroyTrail = true;  // Destroy Rocket Trail on collision (Usually set to false)

	// Realtime Variables
	private bool rocketLaunched = false;
	private float activeTime;
	[SerializeField] Rigidbody myRigidbody;
	public ParticleSystem myThrusterParticleSystem;

	public TrailRenderer myTrailRenderer;
	public float DestroyTrailTime = 3.0f;

	private float currentThrust = 0.0f;
	private float currentTurnRate = 0.0f;
	private Vector3 lineOfSight = Vector3.zero;  // line of sight
	private Vector3 myAcceleration = Vector3.zero;

	private GameObject modTargetTrans;

	private BoxCollider myCollider;

	void Awake() {
		myCollider = gameObject.GetComponent<BoxCollider>();
		myCollider.enabled = false;
	}

	// Use this for initialization
	void Start () {
		if (myThrusterParticleSystem) {
			ParticleSystem.EmissionModule thrusterEmission = myThrusterParticleSystem.emission;
			thrusterEmission.enabled = false;
		}
		if (myTrailRenderer != null)
			myTrailRenderer.enabled = false;

		MeshRenderer rocketMesh = gameObject.GetComponentInChildren<MeshRenderer>();
		if (rocketMesh != null) {
			meshTransform = rocketMesh.transform;
		}
	}

	public void Fire(GameObject launcher, Transform target, bool canFollowTarget, Vector3 startingPos, float volleySpread) {

		activeTime = Time.time;
		rocketLaunched = true;
		SetStartingPosition(startingPos);

		float rocketLevel = (float)RocketType + 1.0f;
		if (myTrailRenderer != null) {
			myTrailRenderer.startWidth = rocketLevel * 0.2f;
			myTrailRenderer.endWidth = rocketLevel * 0.2f;
		}

		if (GuidanceType == RocketGuidanceTypes.Homing) {
			this.target = target;

//			GameObject newTargetPos = new GameObject("RocketTargetPosition");
//			Vector3 targetingOffset = new Vector3(Random.Range(-volleySpread, volleySpread), 0, Random.Range(-volleySpread, volleySpread));
//			targetingOffset = target.position + targetingOffset;
//			targetingOffset.y = 0;
//			newTargetPos.transform.position = targetingOffset;
//			modTargetTrans = newTargetPos;
//			this.target = newTargetPos.transform;
		}
		else if (GuidanceType == RocketGuidanceTypes.Balistic) {
			Vector3 targetingOffset = new Vector3(Random.Range(-volleySpread, volleySpread), 0, Random.Range(-volleySpread, volleySpread));
			targetingOffset = target.position + targetingOffset;
			targetingOffset.y = 0;

			this.target = target;

			if (GuidanceType == RocketGuidanceTypes.Balistic) {
				// Calculate BalisticTimeToTarget
//				float distanceToTarget = Vector3.Distance(newTargetPos.transform.position, gameObject.transform.position);
//				float timeToTargetThrust = 10 / distanceToTarget;
//				BalisticTimeToTarget = (timeToTargetThrust * 25f) * ThrustRamp;

				throwSpeed = calculateBestThrowSpeed(startingPosition, target.transform.position, BalisticTimeToTarget);
				GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
				if (GuidanceType == RocketGuidanceTypes.Balistic) {
					myRigidbody.useGravity = true;
				}
			}
		}

		if (myThrusterParticleSystem) {
			ParticleSystem.EmissionModule thrusterEmission = myThrusterParticleSystem.emission;
			thrusterEmission.enabled = true;
		}
		if (myTrailRenderer != null)
			myTrailRenderer.enabled = true;

		// don't collide with our own launcher
		

		if (target != null)
			GotTarget = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (rocketLaunched) {
			currentLifeTime += Time.deltaTime;
			activeTime += Time.deltaTime;
		}
	}

	public void FixedUpdate()
	{
		if (!rocketLaunched)
			return;

		if (Time.time - currentLifeTime < IgnitionStartDelay)
		{
			return;
		}

		if (targetAquTimer < targetAquisitionDelay)
			targetAquTimer += Time.deltaTime;
		if (targetAquTimer >= targetAquisitionDelay)
			if (!myCollider.enabled)
				myCollider.enabled = true;

		if (myThrusterParticleSystem != null)
		{
			myThrusterParticleSystem.Play();
		}
		if (myTrailRenderer != null)
		{
			myTrailRenderer.enabled = true;
		}

		if (currentThrust < MaxThrust)
		{
			// don't go over in case thrustRamp is very small
			float increase = Time.fixedDeltaTime * MaxThrust / ThrustRamp;
			currentThrust = Mathf.Min(currentThrust + increase, MaxThrust);
		}
		if (currentTurnRate < MaxTurnRate)
		{
			float increase = Time.fixedDeltaTime * MaxTurnRate / TurnRamp;
			currentTurnRate = Mathf.Min(currentTurnRate + increase, MaxTurnRate);
		}

		Vector3 prevLos = lineOfSight;
		if (target != null)
		{
			lineOfSight = target.position - transform.position;
		}
		else
		{
			lineOfSight = transform.forward;
		}

		//		Vector3 balisticForce = Vector3.zero;
		if (GuidanceType == RocketGuidanceTypes.Homing)
		{
			Vector3 dLos = lineOfSight - prevLos;
			dLos = dLos - Vector3.Project(dLos, lineOfSight);
			myAcceleration = Time.fixedDeltaTime * lineOfSight + dLos * NC + Time.fixedDeltaTime * myAcceleration * NC / 2;
		}
		else if (GuidanceType == RocketGuidanceTypes.Balistic)
		{
			Vector3 balisticForce = Vector3.zero;
			if (balisticForce == Vector3.zero)
				balisticForce = BallisticVel(BalisticAngle);
		}

		if (GotTarget)
		{
			if (target != null)
			{
				if (GuidanceType == RocketGuidanceTypes.Homing)
				{
					// Homing Rocket

					// limit acceleration to our maximum thrust
					myAcceleration = Vector3.ClampMagnitude(myAcceleration * currentThrust, MaxThrust);

					//					float distanceToTarget = Vector3.Distance(target.position, gameObject.transform.position);

					
						Quaternion targetRotation = Quaternion.LookRotation(myAcceleration, transform.up);
						transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * currentTurnRate * 100);
					

					myRigidbody.AddForce(transform.forward * myAcceleration.magnitude * 10);
					if (GetComponent<Rigidbody>().linearVelocity != Vector3.zero)
					{
						if (meshTransform != null)
							meshTransform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().linearVelocity);
					}
				}
				else if (GuidanceType == RocketGuidanceTypes.Balistic)
				{
					// Balistic Rocket
					//myRigidbody.velocity = balisticForce;
					if (GetComponent<Rigidbody>().linearVelocity != Vector3.zero)
						transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().linearVelocity);
					//myRigidbody.AddForce(balisticForce, ForceMode.Acceleration);
				}
				//			if (distanceToTarget < 20) {
				//				MaxThrust += 0.1f * Time.deltaTime;
				//				target = null;
				//			}
			}
			else
			{
				GotTarget = false;


				//if (myRigidbody.velocity.sqrMagnitude < MaxThrust)
				myRigidbody.AddForce(transform.forward * myAcceleration.magnitude, ForceMode.Acceleration);

				if (currentLifeTime > LifeTime)
				{
					Detonate(null);
				}
			}
		}
		else
		{
			// No Target

			//if (myRigidbody.velocity.sqrMagnitude < MaxThrust)
			myRigidbody.AddForce(transform.forward * 10);

			//if (!myRigidbody.useGravity)
				//myRigidbody.useGravity = true;

			if (currentLifeTime > LifeTime)
			{
				Detonate(null);
			}
		}
	}

	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
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

	private Vector3 BallisticVel(float angle) {
		Vector3 dir = target.position - transform.position;  // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		float a = angle * Mathf.Deg2Rad;  // convert angle to radians
		dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
		dist += h / Mathf.Tan(a);  // correct for small height differences
		// calculate the velocity magnitude
		float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
		return vel * dir.normalized;
	}
	
	void OnTriggerEnter(Collider other) {						
		// hit!
		if (other.tag != "Rocket") {
			Detonate(other);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "Rocket")
		{
			Detonate(collision.collider);
		}
	}

    public void Detonate(Collider hit) {

		// Damage Hit Object
		if (hit != null)
			hit.gameObject.SendMessage("Damage", RocketDamage, SendMessageOptions.DontRequireReceiver);

		if (modTargetTrans != null) {
			Destroy(modTargetTrans.gameObject);
		}

		if (!DestroyTrail) {
			// detach trail so it will remain behind
			if (myThrusterParticleSystem != null) {
				Destroy(myThrusterParticleSystem);
			}
			if (myTrailRenderer != null) {
				myTrailRenderer.time = 3.0f;
				myTrailRenderer.transform.parent = null;
				Destroy(myTrailRenderer, DestroyTrailTime);
			}
		}

		if (myThrusterParticleSystem != null) {
			myThrusterParticleSystem.transform.parent = null;
			Destroy(myThrusterParticleSystem.gameObject, 3);
		}

		if (explosionPrefab != null) {
			Transform explosion = (Transform)Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			explosion.GetComponent<RWSP_ExplosionLevelModifier>().SetRocketType(RocketType);
			//explosion.rigidbody.velocity = myRigidbody.velocity;
		}
		Destroy(this.gameObject);
	}
}
