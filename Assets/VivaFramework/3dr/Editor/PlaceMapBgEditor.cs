using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlaceMapBg))]
public class PlaceMapBgEditor : Editor
{
    private SerializedObject obj;  
    private PlaceMapBg testA;
    
    void OnEnable()  
    {  
        obj = new SerializedObject(target);  
        testA = (PlaceMapBg)target;  
    }
    public override void OnInspectorGUI()
    {
        
        if(GUILayout.Button("Place"))
        {
            testA.TakePlace();
        }
        base.OnInspectorGUI();
    }
}