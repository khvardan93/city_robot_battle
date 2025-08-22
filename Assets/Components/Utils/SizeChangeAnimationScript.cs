using UnityEngine;
using System.Collections;

public class SizeChangeAnimationScript : MonoBehaviour {

	public Vector3 scaleSize = new Vector3 (0.5f, 0.5f, 0.5f);
	public float scaleSpeed = 0.1f;
	
	private bool back = false;
	
	private Vector3 startScale;
	
	void Start(){
		startScale = transform.localScale;
	}
	
	void Update(){
		if (!back) {
			//go
			transform.localScale = Vector3.Lerp(transform.localScale, startScale + scaleSize, scaleSpeed);
			
			if(Mathf.Abs(transform.localScale.x - startScale.x - scaleSize.x) < 0.0005f){
				back = true;
			}
		} else {
			//back
			transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleSpeed);
			if(Mathf.Abs(transform.localScale.x - startScale.x) < 0.0005f){
				back = false;
			}
		}
	}
}
