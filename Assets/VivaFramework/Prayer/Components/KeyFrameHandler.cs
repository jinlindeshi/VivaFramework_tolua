using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using UnityEditor;
using System.Reflection;
using DG.Tweening;

[ExecuteInEditMode]
public class KeyFrameHandler : MonoBehaviour
{
    const char splitSign = '|';
    const char splitSignBorn = '+';
    public LuaFunction callback;
    public List<Transform> fxRootArr;
    private Animator _animator;
  //  private KeyFrameHandleEffectData effAsset;
    private static string dataPath = "Assets/Framework/3rdParty/Custom/KeyFrameHandleData/";
    [Serializable]
    public struct FxKeyValue
    {
        public string key;
        public GameObject fxPrefab;
    }
    public struct CustomData
    {
        public GameObject fxPrefab;
        public int index;
    }

    public struct PrefabKeyValue
    {
        public string key;
        public GameObject prefab;
    }

    public struct PrefabData
    {
        public GameObject prefab;
        public int index;
    }

    public FxKeyValue[] _fxList;
    private Dictionary<string, CustomData> _fxDict;
    private Dictionary<string, KeyFrameHandleScriptable.AxKeyValue> _axDict;
    private Dictionary<string, List<AnimationEvent>> _eventsDict;

    private Dictionary<string, GameObject> m_TempFxsForEditor = new Dictionary<string, GameObject>();
    private List<GameObject> m_LastParticleObjects = new List<GameObject>();
    public List<GameObject> LastParticleObjects { get { return m_LastParticleObjects; } }  
    private List<GameObject> m_OldFxs = new List<GameObject>();
    public KeyFrameHandleScriptable data;
    public KeyFrameHandleScriptable updateData;  
    #region ParticleSystem
    private List<GameObject> m_ParticleSystems = new List<GameObject>();
    private double m_ParticlePreviousTime;
    private double m_NowTime;
    private ParticleSystem _currentFx;
    public string NextFx;

    //預緩存模型身上所有材質
    private Renderer[] renderers;
    #endregion

