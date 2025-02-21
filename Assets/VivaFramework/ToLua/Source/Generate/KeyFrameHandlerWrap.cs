﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class KeyFrameHandlerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(KeyFrameHandler), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("initFxDic", initFxDic);
		L.RegFunction("initPrefabDic", initPrefabDic);
		L.RegFunction("initAxDic", initAxDic);
		L.RegFunction("SetScriptableData", SetScriptableData);
		L.RegFunction("GetScriptableData", GetScriptableData);
		L.RegFunction("GetUpdateScriptableData", GetUpdateScriptableData);
		L.RegFunction("GetEffData", GetEffData);
		L.RegFunction("SetEffData", SetEffData);
		L.RegFunction("GetEvents", GetEvents);
		L.RegFunction("RegisterLuaCallback", RegisterLuaCallback);
		L.RegFunction("DestroyLastFxObject", DestroyLastFxObject);
		L.RegFunction("ClearFx", ClearFx);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("callback", get_callback, set_callback);
		L.RegVar("fxRootArr", get_fxRootArr, set_fxRootArr);
		L.RegVar("_fxList", get__fxList, set__fxList);
		L.RegVar("data", get_data, set_data);
		L.RegVar("updateData", get_updateData, set_updateData);
		L.RegVar("NextFx", get_NextFx, set_NextFx);
		L.RegVar("_PrefabList", get__PrefabList, set__PrefabList);
		L.RegVar("LastParticleObjects", get_LastParticleObjects, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initFxDic(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			obj.initFxDic();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initPrefabDic(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			obj.initPrefabDic();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initAxDic(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			obj.initAxDic();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetScriptableData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			KeyFrameHandleScriptable arg0 = (KeyFrameHandleScriptable)ToLua.CheckObject<KeyFrameHandleScriptable>(L, 2);
			obj.SetScriptableData(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetScriptableData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			KeyFrameHandleScriptable o = obj.GetScriptableData();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUpdateScriptableData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			KeyFrameHandleScriptable o = obj.GetUpdateScriptableData();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEffData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			KeyFrameHandler.FxKeyValue[] o = obj.GetEffData();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEffData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			System.Collections.Generic.List<KeyFrameHandleScriptable.FxKeyValue> arg0 = (System.Collections.Generic.List<KeyFrameHandleScriptable.FxKeyValue>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<KeyFrameHandleScriptable.FxKeyValue>));
			obj.SetEffData(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEvents(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.AnimationEvent[] o = obj.GetEvents(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterLuaCallback(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.RegisterLuaCallback(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyLastFxObject(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			obj.DestroyLastFxObject();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearFx(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)ToLua.CheckObject<KeyFrameHandler>(L, 1);
			obj.ClearFx();
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

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_callback(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			LuaInterface.LuaFunction ret = obj.callback;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index callback on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fxRootArr(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			System.Collections.Generic.List<UnityEngine.Transform> ret = obj.fxRootArr;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index fxRootArr on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__fxList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandler.FxKeyValue[] ret = obj._fxList;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index _fxList on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_data(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandleScriptable ret = obj.data;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index data on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_updateData(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandleScriptable ret = obj.updateData;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index updateData on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NextFx(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			string ret = obj.NextFx;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index NextFx on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__PrefabList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandler.PrefabKeyValue[] ret = obj._PrefabList;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index _PrefabList on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LastParticleObjects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			System.Collections.Generic.List<UnityEngine.GameObject> ret = obj.LastParticleObjects;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index LastParticleObjects on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_callback(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.callback = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index callback on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fxRootArr(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			System.Collections.Generic.List<UnityEngine.Transform> arg0 = (System.Collections.Generic.List<UnityEngine.Transform>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Transform>));
			obj.fxRootArr = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index fxRootArr on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__fxList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandler.FxKeyValue[] arg0 = ToLua.CheckStructArray<KeyFrameHandler.FxKeyValue>(L, 2);
			obj._fxList = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index _fxList on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_data(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandleScriptable arg0 = (KeyFrameHandleScriptable)ToLua.CheckObject<KeyFrameHandleScriptable>(L, 2);
			obj.data = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index data on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_updateData(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandleScriptable arg0 = (KeyFrameHandleScriptable)ToLua.CheckObject<KeyFrameHandleScriptable>(L, 2);
			obj.updateData = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index updateData on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NextFx(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.NextFx = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index NextFx on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__PrefabList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			KeyFrameHandler obj = (KeyFrameHandler)o;
			KeyFrameHandler.PrefabKeyValue[] arg0 = ToLua.CheckStructArray<KeyFrameHandler.PrefabKeyValue>(L, 2);
			obj._PrefabList = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index _PrefabList on a nil value");
		}
	}
}

