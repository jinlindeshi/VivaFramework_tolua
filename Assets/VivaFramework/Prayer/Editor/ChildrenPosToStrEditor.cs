using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChildrenPosToStr))]
public class ChildrenPosToStrEditor : Editor
{
    private SerializedObject obj;  
    private ChildrenPosToStr testA;
    
    void OnEnable()  
    {  
        obj = new SerializedObject(target);  
        testA = (ChildrenPosToStr)target;  
    }

    public override void OnInspectorGUI()
    {
        obj.Update();
        
        EditorGUILayout.PropertyField (obj.FindProperty("locStr")); 
        if(GUILayout.Button("输出坐标"))
        {
            testA.TraceLoc();
        }
    }
}