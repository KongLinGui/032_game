using UnityEditor;
using System.Collections;
using InaneGames;
using UnityEngine;
using System.Collections.Generic;
[CustomEditor(typeof(Bridge))] 
[CanEditMultipleObjects]

public class BridgeEditor : Editor {
	
	
	public override void OnInspectorGUI() {
		Bridge myTarget = (Bridge) target;
		
		serializedObject.Update();
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("go1"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("go2"), true);
		if(GUILayout.Button("GetClosest"))
		{
			myTarget.getClosestParts();
			
		}
		if(GUILayout.Button("GenerateName"))
		{
			myTarget.updateName();
			
		}
		serializedObject.ApplyModifiedProperties();		
		
	}
	
	
}
