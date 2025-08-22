using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RobotBattle.UI.GameMenu
{
	public class YokeJoystickScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
	{
		[SerializeField] RectTransform stick;
		[SerializeField] RectTransform joystickBackground;

		private Vector2 startPosition;
		private bool isActive;

		private void Awake()
		{
			SetJoystick(false);
			InputUtil.MoveChangeVector = Vector2.zero;
		}

		private void SetJoystick(bool state)
		{
			isActive = state;
			joystickBackground.gameObject.SetActive(state);
			stick.gameObject.SetActive(state);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SetJoystick(true);
			
			startPosition = eventData.position;

			joystickBackground.position = startPosition;
			stick.position = startPosition;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			SetJoystick(false);
			InputUtil.MoveChangeVector = Vector2.zero;
		}

		public void OnPointerMove(PointerEventData eventData)
		{
			if (!isActive) return;
			stick.position = eventData.position;

			InputUtil.MoveChangeVector = (eventData.position - startPosition).normalized;
		}
	}
}