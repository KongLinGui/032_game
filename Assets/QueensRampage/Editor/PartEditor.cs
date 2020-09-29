using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(ZPart2))] 
[CanEditMultipleObjects]

public class PartEditor : Editor {
	
	
	public override void OnInspectorGUI() {
		ZPart2 myTarget = (ZPart2) target;
		
		serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("killZone"), true);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("col"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("row"), true);
		if(GUILayout.Button("FindClosestPart"))
		{
			myTarget.getClosestParts();
			
		}
		serializedObject.ApplyModifiedProperties();		
		
	}
	
	
}
