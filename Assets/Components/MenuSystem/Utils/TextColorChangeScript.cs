using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(TextMesh))]
public class TextColorChangeScript : MonoBehaviour {
	[SerializeField]
	private float changeSpeed = 1f;
	[SerializeField]
	private Color textColor;

	private TextMesh textMesh;

	private string hintText;

	void OnEnable(){
		if (this.textMesh == null) {
			this.textMesh = this.GetComponent<TextMesh> ();
		}
		this.textMesh.text = this.hintText;
		this.textMesh.color = this.textColor;
	}

	void Update () {
		this.textColor.a -= changeSpeed * Time.deltaTime;
		this.textMesh.color = this.textColor;
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