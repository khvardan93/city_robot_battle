using UnityEngine;
using System.Collections;

/// <summary>
/// Mouse click controller. Controls firing of Rocket Turrets When Clicking on Objects and Terrain
/// </summary>
public class MouseClickController : MonoBehaviour {

	private DemoSceneGUI demoSceneGUI;

	public GameObject goTerrain;
	public GameObject TargetLocationPrefab;
	public Vector3 TargetLocationStartingPos = Vector3.zero;

	public RWSP_RocketTurret[] AvailableRocketTurrets;

	public Transform TargetLocationTransform;
	public Transform CurrentEnemyTargetTransform;

	// Use this for initialization
	void Start () {
		demoSceneGUI = GameObject.FindObjectOfType<DemoSceneGUI>();

		AvailableRocketTurrets = GameObject.FindObjectsOfType<RWSP_RocketTurret>();
		GameObject newTargetTrans = GameObject.Instantiate(TargetLocationPrefab, TargetLocationStartingPos, Quaternion.identity) as GameObject;
		newTargetTrans.name = "MouseTargetingPosition";
		TargetLocationTransform = newTargetTrans.transform;

		ResetTargetingObject();
	}

	// Update is called once per frame
	void Update () {
		if (CurrentEnemyTargetTransform != null) {
			TargetLocationTransform.position = CurrentEnemyTargetTransform.position;
		}

		if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit;
			Ray ray = demoSceneGUI.GetActiveCamera().ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.tag == "Target") {
					ClickedTarget(hit.collider.gameObject.transform);
				}
				else if (hit.collider.gameObject.tag == "Terrain") {
					ClickTerrain(hit.point);
				}
			}
		}
	}

	private void ResetTargetingToNull() {
		if (AvailableRocketTurrets.Length > 0) {
			for (int i = 0; i < AvailableRocketTurrets.Length; i++) {
				if (AvailableRocketTurrets[i].Target != null) {
					AvailableRocketTurrets[i].Target = null;
				}
			}
		}
	}

	private void ResetTargetingObject() {
		if (AvailableRocketTurrets.Length > 0) {
			for (int i = 0; i < AvailableRocketTurrets.Length; i++) {
				if (AvailableRocketTurrets[i].Target == null) {
					if (CurrentEnemyTargetTransform == null) {
						AvailableRocketTurrets[i].gameObject.SendMessage("SetTarget", TargetLocationTransform, SendMessageOptions.DontRequireReceiver);
					}
					else {
						AvailableRocketTurrets[i].gameObject.SendMessage("SetTarget", CurrentEnemyTargetTransform, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	private void ClickedTarget(Transform targetTrans) {
		Debug.Log("Mouse Clicked Target: " + targetTrans.gameObject.name);
		CurrentEnemyTargetTransform = targetTrans;

		ResetTargetingObject();

	}

	private void ClickTerrain(Vector3 hitPoint) {
		Debug.Log("Mouse Clicked Terrain at pos: " + hitPoint.ToString());
		// Update Mouse Targeting Position
		if (CurrentEnemyTargetTransform != null)
			CurrentEnemyTargetTransform = null;
		TargetLocationTransform.position = hitPoint;

		ResetTargetingObject();
	}
}
