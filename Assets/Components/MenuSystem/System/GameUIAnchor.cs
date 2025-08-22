using UnityEngine;
using System.Collections;

public class GameUIAnchor : MonoBehaviour {

	public bool right;
	public bool left;

	public bool refresh;

	private float ratio = 1f;

	void Start () {
		if (this.refresh) {
			this.doRefresh ();
			InvokeRepeating ("doRefresh", 1, 1f);		
		} else {
			this.doRefresh ();
		}
	}

	private void doRefresh(){
		ratio = (float) Screen.width / Screen.height;

		if (right) {
			transform.position = new Vector3(ratio / 2, transform.position.y, transform.position.z);
		}

		if (left) {
			transform.position = new Vector3(-ratio / 2, transform.position.y, transform.position.z);
		}
	}
}
