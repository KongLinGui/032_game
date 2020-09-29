using UnityEditor;
using System.Collections;
using InaneGames;
using UnityEngine;
using System.Collections.Generic;


[CustomEditor(typeof(ZBoard))] 
public class GenerateBoard : Editor {
	

	public override void OnInspectorGUI() {
		ZBoard myTarget = (ZBoard) target;

		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("nomCells"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("nomRows"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("cellOffset"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("easeType"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("moveTime"), true);
		if(GUILayout.Button("GenerateBoard"))
		{
			myTarget.generateBoard();

		}
		serializedObject.ApplyModifiedProperties();		

	}
	

}
