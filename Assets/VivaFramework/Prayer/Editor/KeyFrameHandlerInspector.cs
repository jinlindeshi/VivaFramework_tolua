using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[CustomEditor(typeof(KeyFrameHandler))]
public class KeyFrameHandlerInspector : Editor
{
    private KeyFrameHandler keyFrameHandler { get { return target as KeyFrameHandler; } }
    private Animator _animator;
    private string[] stateNames;
    private string curStateName = "idle";
    private AnimationClip[] animationClips;

    List<AnimatorState> animatorStates = new List<AnimatorState>();
    AnimatorStateMachine animatorStateMachine;
    AnimationEvent[] curAnimationEvents = new AnimationEvent[] { };

    private float curAnimatorStateLength;
    private bool curAnimatorIsLooping;
    private bool isPlayNewAnimatorState;
    private float costTime;

    // 当前是否是预览播放状态
    private bool m_Playing;
    // 当前运行时间
    private float m_RunningTime;
    // 上一次系统时间
    private double m_PreviousTime;
    // 滑动杆的当前时间
    private static float m_CurTime;
    // 是否已经烘培过
    private bool m_HasBake;
    // 总的记录时间
    private float m_RecorderStopTime;
    // 滑动杆总长度
    private static float kDuration = 25;

    private int m_Index;
    private float m_LastTime;

    private string m_LastAnimationName;

    #region ParticleSystemFields
    private List<GameObject> m_ParticleSystems = new List<GameObject>();
    private double m_ParticlePreviousTime;
    private double m_NowTime;

    private float m_BeginTime;// 确定开始播放时间，然后和总动画时间做差值
    private List<float> m_AllBeginTime = new List<float>();
    private Dictionary<float, string> m_AllEventTimeAndFx = new Dictionary<float, string>();
    #endregion

    #region AnimatorEventsFields
    private GUIStyle m_TextFieldRoundEdge;
    private GUIStyle m_TextFieldRoundEdgeCancelButton;
    private GUIStyle m_TextFieldRoundEdgeCancelButtonEmpty;
    private GUIStyle m_TransparentTextField;
    private static string m_InputText;
    private static string m_DataNameInputText = "";
    private static string m_result = "";
    private Dictionary<string, AnimationEvent[]> m_AnimationClipDefultEvents = new Dictionary<string, AnimationEvent[]>();
    //private static string[] dataName;
   // private static int m_DataIndex;
    #endregion

