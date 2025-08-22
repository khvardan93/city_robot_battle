using UnityEngine;
using UnityEngine.UI;

public class PlayerUIMessageScript : MonoBehaviour {

	private Text text;

	void Awake () {
		this.text = GetComponent<Text> ();
		this.gameObject.SetActive (false);
	}

	public void show(string message){
		this.gameObject.SetActive (true);
        this.text.color = new Color(0.65f, 0.5f, 0.05f);
		this.text.text = message;

		Invoke ("hide", 2f);
	}

	public void show(string message, float showingTime){
		this.gameObject.SetActive (true);
        this.text.color = new Color(0.65f, 0.5f, 0.05f);
		this.text.text = message;

		Invoke ("hide", showingTime);
	}

	public void show(string message, Color color){
		this.gameObject.SetActive (true);
		this.text.text = message;
		this.text.color = color;

		Invoke ("hide", 2f);
	}

	public void hide(){
		this.gameObject.SetActive (false);
	}
}
