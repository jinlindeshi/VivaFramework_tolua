using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DrawLineMovie))]
public class DrawLineMovieEditor : Editor
{
    private DrawLineMovie hehe;
    void OnEnable()  
    {  
		hehe = (DrawLineMovie)target;
    }  

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("画线"))
        {
			hehe.Play();
        }

        if (GUILayout.Button("清理"))
        {
			hehe.ClearLine();
        }
    }
}
