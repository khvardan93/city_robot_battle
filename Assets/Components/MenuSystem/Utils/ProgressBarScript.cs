using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProgressBarScript : MonoBehaviour
{
	/// <summary>
	/// The auto animation.
	/// </summary>
	public bool autoAnimation = false;
	public float autoAnimationSpeed = 0.3f;
	/// <summary>
	/// The is line progress.
	/// </summary>
	public bool isLineProgress = false;
	public bool reverseProgress = false;

	/// <summary>
	/// The value of progressbar for testing inside editor
	/// </summary>
	//public float value;

	/// <summary>
	/// Progress bar with icons
	/// </summary>
	public string ProgressIconName = "ProgressIcon";
	private List<Transform> children = new List<Transform>();

	/// <summary>
	/// Line progress Bar
	/// </summary>
	//public float length;
	private Slider slider;

	private float currentFillPercent = 0;

	void Awake()
	{
		if (this.isLineProgress)
		{
			this.slider = this.GetComponent<Slider>();
			this.slider.direction = this.reverseProgress ? Slider.Direction.RightToLeft : Slider.Direction.LeftToRight;
			this.slider.maxValue = 1;
		}
		else
		{
			foreach (Transform chid in this.transform.GetComponentsInChildren<Transform>(true))
			{
				if (chid.name == this.ProgressIconName)
				{
					this.children.Add(chid);
				}
			}
		}
	}

	void OnEnable()
	{
		if (this.autoAnimation)
		{
			setProgressStatus(0);
			InvokeRepeating("fillAnimate", 0, this.autoAnimationSpeed);
		}
	}

	/*void Update () {
		setProgressStatus (this.value);
	}*/

	/// <summary>
	/// Sets the progress status by percent [0, 100].
	/// </summary>
	/// <param name="percent">Percent.</param>
	public void setProgressStatus(float percent)
	{
		percent = Mathf.Clamp(percent, 0, 100f);

		if (this.isLineProgress)
		{
			this.slider.value = percent / 100f;
		}
		else
		{
			float activeCount = (this.children.Count / 100f) * percent;

			if (this.reverseProgress)
			{
				for (int i = this.children.Count - 1; i >= 0; i--)
				{
					this.children[i].gameObject.SetActive(i > activeCount);
				}
			}
			else
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					this.children[i].gameObject.SetActive(i < activeCount);
				}
			}
		}
	}

	/// <summary>
	/// Sets the progress status current and max values.
	/// </summary>
	/// <param name="currentValue">Current value.</param>
	/// <param name="maxValue">Max value.</param>
	public void setProgressStatus(float currentValue, float maxValue)
	{
		this.setProgressStatus(currentValue * 100f / maxValue);
	}

	/// <summary>
	/// Fills the animate.
	/// </summary>
	private void fillAnimate()
	{
		if (this.currentFillPercent < 100f)
		{
			this.currentFillPercent += 5f;
			setProgressStatus(this.currentFillPercent);
		}
		else
		{
			this.currentFillPercent = 0;
			setProgressStatus(0);
		}
	}
}
