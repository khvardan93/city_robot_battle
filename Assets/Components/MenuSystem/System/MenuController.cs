using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController<T> : MonoBehaviour {

	//properties

	//loading
	protected bool isLoadingScene = false;
	private string newScreen;

	//singletone
	private static T _instance;

	public static T instance {
		get {
			return _instance;
		}
	}

	public void Awake () {
		_instance = GetComponent<T> ();
	}

	protected void backButtonHandler(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			backButtonPressed ();
		}
	}

	protected virtual void backButtonPressed(){
		Debug.Log ("back button pressed");
	}

	/**
	 * system
	 * */
	public void invokeMethod(string method,float invokeTime){
		Invoke (method, invokeTime);
	}

	protected void loadScene(string screen){
		newScreen = screen;
		isLoadingScene = true;
	}

	protected void lateOpenScene(){
		SceneManager.LoadScene (newScreen); //GameManager.instance.getCurrentLevel().scene
	}
}
