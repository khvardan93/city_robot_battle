using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float LifeTime = 30.0f;
	private float lifeTimer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeTimer += Time.deltaTime;
		if (lifeTimer > LifeTime) {
			Destroy(gameObject);
		}
	}
}
