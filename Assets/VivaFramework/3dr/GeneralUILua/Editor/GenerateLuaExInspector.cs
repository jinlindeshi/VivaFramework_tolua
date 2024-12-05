
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using System.Reflection;
using System;
// using ICSharpCode.NRefactory.Ast;


[CustomEditor(typeof(GenerateLuaEx))]
public class GenerateLuaExInspector : Editor
{
    GenerateLuaEx m_Target;
    public override void OnInspectorGUI()
    {
        const string VERSION = "1.0.0";
        m_Target = target as GenerateLuaEx;

        //base.OnInspectorGUI();

        //serializedObject.Update();

        if (GUILayout.Button("OpenEditorWnd"))
        {
            EditorWindow.GetWindow<CatchComponentWnd>("CatchWnd  "+ VERSION, false);
        }
        if (GUILayout.Button("UpdateToVersion-"+VERSION))
        {
            m_Target.GetData();
            Dictionary<long, MsgData> tsDict = new Dictionary<long, MsgData>();
            SetPathID(ref tsDict, m_Target.transform.parent);
            CatchNode nodeRoot = Copy(m_Target.transform.parent, m_Target.rootNode, tsDict);


            UpdateFunc(nodeRoot, nodeRoot);
            m_Target.strRootNode = JsonUtility.ToJson(nodeRoot);

            var prefabStage = PrefabStageUtility.GetPrefabStage(m_Target.transform.gameObject);
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }

            var so = new SerializedObject(m_Target);
            so.ApplyModifiedProperties();
        }
    }

    private CatchNode Copy(Transform rootTs, CatchNode rootNode, Dictionary<long, MsgData> tsDict, CatchNode parentNode = null)
    {
        CatchNode newNode = new CatchNode();
        newNode.deep = rootNode.deep;
        newNode.rect = rootNode.rect;
        newNode.localID = rootNode.localID;
        //if (string.IsNullOrEmpty(rootNode.saveName) || rootNode.saveName.Equals("Root"))
        if (parentNode == null)
        {
            newNode.saveName = "Root";
            //newNode.transform = rootTs;
        }
        else
        {
            bool needReturn = true;
            foreach (var v in tsDict)
            {
                newNode.transform = v.Value.ts;
                needReturn = false;
            }
            if (needReturn)
            {
                Debug.LogError(rootNode.saveName + " not find");
                return null;
            }
        }
        newNode.parent = parentNode;
        if (rootNode.componentSelect != null)
        {
            newNode.componentSelect = new List<string>(rootNode.componentSelect.ToArray());
        }
        newNode.saveName = rootNode.saveName;
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

    private void UpdateFunc(CatchNode cNode, CatchNode rootNode)
    {
        if (cNode != null)
        {
            Transform selectTs = null;
            if ((cNode == rootNode))
            {
                selectTs = m_Target.transform.parent;
            }
            else
            {
                selectTs = m_Target.transform.parent.Find(cNode.saveName);
            }
            if (selectTs)
            {
                PropertyInfo info = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
                SerializedObject so = new SerializedObject(selectTs);
                info.SetValue(so, InspectorMode.Debug, null);
                SerializedProperty localIdProp = so.FindProperty("m_LocalIdentfierInFile");
                cNode.localID = localIdProp.intValue;
            }
            else
            {
                cNode.localID = 0;
            }

            if (cNode.childList != null)
            {
                foreach (var v in cNode.childList)
                {
                    UpdateFunc(v, rootNode);
                }
            }
        }
    }

    private void SetPathID(ref Dictionary<long, MsgData> dict, Transform ts, string path = "")
    {
        string mPath = path;
        long id = GetLocalID(ts);
        if (string.IsNullOrEmpty(path))
        {
            mPath = "Root";
        }
        else if (mPath.Equals("Root"))
        {
            mPath = ts.name;
        }
        else
        {
            mPath += "/" + ts.name;
        }
        MsgData mData = new MsgData();
        mData.ts = ts;
        mData.path = mPath;
        dict[id] = mData;
        for (int i = 0; i < ts.childCount; i++)
        {
            SetPathID(ref dict, ts.GetChild(i), mPath);
        }
    }

    private long GetLocalID(Transform ts)
    {
        PropertyInfo info = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
        SerializedObject so = new SerializedObject(ts);
        info.SetValue(so, InspectorMode.Debug, null);
        SerializedProperty localIdProp = so.FindProperty("m_LocalIdentfierInFile");
        return localIdProp.intValue;
    }

}

