using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LuaInterface;

//小助手
public class BehaviorToLua:BehaviorTree
{
    private Dictionary<String, LuaFunction> _dic = new Dictionary<string, LuaFunction>();
    
    
    private bool _editorInit = false;
    public void EditorInit()
    {
        if (_editorInit == true)
        {
            return;
        }

        _editorInit = true;
    }
    private void Start()
    {
        EditorInit();
    }

    //暂停
    public void Pause()
    {
        DisableBehavior(true);
    }
    //恢复
    public void Resume()
    {
        // EnableBehavior();
        BehaviorManager.instance.EnableBehavior(this);
    }

    //停止
    public void Stop()
    {
        DisableBehavior(false);
    }

    public void RegisterLuaFun(String name, LuaFunction fun)
    {
//        print("RegisterLuaFun " + _dic);
        if (_dic.ContainsKey(name) == true)
        {
            _dic.Remove(name);
        }
        _dic.Add(name, fun);
    }

    public void InvokeFun(String name, LuaAction action, String statusFunName,
        float paramFloat, bool paramBool, bool paused = false)
    {
        if (_dic.ContainsKey(name) == false)
        {
            Debug.Log("BehaviorToLua 未注册的LuaAction - " + name);
            return;
        }

        if (_dic[name].GetLuaState() != null)
        {
            _dic[name].Call(action, statusFunName, paramFloat, paramBool, paused);
        }
    }
    
    
}