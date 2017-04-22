using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

///<summary>
/// An easy way to hookup key presses to UnityEvents.
/// Used a custom inspector to use in editor.
/// Would look and function better if I had more time.
///</summary>

public class QuickInputKeyUp : MonoBehaviour {
	public List<KeyCode> keyCodes;
	public UnityEvent[] events;

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
		for(int i = 0; i < keyCodes.Count; i++) {
			if(Input.GetKeyUp(keyCodes[i])) {
				events[i].Invoke();
			}
		}
	}
}