public class CatchComponentWnd : EditorWindow
{
    private GenerateLuaEx gLua;
    private CatchNode nodeRoot;
    private Dictionary<string, bool> assistantParentDictClone;
    private Dictionary<string, bool> componentDictClone;
    private Dictionary<string, bool> isOpen;//是否折叠
    private readonly bool defaultOpenState = false; //默认折叠是否打开

    private Vector2 scrollViewPos;
    private int openBtnIndex;
    private readonly string[] openName = new string[2] { "Open ", "Close" };
    private Color defaultColor;
    private Color assistantParentColor = Color.cyan;
    private Color diffAPColor = Color.yellow;
    private readonly Color diffColor = Color.red;
    private readonly Color foldColor = Color.green;
    private readonly Color componentColor = Color.green;
    private readonly string componentForm = "*{0}*";
    private readonly string parentNodeForm = "[{0}] is ParentNode";

    private List<System.Action> tempActionList;

    //------------------------OutputParams---------------------------
    private const string OutputDefaultPath = "Assets/Lua/Module/";
    private const string PrefabDefaultPath = "Assets/Res/Prefabs/";
    private string guiOutputContent;

    //-----------------------CalculateTime----------------------------
    private float lastResetDataClickTime;
    private float lastResetAllClickTime;
    private const float resetVaildInterval = 0.2f;

    //-----------------------CurEventPos------------------------------
    private EventType curEventType;
    private Vector2 curMousePos;

    private bool initOK;
    private void ResetAll()
    {
        initOK = false;
        gLua.ResetAll();
        ResetSetting();
        ShowNotification(new GUIContent("Reset All"));
    }

    private void RevertToSaveData()
    {
        initOK = false;
        ResetSetting();
        ShowNotification(new GUIContent("Already Revert To Defaults"));
    }

    private void ResetSetting()
    {
        scrollViewPos = Vector2.zero;
        openBtnIndex = 0;

        if (!string.IsNullOrEmpty(gLua.savePath))
        {
            guiOutputContent = gLua.savePath;
        }
        else
        {
            guiOutputContent = OutputDefaultPath;
        }

        if (assistantParentDictClone == null)
            assistantParentDictClone = new Dictionary<string, bool>();
        else
            assistantParentDictClone.Clear();

        if (componentDictClone == null)
            componentDictClone = new Dictionary<string, bool>();
        else
            componentDictClone.Clear();

        if (isOpen == null)
            isOpen = new Dictionary<string, bool>();
        else
            isOpen.Clear();

        if (tempActionList == null)
            tempActionList = new List<System.Action>();
        else
            tempActionList.Clear();
    }

    private void OnGUI()
    {
        curEventType = Event.current.type;
        defaultColor = GUI.color;

        GameObject o = Selection.activeGameObject as GameObject;
        if (o)
            gLua = o.transform.root.GetComponentInChildren<GenerateLuaEx>();
        
        if (gLua == null)
        {
            initOK = false;
            GUI.color = foldColor;
            EditorGUILayout.LabelField("Please Select Correct GameObject");
            GUI.color = defaultColor;
            return;
        }
        else
        {
            if (!initOK || isOpen == null)
            {
                ResetSetting();
                gLua.GetData();
                GetNode();
                initOK = true;
            }
        }

        TopTools();

        scrollViewPos = EditorGUILayout.BeginScrollView(scrollViewPos);

        GUI.color = foldColor;
        EditorGUILayout.LabelField("Root");
        curMousePos = Event.current.mousePosition;
        DragEventListener(nodeRoot);

        GUI.color = defaultColor;
        LoopShowGUI(nodeRoot);

        EditorGUILayout.EndScrollView();
        ExcuteAddCallback();
    }

    private void ExcuteAddCallback()
    {
        foreach (var v in tempActionList)
        {
            v();
        }
        tempActionList.Clear();
    }

