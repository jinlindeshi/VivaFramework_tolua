using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class KeyFrameHandleScriptable : ScriptableObject
{
    [Serializable]
    public class AxKeyValue
    {
        public string key;
        public AudioClip axSource;
    }
    [Serializable]
    public class PrefabKeyValue
    {
        public string key;
        public GameObject prefab;
        public int index;
    }

    [Serializable]
    public class FxKeyValue
    {
        public string key;
        public GameObject fxPrefab;
        public int index;
    }
    public List<FxKeyValue> _fxList;
    public List<AxKeyValue> _axList;
    public List<PrefabKeyValue> _PrefabList;

    [Serializable]
    public class AniEvent
    {
        public float floatParameter;
        public string functionName;
        public string stringParameter;
        public int intParameter;
        public UnityEngine.Object objectReferenceParameter;
        public string data;
        public float time;
    }

    [Serializable]
    public class eventEle
    {
        public string aniName;
        public List<AniEvent> eventsArr; 
    }
    [SerializeField]
    public List<eventEle> allEventList = new List<eventEle>();
    //  public Dictionary<string, AnimationEvent[]> animationClipAllEvents = new Dictionary<string, AnimationEvent[]>(); 
     void Awake()
    {
        if(allEventList == null)
        {
            allEventList = new List<eventEle>();
        }      
    }
#if UNITY_EDITOR
    private bool IsAnimationFromFbx(AnimationClip clip)
    {
        ModelImporter modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(clip)) as ModelImporter;
        if (modelImporter != null)
        {
            return true;
           
        }
        else
        {
            return false;
        }
    }
    public void SaveData(AnimationClip[] animationClips)
    {
    
        //  allEventList = new List<eventEle>();
        allEventList.Clear();
        for (int i = 0; i < animationClips.Length; i++)
        {
            float duration = 1;
            bool isFbx = IsAnimationFromFbx(animationClips[i]);
            if (isFbx)
            {
                duration = animationClips[i].length;
            }
            AnimationEvent[] eArr = AnimationUtility.GetAnimationEvents(animationClips[i]);
            eventEle ele = new eventEle();
            ele.aniName = animationClips[i].name;
            ele.eventsArr = new List<AniEvent>();
            for (int j = 0; j < eArr.Length; j++)
            {
                AniEvent e = new AniEvent();
                e.floatParameter = eArr[j].floatParameter;
                e.functionName = eArr[j].functionName;
                e.objectReferenceParameter = eArr[j].objectReferenceParameter;
                e.intParameter = eArr[j].intParameter;
                e.stringParameter = eArr[j].stringParameter;
                e.time = eArr[j].time/ duration;
                ele.eventsArr.Add(e);
            }
            allEventList.Add(ele);
        }
        EditorUtility.SetDirty(this);
        /**  allEventList = new List<eventEle>();
          foreach (KeyValuePair<string, AnimationEvent[]> pair in animationClipAllEvents)
          {
              eventEle ele = new eventEle();
              ele.aniName = pair.Key;
              ele.eventsArr = new List<AniEvent>();
              for (int i = 0;i< pair.Value.Length;i++)
              {
                  AniEvent e = new AniEvent();
                  e.floatParameter = pair.Value[i].floatParameter;
                  e.functionName = pair.Value[i].functionName;
                  e.objectReferenceParameter = pair.Value[i].objectReferenceParameter;
                  e.intParameter = pair.Value[i].intParameter;
                  e.stringParameter = pair.Value[i].stringParameter;
                  e.time = pair.Value[i].time;
                  ele.eventsArr.Add(e);
              }
            //  ele.eventsArr = pair.Value;**/
        //  allEventList.Add(ele);
        //  }
        // _fxList = allEffs;
    }

    //兼容动画编辑器版本的接口
    public void SaveLegencyData(KeyFrameHandler.FxKeyValue[] fxArr)
    {
        _fxList = new List<FxKeyValue>();
        for (int i = 0;i< fxArr.Length;i++)
        {
            FxKeyValue e = new FxKeyValue();
            e.key = fxArr[i].key;
            e.fxPrefab = fxArr[i].fxPrefab;
           _fxList.Add(e);
        }
        EditorUtility.SetDirty(this);
    }
    #endif
}


