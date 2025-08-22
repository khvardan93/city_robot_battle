using UnityEngine;
using System.Collections;

public class RWSP_TargetBunker : MonoBehaviour {

	public Transform BunkerDestroyedTrans;
	public GameObject ExplosionPrefab;

	public float BunkerHealth = 100;
	public float BunkerHealthMAX = 100;
	public float BunkerHealthRegen = 0.25f;
	private float healthRegenTimer = 0;
	public float BunkerHealthReginFreq = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Bunker Destruction
		if (BunkerHealth <= 0) {
			ExplodeBunker();
		}

		// Regen Health
		if (BunkerHealth < BunkerHealthMAX) {
			if (healthRegenTimer < BunkerHealthReginFreq) {
				healthRegenTimer += Time.deltaTime;
			}
			else {
				BunkerHealth += BunkerHealthRegen;
				healthRegenTimer = 0;
			}
		}
	}

	void Damage(float damageIn) {
		BunkerHealth -= damageIn;
		if (BunkerHealth < 0) {
			BunkerHealth = 0;
		}
	}

	private void ExplodeBunker() {
		// Seperate and Activate Bunker Destroyed Model
		if (BunkerDestroyedTrans != null) {
			BunkerDestroyedTrans.parent = null;
			BunkerDestroyedTrans.gameObject.SetActive(true);
		}

		// Spawn Destruction Explosion
		if (ExplosionPrefab != null) {
			GameObject newExplosion = GameObject.Instantiate(ExplosionPrefab, transform.position, Quaternion.identity) as GameObject;
			newExplosion.name = "NewExplosion";
		}

		Destroy(gameObject);
	}
}
