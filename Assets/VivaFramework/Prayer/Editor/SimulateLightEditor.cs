

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimulateLight))]
public class SimulateLightEditor:Editor
{
    private SimulateLight testA;

    private SerializedObject test;
    private SerializedProperty m_type;//定义类型
    void OnEnable()  
    {  
        testA = (SimulateLight)target;
        testA.Refresh();

        test = new SerializedObject(testA);
        m_type = test.FindProperty("lightType");//获取m_type
    }

//    private void OnSceneGUI()
//    {
//        testA.Refresh();
//    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        testA.Refresh();
        test.Update();
 
        if (m_type.enumValueIndex == 0) {//当选择第一个枚举类型
//            EditorGUILayout.PropertyField(a_int);
        }
        else if (m_type.enumValueIndex == 1) {
//            EditorGUILayout.PropertyField(b_int);
        }
        else if (m_type.enumValueIndex == 2) {
//            EditorGUILayout.PropertyField(b_int);
        }
        test.ApplyModifiedProperties();//应用

    }
}