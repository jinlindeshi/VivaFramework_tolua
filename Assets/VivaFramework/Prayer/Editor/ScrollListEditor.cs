

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScrollList))]
public class ScrollListEditor : Editor
{
    
    private SerializedObject obj;
    private ScrollList testA;

    private float _testScrollValue = 0;
    private bool isPreview = false;
    void OnEnable()  
    {  
        obj = new SerializedObject(target);  
        testA = (ScrollList)target; 
        testA.Ctor();
    }


    private uint _lastHNum = 0;
    private uint _lastVNum = 0;
    private GameObject _lastItemPrefab;
    public override void OnInspectorGUI()
    {
        obj.Update();
        EditorGUILayout.PropertyField (obj.FindProperty("HNum")); 
        EditorGUILayout.PropertyField (obj.FindProperty("VNum")); 
        EditorGUILayout.PropertyField (obj.FindProperty("itemPrefab"));

        bool valueChanged = false;
        if (_lastHNum != testA.HNum)
        {
            _lastHNum = testA.HNum;
            valueChanged = true;
        }
        if (_lastVNum != testA.VNum)
        {
            _lastVNum = testA.VNum;
            valueChanged = true;
        }
        if (_lastItemPrefab != testA.itemPrefab)
        {
            _lastItemPrefab = testA.itemPrefab;
            testA.FixItemSize();
            valueChanged = true;
        }
        
        if (Application.isPlaying == false)
        {
            if(GUILayout.Button("测试数据"))
            {
                isPreview = !isPreview;

                if (isPreview == true)
                {
                    testA.SetShow(10);
                    valueChanged = false;
                }
                else
                {
                    testA.Clear();
                }
            }

            if (isPreview == true)
            {
                if (valueChanged == true)
                {
                    testA.SetShow(10);
                }
                _testScrollValue = EditorGUILayout.Slider("TestScroll", _testScrollValue, 0, 1); 
                testA.SetScrollRate(_testScrollValue);
            }
            else
            {
            }
        }
        obj.ApplyModifiedProperties();
    }
}