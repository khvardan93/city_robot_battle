using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTouchScript : MonoBehaviour
{
	protected bool isPressed;
	protected Vector2 cachedPosition;
	protected Vector2 pressedPosition;
	protected RectTransform thisTransform;
	[SerializeField] RectTransform panel;
	protected Vector3[] corners;

	protected float screenDiameter;

	protected void Start()
	{
		thisTransform = this.GetComponent<RectTransform>();
		
		getCornerPositions();
		screenDiameter = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
	}

	protected virtual void Update()
	{
		if (this.isPressed)
		{
			Vector2 currentPressPosition = this.getPressedPosition();
			if (Vector2.Distance(this.cachedPosition, currentPressPosition) < screenDiameter * 0.35f)
			{
				this.pressedPosition = currentPressPosition;
			}
		}
	}

	protected Vector2 getPressedPosition()
	{
		Vector2 pressedPosition = Vector2.zero;

        #if UNITY_EDITOR
		pressedPosition = Input.mousePosition;
		#else
		if (Input.touchCount > 0) {
			float minDistance = 10000000000f; //bigger then any touch distance can be

			foreach(var touch in Input.touches){
				float distance = Vector2.Distance(touch.position, (Vector2)thisTransform.position);

				if(distance < minDistance){
					pressedPosition = touch.position;
					minDistance = distance;
				}
			}
		}
        #endif

		return checkInputPosition(pressedPosition) ? pressedPosition : Vector2.zero;
	}

	protected void getCornerPositions()
	{
		corners = new Vector3[4];
		panel.GetWorldCorners(corners);
	}

    protected bool checkInputPosition(Vector2 position)
    {
		return corners[0].x < position.x && corners[3].x > position.x && corners[0].y < position.y && corners[1].y > position.y;
	}

	public virtual void setPressed()
	{
		this.isPressed = true;
		this.cachedPosition = this.pressedPosition = this.getPressedPosition();
	}

	public virtual void setUnPressed()
	{
		this.isPressed = false;
	}
}