    #region ModifyAnimatorEventFields
    private GUIStyle m_ModifyTextFieldRoundEdge;
    private GUIStyle m_ModifyTextFieldRoundEdgeCancelButton;
    private GUIStyle m_ModifyTextFieldRoundEdgeCancelButtonEmpty;
    private GUIStyle m_ModifyTransparentTextField;
    private string m_ModifyInputText;
    private bool m_ModifyMark;
    private string m_CurSelectEventOldParm;
    private int m_CurSelectEventIndex;
    #endregion
    private static string dataPath = "Assets/Framework/3rdParty/Custom/KeyFrameHandleData/";
    private string defaultName;
    public static KeyFrameHandleScriptable asset;
    public static KeyFrameHandleScriptable updateAsset;
    //    public static KeyFrameHandleData dataAsset;
    void Awake()
    {
       
        _animator = keyFrameHandler.GetComponent<Animator>();

        RuntimeAnimatorController runtimeAnimatorController = _animator.runtimeAnimatorController;
        animationClips = runtimeAnimatorController.animationClips;
        for (int i = 0; i < animationClips.Length; i++)
        {
            if (!m_AnimationClipDefultEvents.ContainsKey(animationClips[i].name))
            {
                m_AnimationClipDefultEvents.Add(animationClips[i].name, AnimationUtility.GetAnimationEvents(animationClips[i]));
            }
        }
   
        AnimatorController animatorController = _animator.runtimeAnimatorController as AnimatorController;

        if (!Application.isPlaying)
        {
            animatorStateMachine = animatorController.layers[0].stateMachine;
            stateNames = new string[animatorStateMachine.states.Length];
            for (int i = 0; i < animatorStateMachine.states.Length; i++)
            {
                animatorStates.Add(animatorStateMachine.states[i].state);
                stateNames[i] = animatorStateMachine.states[i].state.name;
            }
        } 
        else
        {
            animatorStates = new List<AnimatorState>();
            stateNames = new string[] { };
        }
        Array.Sort(stateNames);
        m_InputText = "";

        if (Application.isPlaying == false)
        {
            if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
                UnityEngine.Object go = PrefabUtility.GetOutermostPrefabInstanceRoot(keyFrameHandler.gameObject);
                dataPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(go);
            }
            else
            {
                dataPath = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage().prefabAssetPath;
            }
            // Debug.Log("Data path : "+ dataPath );
            string[] pathArr = dataPath.Split('/');
            dataPath = "";
            for (int i = 0; i < pathArr.Length; i++)
            {
                if (i <= pathArr.Length - 2)
                {
                    dataPath += pathArr[i] + "/";
                }
            }
            defaultName = Path.GetFileNameWithoutExtension(pathArr[pathArr.Length - 1]);
            m_DataNameInputText = defaultName;
            //CreateDataFile();
        }
    }
    void InitAniEvents()
    {
        AddAllAnimationEvents();
    }
    void OnEnable()
    {
        m_PreviousTime = EditorApplication.timeSinceStartup;
        EditorApplication.update += inspectorUpdate;
    }

    void OnDisable()
    {
        EditorApplication.update -= inspectorUpdate;

        // for (int i = 0; i < animatorStates.Count; i++)
        // {
        //     if (animatorStates[i].name == "idle")
        //     {
        //         animatorStateMachine.defaultState = animatorStates[i];
        //     }
        // }
    }
    private void inspectorUpdate()
    {
        var delta = EditorApplication.timeSinceStartup - m_PreviousTime;
        m_PreviousTime = EditorApplication.timeSinceStartup;

        if (!Application.isPlaying && m_Playing)
        {
            m_RunningTime = Mathf.Clamp(m_RunningTime + (float)delta, 0f, 25);
            UpdateAnimation();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Not Editor Properties");
        serializedObject.Update();
        DrawDataNameTextField(false, "请输入要保存数据的唯一文件名，使用脚本绑定的文件命名,例如：siwa");
       if (GUILayout.Button("CreateOrLoadDataFile"))
        {
            CreateDataFile();
            //SaveData(animationClips);
        }
        EditorGUILayout.LabelField("result:" + m_result);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("data"));
        if (keyFrameHandler.GetScriptableData())
        {
            SerializedObject dataSer2 = new SerializedObject(keyFrameHandler.GetScriptableData());
            var effArr = dataSer2.FindProperty("_fxList");
            if (EditorGUILayout.PropertyField(effArr, true))
            {
                EditorGUI.indentLevel++;
               // effArr.arraySize = EditorGUILayout.DelayedIntField("Size", effArr.arraySize);
                for (int i = 0; i < effArr.arraySize; i++)
                {
                    var e = effArr.GetArrayElementAtIndex(i);
                     
                    EditorGUILayout.PropertyField(e);
                }
                EditorGUI.indentLevel--;
            }
            dataSer2.ApplyModifiedProperties();

            SerializedObject dataSer3 = new SerializedObject(keyFrameHandler.GetScriptableData());
            var prefabArr = dataSer3.FindProperty("_PrefabList");
            if (EditorGUILayout.PropertyField(prefabArr, true))
            {
                EditorGUI.indentLevel++;
                // effArr.arraySize = EditorGUILayout.DelayedIntField("Size", effArr.arraySize);
                for (int i = 0; i < prefabArr.arraySize; i++)
                {
                    var e = prefabArr.GetArrayElementAtIndex(i);

                    EditorGUILayout.PropertyField(e);
                }
                EditorGUI.indentLevel--;
            }
            dataSer3.ApplyModifiedProperties();

            SerializedObject dataSer = new SerializedObject(keyFrameHandler.GetScriptableData());
            var audioArr = dataSer.FindProperty("_axList");
            if (EditorGUILayout.PropertyField(audioArr, true))
            {
                EditorGUI.indentLevel++;
                // effArr.arraySize = EditorGUILayout.DelayedIntField("Size", effArr.arraySize);
                for (int i = 0; i < audioArr.arraySize; i++)
                {
                    var e = audioArr.GetArrayElementAtIndex(i);

                    EditorGUILayout.PropertyField(e);
                }
                EditorGUI.indentLevel--;
            }
            dataSer.ApplyModifiedProperties();
        }
       // EditorGUILayout.PropertyField(serializedObject.FindProperty("fxRoot"));
         var elements = this.serializedObject.FindProperty("fxRootArr");
        if (EditorGUILayout.PropertyField(elements, true))
        {
            EditorGUI.indentLevel++;
           // elements.arraySize = EditorGUILayout.DelayedIntField("Size", elements.arraySize);
            for (int i = 0; i < elements.arraySize; i++)
            {
                var element = elements.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element);
            }
            EditorGUI.indentLevel--;
        }
        //绘制数组对象
        /**   var elements = this.serializedObject.FindProperty("_fxList");
           if (EditorGUILayout.PropertyField(elements, true))
           {
               EditorGUI.indentLevel++;
               elements.arraySize = EditorGUILayout.DelayedIntField("Size", elements.arraySize);
               for (int i = 0; i < elements.arraySize; i++)
               {
                   var element = elements.GetArrayElementAtIndex(i);
                   EditorGUILayout.PropertyField(element);
               }
               EditorGUI.indentLevel--;
           }**/
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("animationName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(10);

        if (!Application.isPlaying)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Play Whole Amination");

            EditorGUILayout.LabelField(string.Format("LastAnimation :   {0}", m_LastAnimationName));

            //keyFrameHandler.CurAnimatorState = (AllAnimatorState)EditorGUILayout.EnumPopup("CurAnimatorState", keyFrameHandler.CurAnimatorState);
            m_Index = EditorGUILayout.Popup(m_Index, stateNames);
            if (!Application.isPlaying)
            {
                for (int i = 0; i < animatorStateMachine.states.Length; i++)
                {
                    if (curStateName == animatorStateMachine.states[i].state.name)
                    {
                        if(animatorStateMachine.states[i].state.motion == null )
                        {
                            Debug.LogError("Animation Clip is missing : " + curStateName);
                            break;
                        }
                        for (int j = 0; j < animationClips.Length; j++)
                        {
                            if ( animatorStateMachine.states[i].state.motion.name == animationClips[j].name)
                            {
                                kDuration = animationClips[j].length; 
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            
            if (GUILayout.Button("Play"))
            {
                if (!Application.isPlaying)
                {
                    StopPlaying();
                    keyFrameHandler.DestroyLastFxObject();
                    PlayAnimation(stateNames[m_Index]);
                }
            }
            curStateName = stateNames[m_Index];

            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Use Slider Control");
            //EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Bake"))
            {
                keyFrameHandler.DestroyLastFxObject();
                StopPlaying();
                BakeAnimation(curStateName, true);
                StopPlaying();
            }
            m_CurTime = EditorGUILayout.Slider("Time:", m_CurTime, 0f, kDuration);
            if (m_AllEventTimeAndFx.ContainsKey(m_CurTime))
            {
                m_BeginTime = m_CurTime;
                keyFrameHandler.NextFx = m_AllEventTimeAndFx[m_BeginTime];
                keyFrameHandler.CheckParticleSystem(keyFrameHandler.NextFx);
            }
            if (m_CurTime >= m_BeginTime)
            {
                if (m_LastTime != (m_CurTime - m_BeginTime))
                {
                    m_LastTime = m_CurTime - m_BeginTime;
                    keyFrameHandler.PlayFxUseSliderForEditor(m_CurTime - m_BeginTime, keyFrameHandler.NextFx);
                }
            }

            ManualUpdateAnimation();

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Add New AnimationEvent");
            DrawInputTextField(false, "请输入拼接的字符串，格式参考为\"fx,atk1\"");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                AddAnimationEvents();
            }
            if (GUILayout.Button("Reset"))
            {
                ResetAnimationClipDefultEvents();
            }
            if (GUILayout.Button("ClearAll"))
            {
                ClearAllEvents();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Current Clip All Events");
            EditorGUILayout.LabelField("stringParameter    time");
            curAnimationEvents = GetCurAnimationEvents();
            string str = "";
            if (curAnimationEvents != null)
            {
                if (curAnimationEvents.Length > 0)
                {
                    for (int i = 0; i < curAnimationEvents.Length; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(string.Format("{0}                {1}秒", curAnimationEvents[i].stringParameter, curAnimationEvents[i].time));

                        //以下代码为是否开启修改当前选中的帧事件的参数功能的按钮，如果有需要在自行打开吧
                        //if (GUILayout.Button("Modify"))
                        //{
                        //    m_ModifyMark = true;
                        //    m_CurSelectEventIndex = i;
                        //    m_CurSelectEventOldParm = string.Format("{0}                {1}", curAnimationEvents[i].stringParameter, curAnimationEvents[i].time);
                        //}

                        GUILayout.Space(10);
                        if (GUILayout.Button("Delete"))
                        {
                            DeleteCurSelectEvent(curAnimationEvents, i);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Not Have Events!");
                }
            }
            else
            {
                EditorGUILayout.LabelField("Not Have Events!");
            }
            GUILayout.Space(20);
          
            //m_DataIndex = EditorGUILayout.Popup(m_DataIndex, dataName);
            /**if (GUILayout.Button("SaveData"))
            {
               // CreateDataFile();
                SaveData(animationClips);
            }**/
            EditorGUILayout.LabelField("动画或者模型更新后，需要手动更新");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("updateData"));
            
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Update"))
            {
               /** asset= keyFrameHandler.GetScriptableData();
                if(asset == null)
                {
                    Debug.LogError("请先选择保存的原始数据,然后更新");
                    return;
                }**/
               // asset = LoadData(dataPath + dataName[m_DataIndex] + ".asset") as KeyFrameHandleScriptable;
              //  keyFrameHandler.SetEffData(asset._fxList);
                InitAniEvents();
                //SaveData(m_AnimationClipDefultEvents, true);
            }

            //以下代码为修改当前选中的帧事件的参数，如果有需要在自行打开吧
            //if (m_ModifyMark)
            //{ 
            //    GUILayout.Space(15);
            //    EditorGUILayout.LabelField("ModifyCurSelectEventParm!!!");
            //    EditorGUILayout.LabelField(string.Format("CurSelectEventOldParm :   {0}", m_CurSelectEventOldParm));
            //    EditorGUILayout.BeginHorizontal();
            //    DrawInputTextField(true);
            //    if (GUILayout.Button("Save"))
            //    {
            //        Debug.LogError(m_CurSelectEventOldParm);
            //        m_ModifyMark = false;
            //        ModifyCurSelectEvent(curAnimationEvents, m_CurSelectEventIndex);
            //    }
            //    EditorGUILayout.EndHorizontal();
            //}
            SerializedObject dataSer2 = new SerializedObject(keyFrameHandler.GetScriptableData());
            var effArr = dataSer2.FindProperty("_fxList");
            string error = "";
            if (GUILayout.Button("CheckFX"))
            {
                for (int i = 0; i < effArr.arraySize; i++)
                {
                    var go = effArr.GetArrayElementAtIndex(i).FindPropertyRelative("fxPrefab").objectReferenceValue as GameObject;
                    foreach (var v in go.GetComponentsInChildren<ParticleSystem>())
                    {
                        if (v.loop)
                        {
                            error += go.name +"------"+v.name + " is loop \r\n";
                        }

                        if (v.playOnAwake)
                        {
                            error += go.name + "------"+v.name + " is onAwake \r\n";
                        }
                    }
                }
                if (error.Length > 0)
                {
                    Debug.LogError(error);
                }
            }
        }
    }

    #region PlayAnimator
    private void PlayAnimation(string stateName)
    {
        if (Application.isPlaying)
            return;

        m_LastAnimationName = stateName;

        List<AnimationEvent> events_fx = new List<AnimationEvent>();
        List<AnimationEvent> events_ax = new List<AnimationEvent>();
        List<AnimationEvent> events_Prefab = new List<AnimationEvent>();
        for (int i = 0; i < animatorStateMachine.states.Length; i++)
        {
            if (stateName == animatorStateMachine.states[i].state.name)
            {
                for (int j = 0; j < animationClips.Length; j++)
                {
                    if (animatorStateMachine.states[i].state.motion.name == animationClips[j].name)
                    {
                        kDuration = animationClips[i].length / 3;
                        curAnimatorIsLooping = animationClips[j].isLooping;
                        curAnimatorStateLength = animationClips[j].length;

                        AnimationEvent[] animationEvents = AnimationUtility.GetAnimationEvents(animationClips[j]);
                        if (animationEvents != null)
                        {
                            for (int k = 0; k < animationEvents.Length; k++)
                            {
                                string[] parms = animationEvents[k].stringParameter.Split(',');
                                if (parms.Length >= 2)
                                {
                                    if (string.Equals(parms[0], "fx"))
                                    {
                                        events_fx.Add(animationEvents[k]);
                                    }
                                    //keyFrameHandler.OnKeyFrameFxForEditor(parms, animationEvents[k].time);
                                    if (string.Equals(parms[0], "ax"))
                                    {
                                        events_ax.Add(animationEvents[k]);
                                    }
                                    if (string.Equals(parms[0], "prefab"))
                                    {
                                        events_Prefab.Add(animationEvents[k]);
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        if (events_fx.Count >= 2)
        {
            keyFrameHandler.CloseAllFxCoroutine();
        }
        if (events_ax.Count >= 2)
        {
            keyFrameHandler.CloseAllAxCoroutine();
        }
        if (events_Prefab.Count >= 2)
        {
            keyFrameHandler.CloseAllPrefabCoroutine();
        }

        for (int i = 0; i < events_fx.Count; i++)
        {
            string[] parms = events_fx[i].stringParameter.Split(',');

            float realTime = events_fx[i].time ;     // //因为原有时间会变成*当前动画长度

            keyFrameHandler.OnKeyFrameFxForEditor(parms, /*events[i].time*/realTime, parms[1], events_fx.Count == 1);
        }
        for (int i = 0; i < events_ax.Count; i++)
        {
            string[] parms = events_ax[i].stringParameter.Split(',');

            float realTime = events_ax[i].time;     // //因为原有时间会变成*当前动画长度

            keyFrameHandler.OnKeyFrameAxForEditor(parms, /*events[i].time*/realTime, parms[1], events_ax.Count == 1);
        }

        for (int i = 0; i < events_Prefab.Count; i++)
        {
            string[] parms = events_Prefab[i].stringParameter.Split(',');

            float realTime = events_Prefab[i].time;     // //因为原有时间会变成*当前动画长度

            keyFrameHandler.OnKeyFramePrefabForEditor(parms, /*events[i].time*/realTime, parms[1], events_Prefab.Count == 1);
        }


        curStateName = stateName;
        PreviewAnimation(stateName);

        keyFrameHandler.kDuration = kDuration;
    }

    private AnimationEvent[] GetCurAnimationEvents()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < animatorStateMachine.states.Length; i++)
            {
                if (curStateName == animatorStateMachine.states[i].state.name)
                {
                    for (int j = 0; j < animationClips.Length; j++)
                    {
                        if (animatorStateMachine.states[i].state.motion != null && animatorStateMachine.states[i].state.motion.name == animationClips[j].name)
                        {
                            return AnimationUtility.GetAnimationEvents(animationClips[j]);
                        }
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 进行预览播放
    /// </summary>
    public void PreviewAnimation(string stateName)
    {
        if (!Application.isPlaying)
        {
            BakeAnimation(stateName);

            m_RunningTime = 0f;
            isPlayNewAnimatorState = true;
            m_Playing = true;
        }
    }
    /// <summary>
    /// 烘培记录动画数据
    /// </summary>
    private void BakeAnimation(string stateName, bool isUseSlider = false)
    {
        Debug.Log("Bake "+ stateName + isUseSlider);
        if (Application.isPlaying)
            return;

        m_AllEventTimeAndFx.Clear();
        Debug.Log("animatorStateMachine.states.Length : " + animatorStateMachine.states.Length);
        float testFrameRate = 0f;
        for (int i = 0; i < animatorStateMachine.states.Length; i++)
        {
            if (stateName == animatorStateMachine.states[i].state.name)
            {
                Debug.Log("stateName : " + stateName);
                for (int j = 0; j < animationClips.Length; j++)
                {
                    if (animatorStateMachine.states[i].state.motion.name == animationClips[j].name)
                    {
                        curAnimatorIsLooping = animationClips[j].isLooping;
                        curAnimatorStateLength = animationClips[j].length;
                       testFrameRate = animationClips[j].frameRate;
                        Debug.Log("curAnimatorStateLength : " + curAnimatorStateLength);
                        AnimationEvent[] animationEvents = AnimationUtility.GetAnimationEvents(animationClips[j]);
                        Debug.Log("animationEvents:" + animationEvents);
                        if (animationEvents != null)
                        {
                            if (animationEvents.Length > 0)
                            {
                                for (int k = 0; k < animationEvents.Length; k++)
                                {
                                    string[] parms = animationEvents[k].stringParameter.Split(',');
                                    if (parms.Length >= 2)
                                    {
                                        kDuration = animationClips[i].length;
                                        m_BeginTime = animationEvents[k].time;    //animationEvents[k].time
                                        if (!m_AllEventTimeAndFx.ContainsValue(parms[1]))
                                        {
                                            m_AllEventTimeAndFx.Add(animationEvents[k].time / kDuration, parms[1]);
                                        }

                                        if (isUseSlider)
                                        {
                                            keyFrameHandler.PlayFxForEditorBake(parms);
                                            keyFrameHandler.CheckParticleSystem();
                                        }
                                    }
                                    else
                                    {
                                        kDuration = animationClips[i].length;
                                    }
                                }
                            }
                            else
                            {
                                kDuration = animationClips[i].length;
                            }
                        }
                        else
                        {
                            kDuration = animationClips[i].length;
                        }
                    }
                }
            }
        }
  
        //keyFrameHandler.CheckParticleSystem();

        const float frameRate = 30f;
        int frameCount = (int)((curAnimatorStateLength * frameRate) + 10);
        _animator.Rebind();
        _animator.StopPlayback();
        _animator.recorderStartTime = 0;//Time.deltaTime;

        // 开始记录指定的帧数
        _animator.StartRecording(frameCount);
        _animator.Play(stateName);
        for (var j = 0; j < frameCount - 1; j++)
        {
            // 记录每一帧
            _animator.Update( 1.0f/frameRate);
        }
        // 完成记录
        _animator.StopRecording();
        m_RecorderStopTime = _animator.recorderStopTime;
        Debug.Log("m_RecorderStopTime: " + m_RecorderStopTime);
        // 开启回放模式
        _animator.StartPlayback();
    }

    ///  
    /// 预览播放状态下的更新
    /// </summary>
    private void UpdateAnimation()
    {
        if (!Application.isPlaying && m_Playing)
        {
            if (_animator.runtimeAnimatorController != null)
            {
                _animator.StartPlayback();
                // Debug.Log("m_RunningTime: " + m_RunningTime);
                _animator.playbackTime = m_RunningTime;
                _animator.Play(curStateName);
                _animator.Update(0);

                m_CurTime = m_RunningTime;

                if(m_CurTime >= curAnimatorStateLength ) 
                {
                    if(curAnimatorIsLooping)
                    {
                        m_RunningTime = 0;
                    }
                    else
                    {
                        StopPlaying();
                    }
                    
                }
            }
        }
    }

    /// <summary>
    /// 非预览播放状态下，通过滑杆来播放当前动画帧
    /// </summary>
    private void ManualUpdateAnimation()
    {
        if (!Application.isPlaying)
        {
            if (m_CurTime > 0)
            {
                _animator.StartPlayback();
                _animator.playbackTime = m_CurTime;
                _animator.Play(curStateName/*, -1, 0f*/);
                //_animator.speed = m_CurTime;
                // _animator.Update(Time.deltaTime);
                _animator.Update(0);
            }
        }
    }


    /// <summary>
    /// 停止预览播放
    /// </summary>
    public void StopPlaying()
    {
        if (Application.isPlaying)
        {
            return;
        }
        _animator.StopPlayback();
        m_CurTime = 0f;
        m_Playing = false;
        keyFrameHandler.NextFx = "";
        keyFrameHandler.StopParticleSystems();
        keyFrameHandler.StopFxCoroutine();
        keyFrameHandler.StopAxCoroutine();
        keyFrameHandler.CloseAllFxCoroutine();
    }
    #endregion

    #region AnimatorEvents
    // 绘制输入框
    private void DrawInputTextField(bool isModify,string tips)
    {
        if (m_TextFieldRoundEdge == null)
        {
            m_TextFieldRoundEdge = new GUIStyle("SearchTextField");
            m_TextFieldRoundEdgeCancelButton = new GUIStyle("SearchCancelButton");
            m_TextFieldRoundEdgeCancelButtonEmpty = new GUIStyle("SearchCancelButtonEmpty");
            m_TransparentTextField = new GUIStyle(EditorStyles.whiteLabel);
            m_TransparentTextField.normal.textColor = EditorStyles.textField.normal.textColor;
        }

        //获取当前输入框的Rect(位置大小)
        Rect position = EditorGUILayout.GetControlRect();
        //设置圆角style的GUIStyle
        GUIStyle textFieldRoundEdge = m_TextFieldRoundEdge;
        //设置输入框的GUIStyle为透明，所以看到的“输入框”是TextFieldRoundEdge的风格
        GUIStyle transparentTextField = m_TransparentTextField;
        //选择取消按钮(x)的GUIStyle
        GUIStyle gUIStyle;
        if (!isModify)
        {
            gUIStyle = (m_InputText != "") ? m_TextFieldRoundEdgeCancelButton : m_TextFieldRoundEdgeCancelButtonEmpty;
        }
        else
        {
            gUIStyle = (m_ModifyInputText != "") ? m_TextFieldRoundEdgeCancelButton : m_TextFieldRoundEdgeCancelButtonEmpty;

        }

        //输入框的水平位置向左移动取消按钮宽度的距离
        position.width -= gUIStyle.fixedWidth;
        //如果面板重绘
        if (Event.current.type == EventType.Repaint)
        {
            //根据是否是专业版来选取颜色
            GUI.contentColor = (EditorGUIUtility.isProSkin ? Color.black : new Color(0f, 0f, 0f, 0.5f));
            //当没有输入的时候提示“请输入”
            if (!isModify)
            {
                if (string.IsNullOrEmpty(m_InputText))
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(tips), 0);
                }
                else
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(""), 0);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(m_ModifyInputText))
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(tips), 0);
                }
                else
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(""), 0);
                }
            }
            //因为是“全局变量”，用完要重置回来
            GUI.contentColor = Color.white;
        }
        Rect rect = position;
        //为了空出左边那个放大镜的位置
        float num = textFieldRoundEdge.CalcSize(new GUIContent("")).x - 2f;
        rect.width -= num;
        rect.x += num;
        rect.y += 1f;//为了和后面的style对其
        if (!isModify)
        {
            m_InputText = EditorGUI.TextField(rect, m_InputText, transparentTextField);
        }
        else
        {
            m_ModifyInputText = EditorGUI.TextField(rect, m_ModifyInputText, transparentTextField);
        }
        //绘制取消按钮，位置要在输入框右边
        position.x += position.width;
        position.width = gUIStyle.fixedWidth;
        position.height = gUIStyle.fixedHeight;
        if (GUI.Button(position, GUIContent.none, gUIStyle) && (isModify ? m_ModifyInputText != "" : m_InputText != ""))
        {
            if (!isModify)
            {
                m_InputText = "";
            }
            else
            {
                m_ModifyInputText = "";
            }
            //用户是否做了输入
            GUI.changed = true;
            //把焦点移开输入框
            GUIUtility.keyboardControl = 0;
        }
    }
    private void DrawDataNameTextField(bool isModify, string tips)
    {
        if (m_TextFieldRoundEdge == null)
        {
            m_TextFieldRoundEdge = new GUIStyle("SearchTextField");
            m_TextFieldRoundEdgeCancelButton = new GUIStyle("SearchCancelButton");
            m_TextFieldRoundEdgeCancelButtonEmpty = new GUIStyle("SearchCancelButtonEmpty");
            m_TransparentTextField = new GUIStyle(EditorStyles.whiteLabel);
            m_TransparentTextField.normal.textColor = EditorStyles.textField.normal.textColor;
        }

        //获取当前输入框的Rect(位置大小)
        Rect position = EditorGUILayout.GetControlRect();
        //设置圆角style的GUIStyle
        GUIStyle textFieldRoundEdge = m_TextFieldRoundEdge;
        //设置输入框的GUIStyle为透明，所以看到的“输入框”是TextFieldRoundEdge的风格
        GUIStyle transparentTextField = m_TransparentTextField;
        //选择取消按钮(x)的GUIStyle
        GUIStyle gUIStyle;
        if (!isModify)
        {
            gUIStyle = (m_DataNameInputText != "") ? m_TextFieldRoundEdgeCancelButton : m_TextFieldRoundEdgeCancelButtonEmpty;
        }
        else
        {
            gUIStyle = (m_ModifyInputText != "") ? m_TextFieldRoundEdgeCancelButton : m_TextFieldRoundEdgeCancelButtonEmpty;

        }

        //输入框的水平位置向左移动取消按钮宽度的距离
        position.width -= gUIStyle.fixedWidth;
        //如果面板重绘
        if (Event.current.type == EventType.Repaint)
        {
            //根据是否是专业版来选取颜色
            GUI.contentColor = (EditorGUIUtility.isProSkin ? Color.black : new Color(0f, 0f, 0f, 0.5f));
            //当没有输入的时候提示“请输入”
            if (!isModify)
            {
                if (string.IsNullOrEmpty(m_DataNameInputText))
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(tips), 0);
                }
                else
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(""), 0);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(m_ModifyInputText))
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(tips), 0);
                }
                else
                {
                    textFieldRoundEdge.Draw(position, new GUIContent(""), 0);
                }
            }
            //因为是“全局变量”，用完要重置回来
            GUI.contentColor = Color.white;
        }
        Rect rect = position;
        //为了空出左边那个放大镜的位置
        float num = textFieldRoundEdge.CalcSize(new GUIContent("")).x - 2f;
        rect.width -= num;
        rect.x += num;
        rect.y += 1f;//为了和后面的style对其
        if (!isModify)
        {
            m_DataNameInputText = EditorGUI.TextField(rect, m_DataNameInputText, transparentTextField);
        }
        else
        {
            m_ModifyInputText = EditorGUI.TextField(rect, m_ModifyInputText, transparentTextField);
        }
        //绘制取消按钮，位置要在输入框右边
        position.x += position.width;
        position.width = gUIStyle.fixedWidth;
        position.height = gUIStyle.fixedHeight;
        if (GUI.Button(position, GUIContent.none, gUIStyle) && (isModify ? m_ModifyInputText != "" : m_DataNameInputText != ""))
        {
            if (!isModify)
            {
                m_DataNameInputText = "";
            }
            else
            {
                m_ModifyInputText = "";
            }
            //用户是否做了输入
            GUI.changed = true;
            //把焦点移开输入框
            GUIUtility.keyboardControl = 0;
        }
    }
    private string MadeValid()
    {
        string text = m_InputText;
        var array = text.Split(',');
        switch (array[0])
        {
            case "prefab":
                string str = "prefab,";
                for (int i= 1; i < array.Length; i++)
                {
                    var content = array[i].Split('|');
                    if (content.Length < 2)
                    {
                        str += content[0]+"|" + stateNames[m_Index]+",";
                    }
                }
                text = str.Substring(0, str.Length - 1);
                break;
        }
        return text;
    }

    private AnimationEvent[] GenerateAnimationEvents(AnimationClip animationClip)
    {
        string str = MadeValid();

        float duration = 1;
        bool isFbx = IsAnimationFromFbx(animationClip);
        if(isFbx)
        {
            duration = kDuration;
        }
        //原有的帧事件
        AnimationEvent[] animationEvents = AnimationUtility.GetAnimationEvents(animationClip);
        AnimationEvent[] newAnimationEvents = new AnimationEvent[animationEvents.Length + 1];
        for (int j = 0; j < animationEvents.Length; j++)
        {
            Debug.Log("animationEvents[j].time: " + animationEvents[j].time);
            animationEvents[j].time = animationEvents[j].time / duration;     //因为原有时间会变成*当前动画长度
            newAnimationEvents[j] = animationEvents[j];
        }
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = "OnKeyFrameEvent";
        animationEvent.stringParameter = str;
        animationEvent.time = m_CurTime / duration;
        Debug.Log("animationEvent.time : " + animationEvent.time + " " + duration + " " + isFbx + " m_CurTime: " + m_CurTime);
        newAnimationEvents[animationEvents.Length] = animationEvent;
     //   SaveData(animationClip.name,newAnimationEvents);
        return newAnimationEvents;
    }
    private bool IsAnimationFromFbx(AnimationClip clip)
    {
        ModelImporter modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(clip)) as ModelImporter;
        if (modelImporter != null)
        {
            return true;
        }else
        {
            return false;
        }
    }
    private void AddAnimationEvents()
    {
 
        if(m_InputText == null || m_InputText == "")
        {
            return;
        }
        for (int i = 0; i < animationClips.Length; i++)
        {
         //   Debug.LogError(animationClips[i].name);
            if (string.Equals(animationClips[i].name, stateNames[m_Index]))
            {
                // ModelImporter modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(animationClips[i])) as ModelImporter;
                // bool isFbx = IsAnimationFromFbx(animationClips[i]);
                // if (modelImporter != null)
                // {
                //     isFbx = true;
                // }

                //原有的帧事件
                // AnimationEvent[] animationEvents = AnimationUtility.GetAnimationEvents(animationClips[i]);

                // AnimationEvent[] newAnimationEvents = new AnimationEvent[animationEvents.Length + 1];
                // for (int j = 0; j < animationEvents.Length; j++)
                // {
                //     animationEvents[j].time = animationEvents[j].time / kDuration;     //因为原有时间会变成*当前动画长度
                //     newAnimationEvents[j] = animationEvents[j];
                // }
                // AnimationEvent animationEvent = new AnimationEvent();
                // animationEvent.functionName = "OnKeyFrameEvent";
                // animationEvent.stringParameter = m_InputText;
                // animationEvent.time = m_CurTime / kDuration;
                // newAnimationEvents[animationEvents.Length] = animationEvent;
               
                var newAnimationEvents = GenerateAnimationEvents(animationClips[i]);
                GenerAnimationEvent(animationClips[i], newAnimationEvents,false);
                break;
            }
        }
        SaveData(animationClips);
        _animator.Rebind();
    }
    private void AddAllAnimationEvents()
    {
        for (int i = 0; i < animationClips.Length; i++)
        {
                GenerAnimationEvent(animationClips[i], null,true);
        }
        keyFrameHandler.SetScriptableData(updateAsset);
        SaveData(animationClips);
        _animator.Rebind();
    }
    private void CreateDataFile()
    {
        asset = KeyFrameHandler.CreateDataFile(dataPath + m_DataNameInputText + ".asset");
        if (keyFrameHandler.ifOldVersion())
        {
            asset.SaveLegencyData(keyFrameHandler.GetEffData());
        }
        //keyFrameHandler.SetScriptableData(asset);
        var data = serializedObject.FindProperty("data");
        data.objectReferenceValue = asset;
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        /**asset = LoadData(dataPath + m_DataNameInputText + ".asset") as KeyFrameHandleScriptable;
         if(asset == null)
         {
             asset = ScriptableObject.CreateInstance<KeyFrameHandleScriptable>();
             AssetDatabase.CreateAsset(asset, dataPath + m_DataNameInputText + ".asset");
             AssetDatabase.SaveAssets();
             m_result = "文件创建成功："+ dataPath + m_DataNameInputText + ".asset";

         }else
         {
             m_result = "创建的文件已经存在：" + dataPath + m_DataNameInputText + ".asset";
            
         }
         if(keyFrameHandler.ifOldVersion())
         {
             asset.SaveLegencyData(keyFrameHandler.GetEffData());
         }else
         {

         }
         keyFrameHandler.SetScriptableData(asset);
        var data =  serializedObject.FindProperty("data");
        data.objectReferenceValue = asset;
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();**/
        SaveData(animationClips);

    }
    private void SaveData(AnimationClip[] animationClips)
    {
        if(keyFrameHandler.GetScriptableData() == null)
        {
            Debug.LogError("请先加载或创建一个数据文件");
            return;
        }
        asset = keyFrameHandler.GetScriptableData();
        asset.SaveData(animationClips);
        AssetDatabase.SaveAssets();
    }
    private  void SaveAllData(AnimationClip[] animationClips)
    {
        asset = ScriptableObject.CreateInstance<KeyFrameHandleScriptable>();
        asset.SaveData(animationClips);
        //  asset.ifUpdate = ifUpdate;
        AssetDatabase.CreateAsset(asset, dataPath + m_DataNameInputText + ".asset");
        AssetDatabase.SaveAssets();

        /** SerializedObject serObject = new SerializedObject(asset);
         var allEventList = serObject.FindProperty("allEventList");
         for (int i = 0; i < allEventList.arraySize; i++)
         {
             var ele = allEventList.GetArrayElementAtIndex(i);
             var eventsArr = ele.FindPropertyRelative("eventsArr");
             var aniName = ele.FindPropertyRelative("aniName");
             
             for (int j = 0; j < eventsArr.arraySize; j++)
             {
                 if(eventsArr.arraySize>0)
                 {
                     var eve = eventsArr.GetArrayElementAtIndex(j);
                     var z = eve.FindPropertyRelative("floatParameter");
                     var floatv = animationClipAllEvents[aniName.stringValue][j].floatParameter;                
                     z.floatValue = floatv;
                    // eve.GetArrayElementAtIndex(j).FindPropertyRelative("functionName").stringValue = animationClipAllEvents[aniName.stringValue][j].functionName;
                     //eve.GetArrayElementAtIndex(j).FindPropertyRelative("intParameter").intValue = animationClipAllEvents[aniName.stringValue][j].intParameter;
                    // eve.GetArrayElementAtIndex(j).FindPropertyRelative("objectReferenceParameter").objectReferenceValue = animationClipAllEvents[aniName.stringValue][j].objectReferenceParameter;
                    // eve.GetArrayElementAtIndex(j).FindPropertyRelative("data").stringValue = animationClipAllEvents[aniName.stringValue][j].stringParameter;
                    // eve.GetArrayElementAtIndex(j).FindPropertyRelative("time").floatValue = animationClipAllEvents[aniName.stringValue][j].time;
                 }  
             }
         }
         serObject.ApplyModifiedProperties();**/



        /** dataAsset = LoadNameListData(dataPath + "KeyFrameHandleData" + ".asset");
         if(dataAsset == null)
         {
             dataAsset = ScriptableObject.CreateInstance<KeyFrameHandleData>();
             AssetDatabase.CreateAsset(dataAsset, dataPath + "KeyFrameHandleData" + ".asset");
         }
         for(int i=0;i<= dataAsset.dataList.Count-1;i++)
         {
             if(dataAsset.dataList[i] == m_DataNameInputText)
             {
                 //Debug.LogError("保存的文件已经存在，请重新命名");
                 m_result = " 成功覆盖已存在的文件";
                 return;
             }       
         }
        
         dataAsset.SaveDataName(m_DataNameInputText);
         dataName = dataAsset.dataList.ToArray();**/
        //    AssetDatabase.SaveAssets();
        //   m_result = " 新文件创建并保存成功";
    }
    private static KeyFrameHandleScriptable LoadData(string path)
    {
        asset = AssetDatabase.LoadAssetAtPath<KeyFrameHandleScriptable>(path);
        return asset;
    }
   /** private static KeyFrameHandleData LoadNameListData(string path)
    {
       dataAsset = AssetDatabase.LoadAssetAtPath<KeyFrameHandleData>(path);
       return dataAsset;
    }**/
    private  void GenerAnimationEvent(AnimationClip animationClip, AnimationEvent[] eventGroup,bool readData)
    {
        if (readData)
        {
            List<AnimationEvent> customList = new List<AnimationEvent>();
            eventGroup = customList.ToArray();
            updateAsset = keyFrameHandler.GetUpdateScriptableData();
            if (updateAsset == null)
            {
                Debug.LogError("请选择要保存的数据");
            }
            for (int i = 0; i <= updateAsset.allEventList.Count - 1; i++)
            {
                KeyFrameHandleScriptable.eventEle ele = updateAsset.allEventList[i];
                if(animationClip.name == ele.aniName)
                {
                    // customEvent = ele.eventsArr.ToArray();
                    for (int j = 0; j <= ele.eventsArr.Count - 1; j++)
                    {
                        AnimationEvent e = new AnimationEvent();
                        e.floatParameter = ele.eventsArr[j].floatParameter;
                        e.functionName = ele.eventsArr[j].functionName;
                        e.objectReferenceParameter = ele.eventsArr[j].objectReferenceParameter;
                        e.intParameter = ele.eventsArr[j].intParameter;
                        e.stringParameter = ele.eventsArr[j].stringParameter;
                        e.time = ele.eventsArr[j].time;
                        customList.Add(e);
                    }
                    eventGroup = customList.ToArray();
                }
            }
        }
         if (eventGroup == null)
        {
            Debug.LogError("eventGroup不能为空");
            return;
        }
        ModelImporter modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(animationClip)) as ModelImporter;
        if (modelImporter != null)
        {
          /*  ModelImporterAnimationType a = modelImporter.animationType;
            bool b = modelImporter.importAnimation;
            for (int i=0;i< modelImporter.clipAnimations.Length;i++)
            {
                modelImporter.clipAnimations[i].events = eventGroup;
            }*/
            if(modelImporter.clipAnimations.Length == 0)
            {
                modelImporter.clipAnimations = modelImporter.defaultClipAnimations;
            }
          
            SerializedObject serializedObject = new SerializedObject(modelImporter);
            SerializedProperty clipAnimations = serializedObject.FindProperty("m_ClipAnimations");
            Debug.Log("clipAnimations.arraySize " + clipAnimations.arraySize);
            for (int i = 0; i < clipAnimations.arraySize; i++)
            {
               
               AnimationClipInfoProperties clipInfoProperties = new AnimationClipInfoProperties(clipAnimations.GetArrayElementAtIndex(i));
                Debug.Log("AnimationClipInfoProperties " + clipInfoProperties.name + animationClip.name);
                if (clipInfoProperties.name == animationClip.name)
                {
                    clipInfoProperties.SetEvents(eventGroup);
                    serializedObject.ApplyModifiedProperties();
                    AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(animationClip));
                    break;
                }
            }
         //   AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(animationClip));
            //  AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        else
        {
            AnimationUtility.SetAnimationEvents(animationClip, eventGroup);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void ResetAnimationClipDefultEvents()
    {
        for (int i = 0; i < animationClips.Length; i++)
        {
            if (string.Equals(animationClips[i].name, stateNames[m_Index]))
            {
                //最原始的帧事件
                if (m_AnimationClipDefultEvents.ContainsKey(animationClips[i].name))
                {
                    AnimationEvent[] animationEvents = m_AnimationClipDefultEvents[animationClips[i].name];

                    AnimationUtility.SetAnimationEvents(animationClips[i], animationEvents);
                    break;
                }
            }
        }
        _animator.Rebind();
    }
    /// <summary>
    /// 删除当前动画片段的帧事件
    /// </summary>
    private void ClearAllEvents()
    {
        for (int i = 0; i < animationClips.Length; i++)
        {
            if (string.Equals(animationClips[i].name, stateNames[m_Index]))
            {
                GenerAnimationEvent(animationClips[i], new AnimationEvent[] { },false);
                break;
            }
        }
        SaveData(animationClips);
    }

    /// <summary>
    /// 删除当前动画片段的选中的帧事件
    /// </summary>
    private void DeleteCurSelectEvent(AnimationEvent[] curAnimationEvents, int deleteIndex)
    {

        for (int i = 0; i < animationClips.Length; i++)
        {
            if (string.Equals(animationClips[i].name, stateNames[m_Index]))
            {
                bool isFbx = IsAnimationFromFbx(animationClips[i]);
                
                float duration = 1;
                if(isFbx)
                {
                    duration = kDuration;
                }
                List<AnimationEvent> tempAnimationEvents = new List<AnimationEvent>();
                for (int idx = 0; idx < curAnimationEvents.Length; idx++)
                {
                    tempAnimationEvents.Add(curAnimationEvents[idx]);
                }
                tempAnimationEvents.RemoveAt(deleteIndex);
                curAnimationEvents = new AnimationEvent[tempAnimationEvents.Count];
                for (int idx = 0; idx < tempAnimationEvents.Count; idx++)
                {
                    tempAnimationEvents[idx].time = tempAnimationEvents[idx].time / duration;
                    curAnimationEvents[idx] = tempAnimationEvents[idx];
                }
              //  SaveData(animationClips[i].name,curAnimationEvents);
                GenerAnimationEvent(animationClips[i], curAnimationEvents,false);
                break;
            }
        }
        SaveData(animationClips);
    }

    /// <summary>
    /// 修改当前动画片段的选中的帧事件的参数
    /// </summary>
    private void ModifyCurSelectEvent(AnimationEvent[] curAnimationEvents, int modifyIndex)
    {
        // string[] strs = m_ModifyInputText.Split(',');
        // if (strs.Length == 2)
        // {
        //     if (string.Equals(strs[0], "fx"))
        //     {
        for (int i = 0; i < animationClips.Length; i++)
        {
            if (string.Equals(animationClips[i].name, stateNames[m_Index]))
            {
                curAnimationEvents[modifyIndex].stringParameter = m_ModifyInputText;
                GenerAnimationEvent(animationClips[i], curAnimationEvents,false);
                break;
            }
        }
        SaveData(animationClips);
        // }
        // else
        // {
        //     Debug.LogError("输入有误，请输入格式为:\"fx,atck1\"");
        // }
        // }
        // else
        // {
        //     Debug.LogError("输入有误，请输入格式为:\"fx,atck1\"");
        // }
        _animator.Rebind();
    }

    #endregion
}