    private int animHashName;
    public PrefabKeyValue[] _PrefabList;
    private Dictionary<string, PrefabData> _PrefabDict;
    private Dictionary<int, List<GameObject>> _CachePrefab;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _eventsDict = new Dictionary<string, List<AnimationEvent>>();
        renderers = transform.gameObject.GetComponentsInChildren<Renderer>();
        _CachePrefab = new Dictionary<int, List<GameObject>>();
        // if (fxRoot == null)
        // {
        //     fxRoot = transform;
        // }
        if (fxRootArr == null || fxRootArr.Count == 0)
        {
            fxRootArr = new List<Transform>();
            fxRootArr.Add(transform);
        }else
        {
            fxRootArr[0] = transform;
        }
        // curFxRoot = fxRoot;
        initPrefabDic();
        initFxDic();
        initAxDic();
       //ifOldVersion();
        if (!_animator.runtimeAnimatorController || _animator.runtimeAnimatorController.animationClips != null)
            return;
        // get all attack and skill event info
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if(_eventsDict.ContainsKey(clip.name)) return;
            var aniEvents = new List<AnimationEvent>();
            foreach (AnimationEvent keyFrameEvent in clip.events)
            {

                if (keyFrameEvent.stringParameter.Equals("attack") || keyFrameEvent.stringParameter.Equals("skill"))
                {
                    // Debug.Log(clip.name + " Event : " + keyFrameEvent.time + keyFrameEvent.stringParameter);
                    aniEvents.Add(keyFrameEvent);
                }

            }
            if (aniEvents.Count > 0)
            {
                _eventsDict.Add(clip.name, aniEvents);
            }
        }
    }

    private void OnEnable()
    {
        if (renderers.Length > 0)
        {
            foreach (Renderer r in renderers)
            {
                foreach (Material m in r.sharedMaterials)
                {
                    if (m != null)
                    {
                        m.SetFloat("_Fade", 1);
                    }

                }
            }
        }
    }

    public void initFxDic()
    {
        if(data)
        {
            _fxDict = new Dictionary<string, CustomData>();
            if ( data._fxList != null)
            {
                foreach (KeyFrameHandleScriptable.FxKeyValue entry in data._fxList)
                {
                    CustomData temp = new CustomData();
                    temp.fxPrefab = entry.fxPrefab;
                    temp.index = entry.index;
                    _fxDict.Add(entry.key, temp);
                }
            }
        }
    }

    public void initPrefabDic()
    {
        if (data)
        {
            _PrefabDict = new Dictionary<string, PrefabData>();
            if (data._PrefabList != null)
            {
                foreach (KeyFrameHandleScriptable.PrefabKeyValue entry in data._PrefabList)
                {
                    PrefabData temp = new PrefabData();
                    temp.prefab = entry.prefab;
                    temp.index = entry.index;
                    _PrefabDict.Add(entry.key, temp);
                }
            }
        }
    }


    public void initAxDic()
    {
        if (data)
        {
            _axDict = new Dictionary<string, KeyFrameHandleScriptable.AxKeyValue>();
            if (data._fxList != null)
            {
                foreach (KeyFrameHandleScriptable.AxKeyValue entry in data._axList)
                {
                    KeyFrameHandleScriptable.AxKeyValue temp = new KeyFrameHandleScriptable.AxKeyValue();
                    temp.axSource = entry.axSource;
                    temp.key = entry.key;
                    _axDict.Add(entry.key, temp);
                }
            }
        }

    }
    public void SetScriptableData(KeyFrameHandleScriptable obj)
    {
         data = obj;
    }
    public KeyFrameHandleScriptable GetScriptableData()
    {
        return data;
    }
    public KeyFrameHandleScriptable GetUpdateScriptableData()
    {
        return updateData; 
    }
    public FxKeyValue[] GetEffData()

    {
        return _fxList;
    }
    public void SetEffData(List<KeyFrameHandleScriptable.FxKeyValue> dataList)
    {
        List<KeyFrameHandleScriptable.FxKeyValue> tempList = new List<KeyFrameHandleScriptable.FxKeyValue>();
        for(int i=0;i<= dataList.Count-1;i++)
        {
            KeyFrameHandleScriptable.FxKeyValue e = new KeyFrameHandleScriptable.FxKeyValue();
            e.key = dataList[i].key;
            e.fxPrefab = dataList[i].fxPrefab;
            tempList.Add(e);
        }
        data._fxList = tempList;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != animHashName)
        {
            
            AnimChange(animHashName);
            animHashName = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        }
    }
    void OnDestroy()
    {
        if (callback != null)
        {
            callback.Dispose();
            callback = null;
        }
    }

    void AnimChange(int id)
    {
        if (_CachePrefab.TryGetValue(id, out List<GameObject> list))
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i])
                {
                    Destroy(list[i]);
                }
            }
            list.Clear();
        }
    }

    public AnimationEvent[] GetEvents(string animName)
    {
        if (_eventsDict.ContainsKey(animName))
        {
            return _eventsDict[animName].ToArray();
        }
        else
        {
            return null;
        }
    }
    public void RegisterLuaCallback(LuaFunction lucCallback)
    {
        callback = lucCallback;
    }
    [NoToLua]
    public void NewEvent(string eventStr)
    {
        OnKeyFrameEvent(eventStr);
    }
    [NoToLua]
    public void OnKeyFrameEvent(string eventStr)
    {
        string[] eventParams = eventStr.Split(',');
        if (callback != null)
        {

            callback.BeginPCall();
            for (int i = 0; i < eventParams.Length; i++)
            {
                callback.Push(eventParams[i]);
                // Debug.Log("KeyFrameEvent Params : "+ eventParams[i]);
            }
            callback.PCall();
            callback.EndPCall();
        }
        if (eventParams[0] == "prefab")
        {
            OnKeyFramePrefab(eventParams);
        }
        if(eventParams[0] == "fx")
        {
            OnKeyFrameFx(eventParams);
        }
        if (eventParams[0] == "ax")
        {
            OnKeyFrameAx(eventParams);
        }

        if (eventParams[0] == "FadeOut")
        {
            OnKeyFrameFadeOut(eventParams);
        }
        
        if (eventParams[0] == "FadeIn")
        {
            OnKeyFrameFadeIn(eventParams);
        }

        if (eventParams[0] == "Remove")
        {
            OnKeyFrameRemove(eventParams);
        }


    }

    [NoToLua]
    public void OnKeyFrameRemove(string[] eventParams)
    {
        if (eventParams[0] != null)
        {
            if (Application.isPlaying)
            {
                if (eventParams[0].Equals("Remove"))
                {
                    int hashName = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
                  
                    if (_CachePrefab.TryGetValue(hashName, out List<GameObject> array))
                    {
                        for (int i = array.Count-1; i >= 0; i--)
                        {
                            for(int j = 1;j < eventParams.Length; j ++)
                            {
                                if (array[i] && array[i].name.Equals(eventParams[j]))
                                {
                                    Destroy(array[i]);
                                    array.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }

                    //string fxKey = eventParams[1];
                    //if (!_PrefabDict.ContainsKey(fxKey))
                    //{
                    //    Debug.LogWarning("Keyframe Prafab Warning: Prefab Key not exist, " + fxKey);
                    //    return;
                    //}
                    //Transform curFxRoot = fxRootArr[_PrefabDict[fxKey].index];
                    //Transform fxTrans = curFxRoot.Find(fxKey);
                    //if (fxTrans == null)
                    //{

                    //    GameObject fxObj = Instantiate(_PrefabDict[fxKey].prefab, curFxRoot);
                    //    fxObj.name = fxKey;
                    //    fxTrans = fxObj.transform;
                    //    if (curFxRoot.gameObject.layer != fxObj.layer)
                    //    {

                    //    }
                    //}
                }
            }
        }
    }

    [NoToLua]
    public void OnKeyFrameFadeOut(string[] eventParams)
    {
        if (eventParams[0] != null)
        {
            if (Application.isPlaying)
            {       
                float foTime = 0.3f;
                if (eventParams.Length > 1)
                {
                    if (!string.IsNullOrEmpty(eventParams[1]))
                    {
                        foTime = float.Parse(eventParams[1]);
                    }
                }
                
                foreach (Renderer r in renderers)
                {
                    foreach (Material m in r.materials)
                    {
                        m.DOFloat(0f, "_Fade", foTime);
                    }

                }
            }
        }
    }
    
    [NoToLua]
    public void OnKeyFrameFadeIn(string[] eventParams)
    {
        if (eventParams[0] != null)
        {
            if (Application.isPlaying)
            {
                float fiTime = 0.3f;
                if (eventParams.Length > 1)
                {
                    if (!string.IsNullOrEmpty(eventParams[1]))
                    {
                        fiTime = float.Parse(eventParams[1]);
                    }
                }

                foreach (Renderer r in renderers)
                {
                    foreach (Material m in r.materials)
                    {
                        m.SetFloat("_Fade",0f); 
                        m.DOFloat(1f, "_Fade", fiTime);
                    }

                }
            }
        }
    }
    
    [NoToLua]
    public void OnKeyFrameAx(string[] eventParams)
    {
        // Debug.Log("KeyFrameEvent Params : "+ eventParams);
        if (eventParams[0] != null)
        {
            if (Application.isPlaying)
            {
                if (eventParams[0].Equals("ax"))
                {
                    string axKey = eventParams[1];
                    if (!_axDict.ContainsKey(axKey))
                    {
                        Debug.LogWarning("Keyframe Fx Warning: Fx Key not exist, " + axKey);
                        return;
                    }
                    Transform axTrans = transform.Find(axKey + "_ax");
                    AudioClip ac = _axDict[axKey].axSource;
                    if (axTrans == null )
                    {
                        
                        GameObject obj = new GameObject(axKey + "_ax");
                        obj.AddComponent<AudioSource>(); 
                        obj.transform.SetParent(transform);
                        axTrans = obj.transform;
                    }
                    AudioSource asc = axTrans.GetComponent<AudioSource>();
                    asc.PlayOneShot(ac);
                }
            }
        }
    }
    [NoToLua]
    public void OnKeyFramePrefab(string[] eventParams)
    {
        if (eventParams[0] != null)
        {
            if (Application.isPlaying)
            {
                if (eventParams[0].Equals("prefab"))
                {
                    string[] nameArray = eventParams[1].Split(splitSign);
                    
                    string fxKey = nameArray[0];
                    if (!_PrefabDict.ContainsKey(fxKey))
                    {
                        Debug.LogWarning("Keyframe Prafab Warning: Prefab Key not exist, " + fxKey);
                        return;
                    }
                    Transform curFxRoot = fxRootArr[_PrefabDict[fxKey].index];
                    Transform fxTrans = curFxRoot.Find(fxKey);
                    if (fxTrans == null)
                    {

                        GameObject fxObj = Instantiate(_PrefabDict[fxKey].prefab, curFxRoot);
                        fxObj.name = fxKey;
                        fxTrans = fxObj.transform;
                        if (curFxRoot.gameObject.layer != fxObj.layer)
                        {
                            SetLayerWithChildren(fxTrans, curFxRoot.gameObject.layer);
                        }
                    }
                    string name = nameArray[1];
                    int hashName = Animator.StringToHash(name);
                    if (!_CachePrefab.TryGetValue(hashName, out List<GameObject> array))
                    {
                        array = new List<GameObject>();
                        _CachePrefab[hashName] = array;
                    }
                    array.Add(fxTrans.gameObject);

                    fxTrans.gameObject.SetActive(true);
                    //_currentFx = fxTrans.GetComponent<ParticleSystem>();
                    //if (_currentFx)
                    //{
                    //    SetParticleSystemSpeedWithChildren(fxTrans, _animator.speed);
                    //    _currentFx.Play();
                    //}
                    //fxTrans.GetComponent<FMODParticleEmitter>()?.Play();
                }
            }
        }
    }

    [NoToLua]
    public void OnKeyFrameFx(string[] eventParams)
    {
        // Debug.Log("KeyFrameEvent Params : "+ eventParams);
        if (eventParams[0] != null)
        {
            if (Application.isPlaying)
            {
                if (eventParams[0].Equals("fx"))
                {
                    string fxKey = eventParams[1];
                    if(! _fxDict.ContainsKey(fxKey))
                    {
                        //Debug.LogWarning("Keyframe Fx Warning: Fx Key not exist, " + fxKey);
                        return;
                    }
                    Transform curFxRoot = fxRootArr[_fxDict[fxKey].index];
                    Transform fxTrans = curFxRoot.Find(fxKey);
                    if (fxTrans == null)
                    {

                        GameObject fxObj = Instantiate(_fxDict[fxKey].fxPrefab, curFxRoot);
                        fxObj.name = fxKey;
                        fxTrans = fxObj.transform;
                        if (curFxRoot.gameObject.layer != fxObj.layer)
                        {
                            SetLayerWithChildren(fxTrans, curFxRoot.gameObject.layer);
                        }
                    }
                    _currentFx = fxTrans.GetComponent<ParticleSystem>();
                    if (_currentFx)
                    {
                        SetParticleSystemSpeedWithChildren(fxTrans, _animator.speed);
                        _currentFx.Play();
                    }
                }
            }
        }
    }
    public void DestroyLastFxObject()
    {
        for (int i = 0; i < m_LastParticleObjects.Count; i++)
        {
            DestroyImmediate(m_LastParticleObjects[i]);
        }
        m_LastParticleObjects.Clear();
    }
    public void ClearFx()
    {
        if (_currentFx != null)
        {
            _currentFx.Stop(true);
        }

    }
    private void SetParticleSystemSpeedWithChildren(Transform fxTrans, float speed)
    {
        Component[] particleSystems;
        particleSystems = fxTrans.GetComponentsInChildren(typeof(ParticleSystem));
        if (particleSystems != null)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ParticleSystem.MainModule psMain = ps.main;
                psMain.simulationSpeed = speed;
            }
        }
    }
    private void SetLayerWithChildren(Transform target, int layer)
    {
        target.gameObject.layer = layer;
        foreach (Transform trans in target.GetComponentsInChildren(typeof(Transform), true))
        {
            trans.gameObject.layer = layer;
        }
    }
#if UNITY_EDITOR
    [ContextMenuItem("PlayAnimation", "PlayAnimation")]
    [NoToLua]
    public string animationName;
    [NoToLua]
    public float speed = 1.0f;
    [NoToLua]
    private Coroutine mFx_Coroutine = null;
    [NoToLua]
    private Coroutine mAx_Coroutine = null;
    [NoToLua]
    private Coroutine mPrefab_Coroutine = null;
    [NoToLua]
    List<Coroutine> mFx_Coroutines = new List<Coroutine>();
    [NoToLua]
    List<Coroutine> mAx_Coroutines = new List<Coroutine>();
    [NoToLua]
    List<Coroutine> mPrefab_Coroutines = new List<Coroutine>();
    [NoToLua]
    bool fxEventInit = false;
    [NoToLua]
    public float kDuration;
    [NoToLua]
    private void PlayAnimation()
    {
        // Debug.Log("play anim" + animationName);
        _animator.speed = speed;
        _animator.Play(animationName, -1, 0f); 
        // Debug.Log("all events : " + GetEvents(animationName)?.Length);
    }
    #region EditorAnimator

    [NoToLua]
    public void OnKeyFrameFxForEditor(string[] eventParams, float delyTime, string str, bool onlyOne = true)
    {
        if (onlyOne)
        {
            StopFxCoroutine();
            mFx_Coroutine = StartCoroutine(PlayFxForEditor(eventParams, delyTime, str));
        }
        else
        {
            mFx_Coroutines.Add(StartCoroutine(PlayFxForEditor(eventParams, delyTime, str)));
        }
    }
    [NoToLua]
    public void OnKeyFrameAxForEditor(string[] eventParams, float delyTime, string str, bool onlyOne = true)
    {
        if (onlyOne)
        {
            StopAxCoroutine();
            mAx_Coroutine = StartCoroutine(PlayAxForEditor(eventParams, delyTime, str));
        }
        else
        {
            mAx_Coroutines.Add(StartCoroutine(PlayAxForEditor(eventParams, delyTime, str)));
        }
    }

    [NoToLua]
    public void OnKeyFramePrefabForEditor(string[] eventParams, float delyTime, string str, bool onlyOne = true)
    {
        if (onlyOne)
        {
            StopAxCoroutine();
            mPrefab_Coroutine = StartCoroutine(PlayPrefabForEditor(eventParams, delyTime, str));
        }
        else
        {
            mPrefab_Coroutines.Add(StartCoroutine(PlayPrefabForEditor(eventParams, delyTime, str)));
        }
    }
    [NoToLua]
    public void StopFxCoroutine()
    {
        if (mFx_Coroutine != null)
        {
            StopCoroutine(mFx_Coroutine);
            mFx_Coroutine = null;
        }
    }
    [NoToLua]
    public void StopAxCoroutine()
    {
        if (mAx_Coroutine != null)
        {
            StopCoroutine(mAx_Coroutine);
            mAx_Coroutine = null;
        }
    }
    [NoToLua]
    public void StopPrefabCoroutine()
    {
        if (mPrefab_Coroutine != null)
        {
            StopCoroutine(mPrefab_Coroutine);
            mPrefab_Coroutine = null;
        }
    }
    [NoToLua]
    public void CloseAllFxCoroutine()
    {
        for (int i = 0; i < mFx_Coroutines.Count; i++)
        {
            if (mFx_Coroutines[i] != null)
            {
                StopCoroutine(mFx_Coroutines[i]);
                mFx_Coroutines[i] = null;
            }
        }
        mFx_Coroutines.Clear();
    }
    [NoToLua]
    public void CloseAllAxCoroutine()
    {
        for (int i = 0; i < mAx_Coroutines.Count; i++)
        {
            if (mAx_Coroutines[i] != null)
            {
                StopCoroutine(mAx_Coroutines[i]);
                mAx_Coroutines[i] = null;
            }
        }
        mAx_Coroutines.Clear();
    }

    [NoToLua]
    public void CloseAllPrefabCoroutine()
    {
        for (int i = 0; i < mPrefab_Coroutines.Count; i++)
        {
            if (mPrefab_Coroutines[i] != null)
            {
                StopCoroutine(mPrefab_Coroutines[i]);
                mPrefab_Coroutines[i] = null;
            }
        }
        mPrefab_Coroutines.Clear();
    }
    [NoToLua]
    private IEnumerator PlayFxForEditor(string[] eventParams, float delayTime, string str)
    {
        //yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < data._fxList.Count; i++)
        {
            if (eventParams.Length >= 2)
            {
                // String fxKey = eventParams[1];lk
                Transform curFxRoot = fxRootArr[data._fxList[i].index];
                if (curFxRoot.Find(eventParams[1]) == null)
                {
                    if (data._fxList[i].key == eventParams[1])
                    {
                        GameObject temp = data._fxList[i].fxPrefab;
                        
                        GameObject fxObj = Instantiate(temp, curFxRoot);
                        //fxObj.name = eventParams[1] + splitSignBorn + EditorApplication.timeSinceStartup;
                        foreach (var v in fxObj.GetComponentsInChildren<ParticleSystem>())
                        {
                            if (fxObj.name == v.name)
                                fxObj.name = eventParams[1] + splitSignBorn.ToString() + (EditorApplication.timeSinceStartup + delayTime);
                            else
                                v.name += splitSignBorn.ToString() + (EditorApplication.timeSinceStartup + delayTime);
                        }
                        m_LastParticleObjects.Add(fxObj);
                    }
                }
            }
        }
        NextFx = str;
        CheckParticleSystem(str);
        PlayParticleSystems();
        yield return 0;
    }
    [NoToLua]
    private IEnumerator PlayAxForEditor(string[] eventParams, float delayTime, string str)
    {
        yield return new WaitForSeconds(delayTime);
     
        if(eventParams.Length >= 2 && eventParams[0] == "ax")
        {
            AudioClip ac = null;
            for (int i = 0; i < data._axList.Count; i++)
            {
             
                  
                    if (data._axList[i].key == eventParams[1])
                    {
                        ac = data._axList[i].axSource;

                    }      
           
            }
            if (ac == null)
            {
                Debug.LogError("没有找到播放的声音资源:" + eventParams[1]);
                yield break;
            }
            PublicAudioUtil.PlayClip(ac);
        } 
    }

    [NoToLua]
    private IEnumerator PlayPrefabForEditor(string[] eventParams, float delayTime, string str)
    {
        yield return new WaitForSeconds(delayTime);
        var array = eventParams[1].Split(splitSign);
        for (int i = 0; i < data._PrefabList.Count; i++)
        {
            if (eventParams.Length >= 2)
            {
                // String fxKey = eventParams[1];
                Transform curFxRoot = fxRootArr[data._PrefabList[i].index];
                if (curFxRoot.Find(array[0]) == null)
                {
                    if (data._PrefabList[i].key == array[0])
                    {
                        GameObject temp = data._PrefabList[i].prefab;

                        GameObject fxObj = Instantiate(temp, curFxRoot);
                        fxObj.name = eventParams[1].Split(splitSign)[0];
                        m_LastParticleObjects.Add(fxObj);
                    }
                }
            }
        }

            //string[] nameArray = eventParams[1].Split(splitSign);
            //string fxKey = nameArray[0];
            //if (!_PrefabDict.ContainsKey(fxKey))
            //{
            //    Debug.LogWarning("Keyframe Prafab Warning: Prefab Key not exist, " + fxKey);
            //    return;
            //}
            //Transform curFxRoot = fxRootArr[_PrefabDict[fxKey].index];
            //Transform fxTrans = curFxRoot.Find(fxKey);
            //if (fxTrans == null)
            //{

            //    GameObject fxObj = Instantiate(_PrefabDict[fxKey].prefab, curFxRoot);
            //    fxObj.name = fxKey;
            //    fxTrans = fxObj.transform;
            //    if (curFxRoot.gameObject.layer != fxObj.layer)
            //    {
            //        SetLayerWithChildren(fxTrans, curFxRoot.gameObject.layer);
            //    }
            //}
            //string name = nameArray[1];
            //int hashName = Animator.StringToHash(name);
            //if (!_CachePrefab.TryGetValue(hashName, out List<GameObject> array))
            //{
            //    array = new List<GameObject>();
            //    _CachePrefab[hashName] = array;
            //}
            //array.Add(fxTrans.gameObject);

            //fxTrans.gameObject.SetActive(true);
    }

    [NoToLua]
    public void PlayFxForEditorBake(string[] eventParams)
    {
        for (int i = 0; i < data._fxList.Count; i++)
        {
            if (eventParams.Length >= 2)
            {
                Transform curFxRoot = fxRootArr[data._fxList[i].index];
                if(curFxRoot.Find(eventParams[1]) == null)
                {
                    if (data._fxList[i].key == eventParams[1])
                    {
                        GameObject temp = data._fxList[i].fxPrefab;
                        
                        GameObject fxObj = Instantiate(temp, curFxRoot);
                        fxObj.name = eventParams[1];
                        m_LastParticleObjects.Add(fxObj);
                    }
                }
            }
        }
    }
    [NoToLua]
    private void PlayParticleSystems()
    {
        Debug.Log("PlayParticleSystems");
        if (!fxEventInit)
        {
            EditorApplication.update += PlayParticleSystemForEditor;
            fxEventInit = !fxEventInit;
        }
        // ClearParticleSystems();
        CheckParticleSystem(NextFx);
        m_NowTime = EditorApplication.timeSinceStartup;
        
    }
    [NoToLua]
    public void StopParticleSystems()
    {
        Debug.Log("StopParticleSystems");
        if (fxEventInit)
        {
            EditorApplication.update -= PlayParticleSystemForEditor;
            fxEventInit = !fxEventInit;
        }
        // ClearParticleSystems();
        CheckParticleSystem(NextFx);
        
    }
    [NoToLua]
    public void CheckParticleSystem(string key = "")
    {
        //ClearParticleSystems();
        if (!Application.isPlaying)
        {
            if(key == "")
            {
                return;
            }
            string str = key.Split(splitSign)[0];
            int index = 0;
            if (_fxDict.TryGetValue(str, out CustomData cData))
            {
                index = cData.index;
            }
            else if (_PrefabDict.TryGetValue(str, out PrefabData pData))
            {
                index = pData.index;
            }
            Transform curFxRoot = fxRootArr[index];
            if (curFxRoot != null)
            {
                Transform[] trans = null;
                if (!string.IsNullOrEmpty(str))
                {
                    Transform strTrans = null;// curFxRoot.transform.Find(str);
                    for (int i = 0; i < curFxRoot.transform.childCount; i++)
                    {
                        if (curFxRoot.transform.GetChild(i).name.Split(splitSignBorn)[0].Equals(str))
                        {
                            strTrans = curFxRoot.transform.GetChild(i);
                            break;
                        }
                    }


                    if (strTrans != null)
                    {
                        trans = strTrans.GetComponentsInChildren<Transform>(true);
                    }
                }
                else
                {
                    trans = curFxRoot.GetComponentsInChildren<Transform>(true);
                }
                if (trans == null)
                {
                    return;
                }
                foreach (Transform child in trans)
                {
                    if (child.GetComponent<ParticleSystem>() != null && !m_ParticleSystems.Contains(child.gameObject))
                    {
                        //child.gameObject.GetComponent<ParticleSystem>().useAutoRandomSeed = false;
                        m_ParticleSystems.Add(child.gameObject);
                    }
                }
            }
        }
    }
    [NoToLua]
    public void ClearParticleSystems()
    {
        foreach (var item in m_ParticleSystems)
        {
            if (item != null)
            {
                item.GetComponent<ParticleSystem>().Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
        m_ParticleSystems.Clear();
    }
    [NoToLua]
    public void PlayFxUseSliderForEditor(float curTime, string str)
    {
        foreach (var item in m_ParticleSystems)
        {
            if (item != null)
            {
                item.GetComponent<ParticleSystem>().Simulate(curTime);
            }
        }
    }
    [NoToLua]
    public void PlayParticleSystemForEditor()
    {

        m_ParticlePreviousTime = EditorApplication.timeSinceStartup;
        float time = (float)(m_ParticlePreviousTime - m_NowTime);
        for (int i = 0; i < m_ParticleSystems.Count; i++)
        {
            if (m_ParticleSystems[i] != null)
            {
                var v = m_ParticleSystems[i].transform.name.Split(splitSignBorn);
                time = (float)(m_ParticlePreviousTime - (v.Length > 1 ? double.Parse(v[1]) : EditorApplication.timeSinceStartup));
                //Debug.LogError(m_ParticleSystems[i].transform.name + "  " + time);
                m_ParticleSystems[i].GetComponent<ParticleSystem>().Simulate(time % kDuration);
                // m_ParticleSystems[i].GetComponent<ParticleSystem>().useAutoRandomSeed = false;
            }
        }
    }
    [NoToLua]
    public static KeyFrameHandleScriptable CreateDataFile(string path)
    {
        KeyFrameHandleScriptable asset = LoadData(path) as KeyFrameHandleScriptable;
        string m_result;
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<KeyFrameHandleScriptable>();
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            m_result = "文件创建成功：" + path;
        }
        else
        {
            m_result = "创建的文件已经存在：" + path;
        }
        /**if (ifOldVersion())
        {
            asset.SaveLegencyData(GetEffData());
        }
        else
        {

        }**/
        return asset;
        //SerializedObject serObject = new SerializedObject(this);
        //var data = serObject.FindProperty("data");
        //data = asset;
        //serObject.ApplyModifiedProperties();
        //serObject.Update();
    }
    private static KeyFrameHandleScriptable LoadData(string path)
    {
        KeyFrameHandleScriptable asset = AssetDatabase.LoadAssetAtPath<KeyFrameHandleScriptable>(path);
        return asset;
    }
    [NoToLua]
    public bool ifOldVersion()
    {
        if(_fxList!=null && _fxList.Length>0)
        {
            return true;
        }else
        {
            return false;
        }
    }
    #endregion 
#endif
}
#if UNITY_EDITOR
[NoToLua]
public static class PublicAudioUtil
{

    public static void PlayClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayClip",
            BindingFlags.Static | BindingFlags.Public
        );
        method.Invoke(
            null,
            new object[3] {
                clip,1,false
            }
        );
    }

}
#endif
