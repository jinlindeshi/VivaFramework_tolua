using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIExtends : MonoBehaviour
{
//    [MenuItem("GameObject/UI/Image")]
//    public static void CreateImage()
//    {
//        var transform = Selection.activeTransform;
//        GameObject go = new GameObject("Image", typeof(UnityEngine.UI.Image));
//        go.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
//        go.transform.SetParent(transform);
//        go.transform.localScale = Vector3.one;
//        go.transform.localPosition = Vector3.zero;
//    }
//    
//    [MenuItem("GameObject/UI/Text")]
//    public static void CreateText()
//    {
//        var transform = Selection.activeTransform;
//        GameObject go = new GameObject("Text", typeof(UnityEngine.UI.Text));
//        go.GetComponent<UnityEngine.UI.Text>().raycastTarget = false;
//        go.transform.SetParent(transform);
//        go.transform.localScale = Vector3.one;
//        go.transform.localPosition = Vector3.zero;
//    }


    [MenuItem("GameObject/HappyUI/ScrollList", priority = 0)]
    static void AddScrollList()
    {
        GameObject obj = new GameObject();
        obj.transform.parent = Selection.activeTransform;
        obj.transform.localPosition = Vector3.zero;
        obj.name = "ScrollList";
        obj.AddComponent<ScrollList>();
    }
    
}