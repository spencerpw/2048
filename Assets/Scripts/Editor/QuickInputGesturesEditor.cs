using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(QuickInputGestures))]
public class QuickInputGesturesEditor : Editor {

	public override void OnInspectorGUI()
	{
		QuickInputGestures myTarget = (QuickInputGestures)target;

		if (myTarget.events == null) myTarget.events = new UnityEvent[5];

		myTarget.threshold = EditorGUILayout.FloatField("Threshold", myTarget.threshold);

		for(int i = 0; i < 5; i++) {
			EditorGUILayout.LabelField(((QuickInputGestures.Direction)i).ToString());
			SerializedProperty sProp = serializedObject.FindProperty("events").GetArrayElementAtIndex(i);
			EditorGUIUtility.LookLikeControls();
			EditorGUILayout.PropertyField(sProp,true);
		}


		serializedObject.ApplyModifiedProperties();
	}
}
