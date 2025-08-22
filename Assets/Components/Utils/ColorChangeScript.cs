using UnityEngine;
using System.Collections;

public class ColorChangeScript : MonoBehaviour {

	private Color startColor;
    private TextMesh textMesh;

	public float range = 0.5f;
	public float changeValue = 0.016f;

	private bool toDown = true;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh> ();
		startColor = textMesh.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (toDown) {
			textMesh.color = new Color(textMesh.color.r - changeValue, textMesh.color.g - changeValue,textMesh.color.b - changeValue);

			if (textMesh.color.r < startColor.r - range || textMesh.color.r <= 0) {
					toDown = false;
			}
		} else {
			textMesh.color = new Color(textMesh.color.r + changeValue, textMesh.color.g + changeValue,textMesh.color.b + changeValue);
			
			if (textMesh.color.r > startColor.r + range || textMesh.color.r >= 1) {
				toDown = true;
			}
		}


	}
}
