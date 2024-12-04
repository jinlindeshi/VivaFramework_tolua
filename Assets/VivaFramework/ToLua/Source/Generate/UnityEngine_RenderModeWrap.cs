﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_RenderModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(UnityEngine.RenderMode));
		L.RegVar("ScreenSpaceOverlay", get_ScreenSpaceOverlay, null);
		L.RegVar("ScreenSpaceCamera", get_ScreenSpaceCamera, null);
		L.RegVar("WorldSpace", get_WorldSpace, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
		TypeTraits<UnityEngine.RenderMode>.Check = CheckType;
		StackTraits<UnityEngine.RenderMode>.Push = Push;
	}

	static void Push(IntPtr L, UnityEngine.RenderMode arg)
	{
		ToLua.Push(L, arg);
	}

	static bool CheckType(IntPtr L, int pos)
	{
		return TypeChecker.CheckEnumType(typeof(UnityEngine.RenderMode), L, pos);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ScreenSpaceOverlay(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.RenderMode.ScreenSpaceOverlay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ScreenSpaceCamera(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.RenderMode.ScreenSpaceCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WorldSpace(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.RenderMode.WorldSpace);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UnityEngine.RenderMode o = (UnityEngine.RenderMode)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

