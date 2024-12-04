using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTools 
{
    static string TagName = "EditorOnly";

    [MenuItem("GameObject/GenerateUIEditorNode", false, -10)]
    static void AddUIEditorNode()
    {
        GameObject parentGO = Selection.activeGameObject;
        if (parentGO)
        {
            GameObject go = new GameObject("UIEditorNode");
            go.tag = TagName;
            go.transform.SetParent(parentGO.transform);
            go.AddComponent<GenerateLuaEx>();
        }
    }
}
