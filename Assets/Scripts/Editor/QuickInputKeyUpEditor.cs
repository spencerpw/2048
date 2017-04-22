using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(QuickInputKeyUp))]
public class QuickInputKeysEditor : Editor {

	public override void OnInspectorGUI()
	{
		QuickInputKeyUp myTarget = (QuickInputKeyUp)target;
		List<UnityEvent> myEvents;

		if (myTarget.keyCodes == null) myTarget.keyCodes = new List<KeyCode>();
		if (myTarget.events == null) myTarget.events = new UnityEvent[1];

		myEvents = myTarget.events.ToList();

		for(int i = 0; i < myTarget.keyCodes.Count; i++) {
			myTarget.keyCodes[i] = (KeyCode)EditorGUILayout.EnumPopup("KeyCode",myTarget.keyCodes[i]);

			if(myTarget.keyCodes[i] != KeyCode.None) {
				SerializedProperty sProp = serializedObject.FindProperty("events").GetArrayElementAtIndex(i);
				EditorGUIUtility.LookLikeControls();
				EditorGUILayout.PropertyField(sProp,true);

			} else {
				myTarget.keyCodes.RemoveAt(i);
				myEvents.RemoveAt(i);
			}
		}

		if(GUILayout.Button("Add")) {
			myTarget.keyCodes.Add(KeyCode.A);
			myEvents.Add(new UnityEvent());
			myTarget.events = myEvents.ToArray();
		}

		serializedObject.ApplyModifiedProperties();
	}
}
