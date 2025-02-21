﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class VivaFramework_GameManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(VivaFramework.GameManager), typeof(Manager));
		L.RegFunction("CreateProgressUI", CreateProgressUI);
		L.RegFunction("CheckExtractResource", CheckExtractResource);
		L.RegFunction("OnResourceInited", OnResourceInited);
		L.RegFunction("ReLaunch", ReLaunch);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateProgressUI(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			VivaFramework.GameManager obj = (VivaFramework.GameManager)ToLua.CheckObject<VivaFramework.GameManager>(L, 1);
			System.Collections.IEnumerator o = obj.CreateProgressUI();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckExtractResource(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			VivaFramework.GameManager obj = (VivaFramework.GameManager)ToLua.CheckObject<VivaFramework.GameManager>(L, 1);
			obj.CheckExtractResource();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnResourceInited(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			VivaFramework.GameManager obj = (VivaFramework.GameManager)ToLua.CheckObject<VivaFramework.GameManager>(L, 1);
			obj.OnResourceInited();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReLaunch(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			VivaFramework.GameManager obj = (VivaFramework.GameManager)ToLua.CheckObject<VivaFramework.GameManager>(L, 1);
			obj.ReLaunch();
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

