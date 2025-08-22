using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAnimationScript : MonoBehaviour {
	public float animationSpeed = 0.6f;
	public float animationMagnitude = 5f;
	public float animationaStartDelay = 1f;
	public float extinctionStep = 20f;
	public float timeInatervalBetweenAnimations = 5f; 

	private Quaternion defaultRotarion;
	private Vector3 rotationTarget;
	public bool isAnimate = true; 
	private float currentAngle;
	void Start () {
		this.defaultRotarion = this.transform.localRotation;
		Invoke("animate", this.animationaStartDelay);
	}

	void Update () {
		if (this.isAnimate) {
			this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(this.rotationTarget), this.animationSpeed);

			if (Mathf.Abs( this.transform.localRotation.eulerAngles.z - this.rotationTarget.z ) <= 1.5f || Mathf.Abs(this.transform.localRotation.eulerAngles.z - 360f - this.rotationTarget.z) <= 1.5f) {
				if (this.rotationTarget == Vector3.zero) {
					this.isAnimate = false;
					Invoke ("animate", this.timeInatervalBetweenAnimations);
					return;
				}
				this.rotationTarget *= -1f;
				this.rotationTarget -= this.rotationTarget / this.extinctionStep;
				if (this.rotationTarget.magnitude <= 2f) {
					this.rotationTarget = Vector3.zero;
				}
			}
		}
	}

	private void animate(){
		this.rotationTarget = new Vector3(0, 0, this.animationMagnitude);
		this.isAnimate = true;
	}
}
