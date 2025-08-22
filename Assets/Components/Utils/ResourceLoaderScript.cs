using UnityEngine;
using System.Collections;

public class ResourceLoaderScript : MonoBehaviour {

	public string resourceName;

	private GameObject instantiatedObject;

	void Start () {
		Invoke ("loadResource", 0.2f);
	}

	private void loadResource(){
		instantiatedObject = (GameObject) Instantiate (Resources.Load ("Other/" + resourceName));
	}

	void OnDestroy() {
		Destroy (instantiatedObject);
	}
}
