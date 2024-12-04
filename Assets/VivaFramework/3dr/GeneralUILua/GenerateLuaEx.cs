#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MsgData
{
    public string path;
    public Transform ts;
}
[Serializable]
public class CatchNode
{
    public int deep;
    [NonSerialized]
    public Rect rect;
    
    public CatchNode parent;
    public List<CatchNode> childList;
    [NonSerialized]
    public Transform transform;
    public List<string> componentSelect;

    [Header("Next version will delete")]
    public string saveName;
    public bool assistantParent;//嵌套父节点
    public long localID;

    public static CatchNode Copy(Transform rootTs, CatchNode rootNode, Dictionary<long, MsgData> tsDict, CatchNode parentNode = null)
    {
        CatchNode newNode = new CatchNode();
        newNode.deep = rootNode.deep;
        newNode.rect = rootNode.rect;
        newNode.localID = rootNode.localID;
        //if (string.IsNullOrEmpty(rootNode.saveName) || rootNode.saveName.Equals("Root"))
        if(parentNode == null)
        {
            newNode.saveName = "Root";
            newNode.transform = rootTs;
        }
        else
        {
            if (tsDict.TryGetValue(newNode.localID, out MsgData data))
            {
                newNode.transform = data.ts;
                newNode.saveName = data.path;
            }
            else
            {
                return null;
            }
        }
        newNode.parent = parentNode;
        if (rootNode.componentSelect != null)
        {
            newNode.componentSelect = new List<string>(rootNode.componentSelect.ToArray());
        }
        //newNode.saveName = rootNode.saveName;
        newNode.assistantParent = rootNode.assistantParent;
        newNode.childList = new List<CatchNode>();
        if (rootNode.childList != null)
        {
            for (int i = 0; i < rootNode.childList.Count; i++)
            {
                CatchNode cNode = Copy(rootTs, rootNode.childList[i], tsDict, newNode);
                if (cNode == null)
                    continue;
                newNode.childList.Add(cNode);
            }
        }
        return newNode;
    }
}

public class GenerateLuaEx : MonoBehaviour
{
    [NonSerialized]
    public CatchNode rootNode;
    [NonSerialized]
    public Dictionary<string, bool> componentDict;
    [NonSerialized]
    public Dictionary<string, bool> assistantParentDict;
    public string savePath;
    public string strRootNode;

    public void ResetAll()
    {
        rootNode = null;
        componentDict.Clear();
        assistantParentDict.Clear();
        savePath = "";
        strRootNode = "";
    }

    public void GetData()
    {
        if (!string.IsNullOrEmpty(strRootNode))
        {
            rootNode = JsonUtility.FromJson(strRootNode, typeof(CatchNode)) as CatchNode;

            componentDict = new Dictionary<string, bool>();
            assistantParentDict = new Dictionary<string, bool>();

            GetComponentDictAndAssistant(rootNode);
        }
    }

    private void GetComponentDictAndAssistant(CatchNode node)
    {
        assistantParentDict[node.saveName] = false;

        if (node.componentSelect != null)
        {
            foreach (var v in node.componentSelect)
            {
                componentDict[node.saveName + "-" + v] = true;
            }
        }

        if (node.childList != null)
        {
            foreach (var v in node.childList)
            {
                GetComponentDictAndAssistant(v);
            }
        }
    }
}
#endif



