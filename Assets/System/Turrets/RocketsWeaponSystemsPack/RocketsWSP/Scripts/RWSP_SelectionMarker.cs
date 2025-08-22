using UnityEngine;
using System.Collections;

public class RWSP_SelectionMarker : MonoBehaviour {

	public Transform SelectionModelTrans;
	private Vector3 selectionModelEulers = Vector3.zero;
	private float origSelectionModelScale = 1.0f;
	private float selectionModelScale = 1.0f;

	private float bobbingSpeed = 1.5f;  
	private Vector3 bobbingDir = new Vector3(0.0f, 0.1f, 0); 
	private Vector3 initPos = Vector3.zero;
	private float bobTime = 0;
	private float TWOPI = Mathf.PI * 2;

	// Use this for initialization
	void Start () {
		initPos = SelectionModelTrans.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (SelectionModelTrans != null) {
			bobTime += Time.deltaTime * bobbingSpeed;
			Vector3 movement = transform.TransformDirection(bobbingDir) * Mathf.Sin(TWOPI * bobTime) * 50;
			SelectionModelTrans.localPosition = initPos + movement;

			selectionModelScale = origSelectionModelScale * Mathf.Sin(TWOPI * (bobTime / 4)) * 0.85f;
			if (selectionModelScale < 0.5f) {
				selectionModelScale = 0.5f;
			}
			SelectionModelTrans.localScale = new Vector3(selectionModelScale, selectionModelScale, selectionModelScale);

			selectionModelEulers.x = -90;
			selectionModelEulers.y += 10 * Time.deltaTime;
			SelectionModelTrans.localRotation = Quaternion.Euler(selectionModelEulers);
		}
	}
}
