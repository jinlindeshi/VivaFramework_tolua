﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_Rendering_Universal_CameraExtensionsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("CameraExtensions");
		L.RegFunction("GetUniversalAdditionalCameraData", GetUniversalAdditionalCameraData);
		L.RegFunction("GetVolumeFrameworkUpdateMode", GetVolumeFrameworkUpdateMode);
		L.RegFunction("SetVolumeFrameworkUpdateMode", SetVolumeFrameworkUpdateMode);
		L.RegFunction("UpdateVolumeStack", UpdateVolumeStack);
		L.RegFunction("DestroyVolumeStack", DestroyVolumeStack);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUniversalAdditionalCameraData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
			UnityEngine.Rendering.Universal.UniversalAdditionalCameraData o = UnityEngine.Rendering.Universal.CameraExtensions.GetUniversalAdditionalCameraData(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVolumeFrameworkUpdateMode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
			UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode o = UnityEngine.Rendering.Universal.CameraExtensions.GetVolumeFrameworkUpdateMode(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetVolumeFrameworkUpdateMode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
			UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode arg1 = (UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode)ToLua.CheckObject(L, 2, typeof(UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode));
			UnityEngine.Rendering.Universal.CameraExtensions.SetVolumeFrameworkUpdateMode(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateVolumeStack(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
				UnityEngine.Rendering.Universal.CameraExtensions.UpdateVolumeStack(arg0);
				return 0;
			}
			else if (count == 2)
			{
				UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
				UnityEngine.Rendering.Universal.UniversalAdditionalCameraData arg1 = (UnityEngine.Rendering.Universal.UniversalAdditionalCameraData)ToLua.CheckObject<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(L, 2);
				UnityEngine.Rendering.Universal.CameraExtensions.UpdateVolumeStack(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rendering.Universal.CameraExtensions.UpdateVolumeStack");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyVolumeStack(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
				UnityEngine.Rendering.Universal.CameraExtensions.DestroyVolumeStack(arg0);
				return 0;
			}
			else if (count == 2)
			{
				UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 1, typeof(UnityEngine.Camera));
				UnityEngine.Rendering.Universal.UniversalAdditionalCameraData arg1 = (UnityEngine.Rendering.Universal.UniversalAdditionalCameraData)ToLua.CheckObject<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(L, 2);
				UnityEngine.Rendering.Universal.CameraExtensions.DestroyVolumeStack(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rendering.Universal.CameraExtensions.DestroyVolumeStack");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

