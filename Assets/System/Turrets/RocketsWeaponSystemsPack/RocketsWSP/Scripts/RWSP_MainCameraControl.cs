using UnityEngine;
using System.Collections;

public class RWSP_MainCameraControl : MonoBehaviour {

	private Transform myTransform;
	public Transform CameraTransform;
	public Transform CameraLookAtTransform;

	private float currentCameraLookingHeight = 3;
	public float CameraLookHeightSpeed = 10;
	public float cameraLookingHeightMIN = 0.5f;
	public float cameraLookingHeightMAX = 35;
	public float cameraHeightMIN = 1;
	public float cameraHeightMAX = 10;
	private float currentCameraHeight = 1;

	public float CameraRotationSpeed = 50;
	private Vector3 cameraRotationEulers = Vector3.zero;
	private Vector3 LookAtPosition = Vector3.zero;
	private Vector3 CameraPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
		cameraRotationEulers = new Vector3(0, 0, 0);
		LookAtPosition.y = 1;

		if (CameraTransform != null) {
			CameraPosition = CameraTransform.localPosition;
			currentCameraHeight = CameraPosition.y;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			// Rotate Camera Left
			cameraRotationEulers.y += CameraRotationSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)) {
			// Rotate Camera Right
			cameraRotationEulers.y -= CameraRotationSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.W)) {
			// Rotate Camera Up
			if (currentCameraLookingHeight < cameraLookingHeightMAX)
				currentCameraLookingHeight += CameraLookHeightSpeed * Time.deltaTime;
			else
				currentCameraLookingHeight = cameraLookingHeightMAX;
		}
		if (Input.GetKey(KeyCode.S)) {
			// Rotate Camera Down
			if (currentCameraLookingHeight > cameraLookingHeightMIN)
				currentCameraLookingHeight -= CameraLookHeightSpeed * Time.deltaTime;
			else
				currentCameraLookingHeight = cameraLookingHeightMIN;
		}

		if (Input.GetKey(KeyCode.E)) {
			// Move Camera Up
			if (currentCameraHeight < cameraHeightMAX)
				currentCameraHeight += CameraLookHeightSpeed * Time.deltaTime;
			else
				currentCameraHeight = cameraHeightMAX;
		}
		if (Input.GetKey(KeyCode.Q)) {
			// Move Camera Down
			if (currentCameraHeight > cameraHeightMIN)
				currentCameraHeight -= CameraLookHeightSpeed * Time.deltaTime;
			else
				currentCameraHeight = cameraHeightMIN;
		}

		if (CameraTransform != null) {
			CameraPosition.y = currentCameraHeight;
			CameraTransform.localPosition = CameraPosition;
			if (CameraLookAtTransform != null) {
				CameraTransform.LookAt(CameraLookAtTransform.position);
				LookAtPosition.y = Mathf.Lerp(LookAtPosition.y, currentCameraLookingHeight, 4 * Time.deltaTime);
				CameraLookAtTransform.localPosition = LookAtPosition;
			}
		}
	}

	void FixedUpdate() {
		myTransform.rotation = Quaternion.Euler(cameraRotationEulers);
	}
}
