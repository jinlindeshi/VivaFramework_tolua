using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LuaInterface;
using Pathfinding;

//小助手
public class SeekerToLua:Seeker
{
    
    
    private bool _editorInit = false;
    public void EditorInit()
    {
        if (_editorInit == true)
        {
            return;
        }

        _editorInit = true;
    }

    //类型转化
    public static Vector3 IntToVector(Int3 i3)
    {
        return (Vector3) i3;
    }
    
    private void Start()
    {
        EditorInit();
    }


    private LuaFunction _moveCallBack;
    public void TakeMove(Vector3 goal, LuaFunction callBack)
    {
        _moveCallBack = callBack;
        StartPath(transform.position, goal, GetPathComplete);
    }

    void GetPathComplete(Path p)
    {
//        p.path.Count
        if (_moveCallBack != null)
        {
            _moveCallBack.Call(p);
        }
    }
}