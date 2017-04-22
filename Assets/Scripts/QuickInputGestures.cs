using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

///<summary>
/// An easy way to hookup key presses to UnityEvents.
/// Used a custom inspector to use in editor.
/// Would look and function better if I had more time.
///</summary>

public class QuickInputGestures : MonoBehaviour {
	public enum Direction {
		Up,
		Down,
		Right,
		Left
	}
	public UnityEvent[] events;
	public float threshold;

	private Vector2 touchStart;

	private void Start() {
		Messenger.AddListener("DisableInput",DisableInput);
		Messenger.AddListener("EnableInput",EnableInput);

	}

	private void DisableInput() {
		enabled = false;
	}

	private void EnableInput() {
		enabled = true;
	}

	private void Update() {
		if(Input.touchCount > 0) {
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
				touchStart = Input.GetTouch(0).position;
			} else if(Input.GetTouch(0).phase == TouchPhase.Ended) {
				Vector2 touchEnd = Input.GetTouch(0).position;

				if ( Mathf.Abs(touchStart.y - touchEnd.y) > threshold) {
					if(touchStart.y < touchEnd.y)
						events[(int)Direction.Up].Invoke();
					else
						events[(int)Direction.Down].Invoke();
				} else if ( Mathf.Abs(touchStart.x - touchEnd.x) > threshold) {
					if(touchStart.x < touchEnd.x)
						events[(int)Direction.Right].Invoke();
					else
						events[(int)Direction.Left].Invoke();
				}
			}
		}
	}
}
