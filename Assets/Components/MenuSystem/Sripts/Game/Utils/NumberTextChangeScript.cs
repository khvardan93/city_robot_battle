using UnityEngine;
using UnityEngine.UI;

public class NumberTextChangeScript : MonoBehaviour {
	private float currentValue;
	private float value;
	private Text text;

	public float changeSpeed = 0.1f;
	public float stepSize = 1f; 
	public string format = "#00.0";
	public float startValue = 0;

	public string unitText = "";

	void Awake (){
		this.text = this.GetComponent<Text> ();
		this.text.text = 0.ToString (this.format);
	}

	void OnEnable(){
		this.addText ();
	}

	public void set(float newValue){	
		if (this.currentValue == newValue) {
			return;
		}

		if (this.startValue > 0 && this.startValue < newValue && Mathf.Abs(this.currentValue - newValue) > this.startValue) {
			this.currentValue = newValue - this.startValue;
			this.addText ();
		} else if(this.startValue > newValue && this.currentValue > this.startValue){
			this.currentValue = 0;
		}

		CancelInvoke ();
		this.value = newValue;
		if (this.currentValue > this.value) {
			InvokeRepeating ("decrement", 0, this.changeSpeed);
		} else if (this.currentValue < this.value) {
			InvokeRepeating ("increment", 0, this.changeSpeed);
		}
	}


	public void set(int newValue){
		this.set ((float)newValue);
	}

	private void increment(){
		this.currentValue += this.stepSize;
		if(this.currentValue >= this.value){
			this.currentValue = this.value;
			CancelInvoke ();
		}
		this.addText ();
	}

	private void decrement(){
		this.currentValue -= this.stepSize;
		if(this.currentValue <= this.value){
			this.currentValue = this.value;
			CancelInvoke ();
		}
		this.addText ();
	}

	private void addText(){
		if (this.text == null) {
			this.text = this.GetComponent<Text> ();
		}
		this.text.text = this.currentValue.ToString (this.format) + unitText;
	}

	public void addText(float newValue){
		if (this.text == null) {
			this.text = this.GetComponent<Text> ();
		}
		this.text.text = newValue.ToString (this.format) + unitText;
	}
}