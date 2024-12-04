using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HappyCamera))]
public class HappyCameraEditor : Editor
{
	private SerializedObject obj;  
	private HappyCamera testA;

	void OnEnable()  
	{  
		obj = new SerializedObject(target);  
		testA = (HappyCamera)target;  
		testA.EditorInit ();
	}

	public override void OnInspectorGUI()  
	{  
		obj.Update();

		EditorGUILayout.PropertyField (obj.FindProperty("attachObj")); 
		EditorGUILayout.PropertyField (obj.FindProperty("cameraObj")); 

		SerializedProperty lookAtP = obj.FindProperty ("followParams.lookAt");
		EditorGUILayout.PropertyField (obj.FindProperty("followParams.offsetV")); 
		EditorGUILayout.PropertyField (lookAtP); 
        EditorGUILayout.PropertyField(obj.FindProperty("followParams.globalReference"));
        EditorGUILayout.PropertyField(obj.FindProperty("followParams.checkCover"));
		EditorGUILayout.PropertyField(obj.FindProperty("followParams.attachOffset")); 
		EditorGUILayout.PropertyField(obj.FindProperty("tweenResumeSpeed")); 
		EditorGUILayout.PropertyField(obj.FindProperty("vRotateAngle")); 
        EditorGUILayout.PropertyField(obj.FindProperty("hRotateAngle")); 
		EditorGUILayout.PropertyField(obj.FindProperty("lockY")); 

		if(GUILayout.Button("预览(虽然还是运行着调最爽)"))
		{
			testA.FixTransform ();
		}

		obj.ApplyModifiedProperties();

		//testA.FixTransform ();
//		base.OnInspectorGUI();
	} 
}