    private void DragEventListener(CatchNode parentNode)
    {
        Rect rect = GUILayoutUtility.GetLastRect();
        if (rect.position != Vector2.zero && rect.size != Vector2.one)
            parentNode.rect = rect;

        if (curEventType == EventType.DragUpdated
            && parentNode.rect.Contains(Event.current.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
        }

        if ((curEventType == EventType.DragExited)
            && parentNode.rect.Contains(Event.current.mousePosition))
        {
            foreach (var v in DragAndDrop.objectReferences)
            {
                GameObject go = v as GameObject;
                if (go)
                {
                    if (go == gLua.gameObject)
                    {
                        ShowNotification(new GUIContent("EditorNode can't add"));
                        return;
                    }

                    string path = GetSelectPath(go.transform, gLua.transform.parent, parentNode.transform);
                    //string path = GetSelectPath(go.transform, parentNode.transform);
                    if (string.IsNullOrEmpty(path))
                        return;

                    
                    foreach (var tempNode in parentNode.childList)
                    {
                        if (tempNode.transform == go.transform)
                        {
                            ShowNotification(new GUIContent("This node alreay contain drag node"));
                            return;
                        }
                    }


                    if (assistantParentDictClone.TryGetValue(path, out bool tempBool))
                    {
                        ShowNotification(new GUIContent("Can't add same node"));
                        return;
                    } 

                    tempActionList.Add(() =>
                    {
                        CatchNode node = GetNewNode();
                        node.parent = parentNode;
                        node.deep = parentNode.deep + 1;
                        node.transform = go.transform;
                        node.saveName = path;
                        foreach (Component c in node.transform.GetComponents<Component>())
                        {
                            bool b = false;
                            string typeName = c.GetType().Name;
                            componentDictClone.TryGetValue(node.saveName + "-" + typeName, out b);
                            if (b)
                                node.componentSelect.Add(typeName);
                        }
                        parentNode.childList.Add(node);
                        assistantParentDictClone[node.saveName] = true;
                    });
                    
                }
            }
        }
    }

    /// <summary>
    /// 获取相对于传入Transform Root节点的路径
    /// </summary>
    /// <returns></returns>
    private string GetSelectPath(Transform ts, Transform rootTs, Transform subRootTs, string prePath = "", bool findParent = false)
    {
        bool alreadyFind = findParent;
        string path = ts.name;

        if (!string.IsNullOrEmpty(prePath))
        {
            if (ts == rootTs || ts.parent == null)
                path = prePath;
            else
                path += "/" + prePath;
        }

        if (ts == rootTs || ts.parent == null)
        {
            if (alreadyFind)
                return path;
            else
            {
                ShowNotification(new GUIContent("Please check drag gameobject parent is root node"));
                return "";
            }
        }

        if (ts.parent == subRootTs)
            alreadyFind = true;

        return GetSelectPath(ts.parent, rootTs, subRootTs, path, alreadyFind);
    }

    private void TopTools()
    {
        #region line1

        EditorGUILayout.BeginHorizontal();
        guiOutputContent = EditorGUILayout.TextField("SavePath:", guiOutputContent);
        string tempSaveOutputPath = new string(guiOutputContent.ToCharArray());
        if (GUILayout.Button("SelectSavePath"))
        {
            guiOutputContent = EditorUtility.OpenFolderPanel("SetSavePath", guiOutputContent, "");
            if (string.IsNullOrEmpty(guiOutputContent))
            {
                guiOutputContent = tempSaveOutputPath;
            }
            else
            {
                int cutLength = Application.dataPath.Length - "Assets".Length;
                guiOutputContent = guiOutputContent.Substring(cutLength);
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region line2
        EditorGUILayout.BeginHorizontal();
        // --------------Reset To Save Data-------------------
        GUI.color = Color.red;
        if (GUILayout.Button("ResetToDefaults"))
        {
            if (Time.realtimeSinceStartup - lastResetDataClickTime <= resetVaildInterval)
                RevertToSaveData();
            lastResetDataClickTime = Time.realtimeSinceStartup;
        }
        //---------------Reset All-------------------------------
        if (GUILayout.Button("ResetAll"))
        {
            if (Time.realtimeSinceStartup - lastResetAllClickTime <= resetVaildInterval)
                ResetAll();
            lastResetAllClickTime = Time.realtimeSinceStartup;
        }
        GUI.color = defaultColor;
        //---------------Save-----------------------------
        if (GUILayout.Button("SaveConfig"))
        {
            Save();
        }

        //--------------OpenAll---------------------------
        if (GUILayout.Button(openName[openBtnIndex]))
        {
            openBtnIndex = (openBtnIndex + 1) % 2;
            Dictionary<string, bool> temp = new Dictionary<string, bool>();
            foreach (string str in isOpen.Keys)
            {
                temp[str] = openBtnIndex == 1;
            }
            isOpen = temp;
        }
        //--------------Save&OutputLua------------------------
        if (GUILayout.Button("Save&OutputLua"))
        {
            Save();
            AssetDatabase.Refresh();
            OutputLua();
        }

        EditorGUILayout.EndHorizontal();
        #endregion

        #region Tips
        if (nodeRoot.localID == 0)
        {
            GUI.color = diffAPColor;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextArea("LocalID is Empty ! Please Save Config.");
            EditorGUILayout.EndHorizontal();
            GUI.color = defaultColor;
        }
        #endregion
        EditorGUILayout.Space();
    }

    private void LoopShowGUI(CatchNode node)
    {
        foreach (CatchNode cNode in node.childList)
        {
            EditorGUI.indentLevel = cNode.deep;
            string nodeName = cNode.saveName;

            if (!isOpen.TryGetValue(nodeName, out bool bOpen))
                isOpen[nodeName] = defaultOpenState;

            string[] showNodeName = nodeName.Split('/');

            EditorGUILayout.BeginHorizontal();
            GUI.color = foldColor;
            isOpen[nodeName] = EditorGUILayout.Foldout(isOpen[nodeName], showNodeName[showNodeName.Length - 1], true);

            DragEventListener(cNode);

            bool sourceAP = false;
            gLua.assistantParentDict.TryGetValue(nodeName, out sourceAP);

            bool copyAP;
            if (!assistantParentDictClone.TryGetValue(nodeName, out copyAP))
                assistantParentDictClone[nodeName] = sourceAP;

            //string apName = copyAP ? string.Format(parentNodeForm, showNodeName[showNodeName.Length - 1]) : "";

            //if (sourceAP != copyAP)
            //    GUI.color = diffAPColor;
            //else
                GUI.color = assistantParentColor;


            //Rect parentRect = new Rect(GUILayoutUtility.GetLastRect().position + Vector2.right * (15 * (EditorGUI.indentLevel - 1)), GUILayoutUtility.GetLastRect().size);
            //assistantParentDictClone[nodeName] = GUI.Toggle(parentRect, assistantParentDictClone[nodeName],"");// ToggleLeft("Parent", false);
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                RemoveChild(cNode);
                tempActionList.Add(() => 
                {
                    if (node.childList.Contains(cNode))
                    {
                        node.childList.Remove(cNode);
                    }
                });

            }
            //assistantParentDictClone[nodeName] = EditorGUILayout.ToggleLeft(apName, assistantParentDictClone[nodeName]);
            GUI.color = defaultColor;

            EditorGUILayout.EndHorizontal();
            if (!isOpen[nodeName])
                continue;

            foreach (var kvp in cNode.transform.GetComponents<Component>())
            {
                GUI.color = componentColor;
                EditorGUI.indentLevel = cNode.deep + 1;

                bool sourceBool;
                string key = cNode.saveName + "-" + kvp.GetType().Name;

                gLua.componentDict.TryGetValue(key, out sourceBool);

                bool copyBool;
                if (!componentDictClone.TryGetValue(key, out copyBool))
                    componentDictClone[key] = copyBool;

                //EditorGUILayout.BeginHorizontal();
                //EditorGUILayout.LabelField(kvp.Key);

                if (sourceBool != copyBool)
                    GUI.color = diffColor;
                else
                    GUI.color = defaultColor;
                string toggleName = string.Format(componentForm, kvp.GetType().Name);

                componentDictClone[key] = EditorGUILayout.ToggleLeft(toggleName, componentDictClone[key]);
                if (componentDictClone[key] && !cNode.componentSelect.Contains(kvp.GetType().Name))
                    cNode.componentSelect.Add(kvp.GetType().Name);
                else if (!componentDictClone[key] && cNode.componentSelect.Contains(kvp.GetType().Name))
                    cNode.componentSelect.Remove(kvp.GetType().Name);

                    //EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel = cNode.deep;
                GUI.color = defaultColor;
            }
            GUI.color = defaultColor;
            LoopShowGUI(cNode);
        }
    }

    private void RemoveChild(CatchNode node)
    {
        if (assistantParentDictClone.TryGetValue(node.saveName, out bool b))
        {
            assistantParentDictClone.Remove(node.saveName);
        }

        if (node.componentSelect != null)
        {
            string componentStr = ""; 
            foreach (var typeName in node.componentSelect)
            {
                componentStr = node.saveName + "-" + typeName;
                if (componentDictClone.TryGetValue(componentStr, out b))
                {
                    componentDictClone.Remove(componentStr);
                }
            }
        }

        if (node.childList != null)
        {
            foreach (var v in node.childList)
            {
                RemoveChild(v);
            }
        }
    }

    private CatchNode GetNewNode(Transform ts = null, CatchNode parentNode = null)
    {
        CatchNode node = new CatchNode();
        node.childList = new List<CatchNode>();
        node.transform = ts;
        node.parent = parentNode;
        node.componentSelect = new List<string>();
        return node;

    }

    private void GetNode()
    {
        if (gLua.componentDict == null)
            gLua.componentDict = new Dictionary<string, bool>();

        if (gLua.assistantParentDict == null)
            gLua.assistantParentDict = new Dictionary<string, bool>();

        componentDictClone = new Dictionary<string, bool>(gLua.componentDict);
        assistantParentDictClone = new Dictionary<string, bool>(gLua.assistantParentDict);

        if (gLua.rootNode == null)
        {
            nodeRoot = GetNewNode(gLua.transform.parent);
            nodeRoot.saveName = "Root";
            //LoopGetData(nodeRoot);
        }
        else
        {
            Dictionary<long, MsgData> tsDict = new Dictionary<long, MsgData>();
            SetPathID(ref tsDict, gLua.transform.parent);
            nodeRoot = CatchNode.Copy(gLua.transform.parent, gLua.rootNode, tsDict);
        }
    }

    private void SetPathID(ref Dictionary<long, MsgData> dict,Transform ts, string path = "")
    {
        string mPath = path;
        long id = GetLocalID(ts);
        if (string.IsNullOrEmpty(path))
        {
            mPath = "Root";
        }
        else if (mPath.Equals("Root"))
        {
            mPath = ts.name;
        }
        else
        {
            mPath += "/" + ts.name;
        }
        MsgData mData = new MsgData();
        mData.ts = ts;
        mData.path = mPath;
        dict[id] = mData;
        for (int i = 0; i < ts.childCount; i++)
        {
            SetPathID(ref dict, ts.GetChild(i), mPath);
        }
    }

    private void LoopGetData(CatchNode parentNode)
    {
        if (parentNode.parent != null && !isOpen.TryGetValue(parentNode.saveName, out bool bOpen))
            isOpen[parentNode.saveName] = defaultOpenState;

        for (int i = 0; i < parentNode.transform.childCount; i++)
        {

            CatchNode node = GetNewNode(parentNode.transform.GetChild(i), parentNode);
            node.deep = parentNode.deep + 1;
            node.saveName = parentNode.saveName + "/" + node.transform.name;
            foreach (Component c in node.transform.GetComponents<Component>())
            {
                bool b = false;
                string typeName = c.GetType().Name;
                componentDictClone.TryGetValue(node.saveName + "-" + typeName, out b);
                node.componentSelect.Add(typeName);
            }

            parentNode.childList.Add(node);
            LoopGetData(node);
        }
    }

    //----------------------------------------------------------------------Logic------------------------------------------------------------
    private void Save()
    {
        SaveUID();
        var prefabStage = PrefabStageUtility.GetPrefabStage(gLua.transform.gameObject);
        if (prefabStage != null)
        {
            EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }

        gLua.componentDict = new Dictionary<string, bool>(componentDictClone);
        gLua.assistantParentDict = new Dictionary<string, bool>(assistantParentDictClone);
        gLua.savePath = guiOutputContent;
        gLua.rootNode = nodeRoot;
        gLua.strRootNode =JsonUtility.ToJson(nodeRoot);

        var so = new SerializedObject(gLua.transform.GetComponent<GenerateLuaEx>());
        so.ApplyModifiedProperties();
       
        //Transform prefabTs = PrefabUtility.GetCorrespondingObjectFromSource(gLua.rootNode.transform);
        //if (prefabTs) //instance prefab
        //{
        //    PrefabUtility.ApplyPrefabInstance(gLua.rootNode.transform.gameObject, InteractionMode.AutomatedAction);
        //}
        //else if (PrefabStageUtility.GetCurrentPrefabStage().IsPartOfPrefabContents(gLua.rootNode.transform.gameObject)) // open prefab scene
        //{
        //    PrefabUtility.SaveAsPrefabAssetAndConnect(gLua.rootNode.transform.gameObject, PrefabStageUtility.GetCurrentPrefabStage().prefabAssetPath, InteractionMode.AutomatedAction);
        //}
    }

    private void SaveUID(CatchNode cNode = null)
    {
        CatchNode curNode = cNode;
        if (curNode == null)
            curNode = nodeRoot;

        if (curNode.transform)
        {
            curNode.localID = GetLocalID(curNode.transform);
        }

        if (curNode.childList != null)
        {
            foreach (var v in curNode.childList)
            {
                SaveUID(v);
            }
        }
    }

    private long GetLocalID(Transform ts)
    {
        PropertyInfo info = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
        SerializedObject so = new SerializedObject(ts);
        info.SetValue(so, InspectorMode.Debug, null);
        SerializedProperty localIdProp = so.FindProperty("m_LocalIdentfierInFile");
        return localIdProp.intValue;
    }

    private void OutputLua()
    {
        Generate();
    }

    private void Generate()
    {
        string className = gLua.rootNode.transform.name;
        GenerateDefineClass(className);
        GenerateSubClass(className);
        GenerateClass(className);
        AssetDatabase.Refresh();
    }
    
    private void GenerateDefineClass(string className)
    {
        const string luaGenTempletePath = "Assets/VivaFramework/3dr/GeneralUILua/Templete_Define.txt";

        const string luaClassName = "#CLASS";
        const string defineField = "#DEFINE";

        string tempTempleteContent = "";
        string tempTempleteName = "";

        string[] tempArray = new string[2];

        using (StreamReader text = new StreamReader(luaGenTempletePath))
        {
            string tempStr = text.ReadLine();
            while (tempStr != null)
            {
                switch (tempStr)
                {
                    case luaClassName:
                        if (string.IsNullOrEmpty(tempTempleteName))
                            tempTempleteName = luaClassName;
                        else if (!tempTempleteName.Equals(luaClassName))
                            Debug.LogError(tempTempleteName + " not found end sign");
                        else
                        {
                            tempArray[0] = tempTempleteContent.Clone().ToString();
                            tempTempleteContent = "";
                            tempTempleteName = "";
                        }
                        break;
                    case defineField:
                        if (string.IsNullOrEmpty(tempTempleteName))
                            tempTempleteName = defineField;
                        else if (!tempTempleteName.Equals(defineField))
                            Debug.LogError(tempTempleteName + " not found end sign");
                        else
                        {
                            tempArray[1] = tempTempleteContent.Clone().ToString();
                            tempTempleteContent = "";
                            tempTempleteName = "";
                        }
                        break;
                    default:
                        tempTempleteContent += tempStr + "\r\n";
                        break;
                }
                tempStr = text.ReadLine();
            }
        }

        string contentBlock = "";
        GetDefineStr(nodeRoot, ref contentBlock,0,true);

        string generateClass = string.Format(tempArray[0].Replace("CLASSNAME", className), contentBlock);

        using (StreamWriter textWriter = new StreamWriter(gLua.savePath + "/" + className + "_Define.lua"))
        {
            textWriter.Write(generateClass);
            textWriter.Flush();
            textWriter.Close();
        }
    }

    private void GenerateSubClass(string className)
    {
        const string replacePath = "PATH";
        string[] templete = GetGenerateTemplete();

        string contentBlock = "";
        GetContentStr(templete[2], templete[3], nodeRoot, ref contentBlock);

        string generateClass = string.Format(templete[0].Replace("CLASSNAME", className), contentBlock);
        
        string path = "";
        int i = gLua.savePath.LastIndexOf(OutputDefaultPath);
        if (i < 0)
        {
            path = "XXX";
            Debug.LogError("Invaild Path :\"" + OutputDefaultPath +"\". "+ className +".lua need replace XXX on line 1");
        }
        else
        {
            string[] strArray = gLua.savePath.Substring(i + OutputDefaultPath.Length).Split('/');
            foreach (var v in strArray)
            {
                if (!string.IsNullOrEmpty(path))
                    path += ".";
                path += v;
            }
        }

        using (StreamWriter textWriter = new StreamWriter(gLua.savePath + "/" + className + "_Generate.lua"))
        {
            textWriter.Write(generateClass.Replace(replacePath, path));
            textWriter.Flush();
            textWriter.Close();
        }
    }
    
    private void GetDefineStr(CatchNode node, ref string tempContent, int layer,bool lastIndex = false)
    {
        if (layer == 1 && (node.childList == null || node.childList.Count == 0) && (node.componentSelect == null || node.componentSelect.Count == 0)) return;
        string nodeName = "";
        if (node.parent != null)
        {
            string[] array = node.saveName.Split('/');
            string tempStr = array[array.Length - 1];
            nodeName = tempStr.Substring(0, 1).ToLower() + tempStr.Substring(1);

            // if (layer > 1)
            // {
            //     for (int i = 1; i <= layer; i++)
            //     {
            //         tempContent += "\t";
            //     }
            //     tempContent += "---@type UnityEngine.GameObject | table\n";
            // }
            // tempContent += layer == 1 ? "---@type UnityEngine.GameObject | table\n" : "";
            string content = layer == 1 ? "\tself." : "";
  
            if (layer > 1)
            {
                for (int i = 1; i <= layer; i++)
                {
                    content = content + "\t";
                }
            }
      
            content = content + nodeName + " = {\n";
            tempContent += content;
        }

        if (node.childList != null)
        {
            int newLayer = layer + 1;
            int i = 0;
            foreach (var v in node.childList)
            {
                i++;
                GetDefineStr(v, ref tempContent, newLayer,i == node.childList.Count);
            }
        }
        
        int count = 0;
        const string typeInfo = "Assets/Lua/Data/Config/TypeInfo.lua";
        foreach (var c in node.componentSelect)
        {
            count++;
            string lower = c.Substring(0, 1).ToLower() + c.Substring(1);
            tempContent += (count == 1 && node.childList != null && node.childList.Count > 0) ? ",\n" : "\n";
            int newLayer = layer + 1;
            
            if (newLayer > 1)
            {
                for (int i = 1; i <= newLayer; i++)
                {
                    tempContent += "\t";
                }
            }

            string type = "";
            using (StreamReader text = new StreamReader(typeInfo))
            {
                string tempStr = text.ReadLine();
                while (tempStr != null)
                {
                    tempStr = tempStr.Replace("\t", "").Replace(" ", "");
                    if (tempStr.StartsWith(c))
                    {
                        int idx1 = tempStr.IndexOf('(');
                        int idx2 = tempStr.IndexOf(')');
                        type = tempStr.Substring(idx1 + 1, Mathf.Max(idx2 - idx1 - 1,0));

                        int a = 1;
                        break;
                    }
                    tempStr = text.ReadLine();
                }
                text.Close();
            }

            tempContent += "---@type " + type + "\n";
            
            if (newLayer > 1)
            {
                for (int i = 1; i <= newLayer; i++)
                {
                    tempContent += "\t";
                }
            }
            tempContent += lower + " = {}" + (count == node.componentSelect.Count ? "" : ",");
        }

        if (layer != 0)
        { 
            tempContent += "\n";

            for (int i = 1; i <= layer; i++)
            {
                tempContent += "\t";
            }
         
            tempContent += "}";
        }
            
        else
            tempContent += "\n";
        if (!lastIndex)
            tempContent += layer == 1 ? "\n\n\t" : ",\n";
    }
    
    private void GetContentStr(string templeteBind, string templeteComponent, CatchNode node, ref string tempContent, string parentNodeName = "")
    {
        string nodeName = "";
        if (node.parent != null)
        {
            string[] array = node.saveName.Split('/');
            string tempStr = array[array.Length - 1];
            tempStr = tempStr.Substring(0, 1).ToLower() + tempStr.Substring(1);

            if (string.IsNullOrEmpty(parentNodeName))
            {
                nodeName = tempStr;
            }
            else
            {
                nodeName = parentNodeName + "." + tempStr;
            }

            string bind = templeteBind.Replace("NODEPATH", node.saveName).Replace("NODENAME", nodeName);
            
            string component = "";
            foreach (var c in node.componentSelect)
            {
                string lower = c.Substring(0, 1).ToLower() + c.Substring(1);
                component += templeteComponent.Replace("COMPONENT", c).Replace("cOMPONENT", lower);
            }

            tempContent += bind + component ;
        }

        if (node.childList != null)
        {
            foreach (var v in node.childList)
            {
                GetContentStr(templeteBind, templeteComponent, v, ref tempContent, nodeName);
            }
        }
    }
    
    private void GenerateClass(string className)
    {
        const string luaTempletePath = "Assets/VivaFramework/3dr/GeneralUILua/Templete_Class.txt";
        const string replacePath = "PATH";
        const string replaceClassName = "CLASSNAME";

        string classSavePath = gLua.savePath + "/" + className + ".lua";
        classSavePath = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + classSavePath;
        if (File.Exists(classSavePath))
        {
            return;
        }

        string content = "";

        using (StreamReader text = new StreamReader(luaTempletePath))
        {
            string tempStr = text.ReadLine();
            while (tempStr != null)
            {
                content += tempStr + "\r\n";
                tempStr = text.ReadLine();
            }
        }
        string path = "";
        int i = gLua.savePath.LastIndexOf(OutputDefaultPath);
        if (i < 0)
        {
            path = "XXX";
            Debug.LogError("Invaild Path :\"" + OutputDefaultPath +"\". "+ className +".lua need replace XXX on line 1");
        }
        else
        {
            string[] strArray = gLua.savePath.Substring(i + OutputDefaultPath.Length).Split('/');
            foreach (var v in strArray)
            {
                if (!string.IsNullOrEmpty(path))
                    path += ".";
                path += v;
            }
        }
           
        using (StreamWriter textWriter = new StreamWriter(classSavePath))
        {
            textWriter.Write(content.Replace(replaceClassName, className).Replace(replacePath, path));
            textWriter.Flush();
            textWriter.Close();
        }
    }

    private string[] GetGenerateTemplete()
    {
        const string luaGenTempletePath = "Assets/VivaFramework/3dr/GeneralUILua/Templete_Generate.txt";

        const string luaClassName = "#CLASS";
        const string bindContent = "#BINDCONTENT";
        const string bindComponent = "#BINDCOMPONENT";

        string tempTempleteContent = "";
        string tempTempleteName = "";

        string[] tempArray = new string[4];

        using (StreamReader text = new StreamReader(luaGenTempletePath))
        {
            string tempStr = text.ReadLine();
            while (tempStr != null)
            {
                switch (tempStr)
                {
                    case luaClassName:

                        if (string.IsNullOrEmpty(tempTempleteName))
                            tempTempleteName = luaClassName;
                        else if (!tempTempleteName.Equals(luaClassName))
                            Debug.LogError(tempTempleteName + " not found end sign");
                        else
                        {
                            tempArray[0] = tempTempleteContent.Clone().ToString();
                            tempTempleteContent = "";
                            tempTempleteName = "";
                        }
                        break;
                    case bindContent:

                        if (string.IsNullOrEmpty(tempTempleteName))
                            tempTempleteName = bindContent;
                        else if (!tempTempleteName.Equals(bindContent))
                            Debug.LogError(tempTempleteName + " not found end sign");
                        else
                        {
                            tempArray[2] = tempTempleteContent.Clone().ToString();
                            tempTempleteContent = "";
                            tempTempleteName = "";
                        }
                        break;

                    case bindComponent:

                        if (string.IsNullOrEmpty(tempTempleteName))
                            tempTempleteName = bindComponent;
                        else if (!tempTempleteName.Equals(bindComponent))
                            Debug.LogError(tempTempleteName + " not found end sign");
                        else
                        {
                            tempArray[3] = tempTempleteContent.Clone().ToString();
                            tempTempleteContent = "";
                            tempTempleteName = "";
                        }
                        break;

                    default:
                        tempTempleteContent += tempStr + "\r\n";
                        break;
                }

                tempStr = text.ReadLine();
            }
        }

        return tempArray;
    }
}

