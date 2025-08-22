using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	private bool isDownTimer = false;

	private Text textMesh;

	private Color defaultColor;
	public Color warningColor;
	public Color problemColor;

	private int calculatedStarCount = 3;

	/*void Start () {
		if (GameManager.instance.timerTime > 0) {
			this.isDownTimer = true;
		}

		this.textMesh = GetComponent<Text> ();
		this.defaultColor = this.textMesh.color;

		if (GameManager.instance.getCurrentLevel().gameType == Level.GameType.Battle || GameManager.instance.getCurrentLevel().gameType == Level.GameType.Free) {
			InvokeRepeating ("updateCalculateLevelStars", 0, 0.7f);
		}
	}

	void Update () {
		if (GameManager.instance.isGameFinished) {
			return;
		}

		if (GameManager.instance.isTimeTrackerNeeded ()) {
			this.textMesh.text = TimerUtil.timeToString (GameManager.instance.timerTime);
		} else {
			this.textMesh.text = "";
		}
	}

	void FixedUpdate(){

		if (GameManager.instance.isGameFinished) {
			return;
		}
		if (this.isDownTimer) {
			GameManager.instance.timerTime -= Time.fixedDeltaTime;
		} else {
			GameManager.instance.timerTime += Time.fixedDeltaTime;
		}

		updateColor ();

		if (GameManager.instance.timerTime < 0) {
			//down timer - game finished
			GameManager.instance.timerTime = 0;
			GameManager.instance.timerFinished ();
			return;
		}

		if (this.calculatedStarCount < 1) {
			//up timer - game finished
			GameManager.instance.timerFinished ();
		}
	}*/

	/// <summary>
	/// for caching level star calculation - keeps value in this.calculatedStarCount
	/// </summary>
	private void updateCalculateLevelStars(){
//		this.calculatedStarCount = GameManager.gameTypeHandler.calculateLevelStars ();
	}

	/// <summary>
	/// Updates the color based on level calculated stars
	/// </summary>
	private void updateColor () {
		/*if (this.isDownTimer) {
			if (GameManager.instance.timerTime < 5) {
				textMesh.color = this.problemColor;
			} else if (GameManager.instance.timerTime < 10) {
				this.textMesh.color = this.warningColor;
			}
		} else {
			if (this.calculatedStarCount == 3) {
				//Debug.Log ("normal color");
				this.textMesh.color = this.defaultColor;
			} else if (this.calculatedStarCount == 2) {
				//Debug.Log ("middle color");
				this.textMesh.color = this.warningColor;
			} else if (this.calculatedStarCount == 1) {
				//Debug.Log ("problem color");
				textMesh.color = this.problemColor;
			} else {
				textMesh.color = this.problemColor;
			}
		}*/
	}
}
