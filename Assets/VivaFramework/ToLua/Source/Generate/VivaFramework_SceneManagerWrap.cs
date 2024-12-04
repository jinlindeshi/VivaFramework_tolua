﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class VivaFramework_SceneManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(VivaFramework.SceneManager), typeof(Manager));
		L.RegFunction("LoadSceneAsync", LoadSceneAsync);
		L.RegFunction("UnLoadSceneAsync", UnLoadSceneAsync);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSceneAsync(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			VivaFramework.SceneManager obj = (VivaFramework.SceneManager)ToLua.CheckObject<VivaFramework.SceneManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			UnityEngine.SceneManagement.LoadSceneMode arg2 = (UnityEngine.SceneManagement.LoadSceneMode)ToLua.CheckObject(L, 4, typeof(UnityEngine.SceneManagement.LoadSceneMode));
			obj.LoadSceneAsync(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnLoadSceneAsync(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			VivaFramework.SceneManager obj = (VivaFramework.SceneManager)ToLua.CheckObject<VivaFramework.SceneManager>(L, 1);
			UnityEngine.SceneManagement.Scene arg0 = StackTraits<UnityEngine.SceneManagement.Scene>.Check(L, 2);
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			obj.UnLoadSceneAsync(arg0, arg1);
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

