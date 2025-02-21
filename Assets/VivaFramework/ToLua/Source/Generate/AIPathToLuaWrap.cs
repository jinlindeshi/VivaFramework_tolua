﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class AIPathToLuaWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AIPathToLua), typeof(Pathfinding.AIPath));
		L.RegFunction("SetLuaCallBack", SetLuaCallBack);
		L.RegFunction("StopMove", StopMove);
		L.RegFunction("OnTargetReached", OnTargetReached);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLuaCallBack(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			AIPathToLua obj = (AIPathToLua)ToLua.CheckObject<AIPathToLua>(L, 1);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.SetLuaCallBack(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopMove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AIPathToLua obj = (AIPathToLua)ToLua.CheckObject<AIPathToLua>(L, 1);
			obj.StopMove();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnTargetReached(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AIPathToLua obj = (AIPathToLua)ToLua.CheckObject<AIPathToLua>(L, 1);
			obj.OnTargetReached();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

