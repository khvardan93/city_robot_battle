using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UITextColorChangeScript : MonoBehaviour {
	[SerializeField]
	private float changeSpeed = 1f;
	[SerializeField]
	private Color textColor;

	private Text uiText;

	private string hintText;

	void OnEnable(){
		if (this.uiText == null) {
			this.uiText = this.GetComponent<Text> ();
		}
		this.uiText.text = this.hintText;
		this.uiText.color = this.textColor;
	}

	void Update () {
		this.textColor.a -= changeSpeed * Time.deltaTime;
		this.uiText.color = this.textColor;
		if (this.textColor.a <= 0) {
			this.gameObject.SetActive (false);
		}
	}

	public void setHint(string message){
		this.hintText = message;
		this.textColor.a = 1f;
		this.gameObject.SetActive (true);
	}

	public void setHint(string message, Color textColor){
		this.textColor = textColor;
		this.setHint (message);
	} 
}