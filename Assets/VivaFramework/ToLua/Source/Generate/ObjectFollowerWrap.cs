﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ObjectFollowerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ObjectFollower), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("SetTarget", SetTarget);
		L.RegFunction("AddTarget", AddTarget);
		L.RegFunction("ClearTargets", ClearTargets);
		L.RegFunction("RemoveTarget", RemoveTarget);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("targets", get_targets, set_targets);
		L.RegVar("centerOffset", get_centerOffset, set_centerOffset);
		L.RegVar("defaultObject", get_defaultObject, set_defaultObject);
		L.RegVar("isGUI", get_isGUI, set_isGUI);
		L.RegVar("stageCamera", get_stageCamera, set_stageCamera);
		L.RegVar("subViewport", get_subViewport, set_subViewport);
		L.RegVar("syncRotateType", get_syncRotateType, set_syncRotateType);
		L.RegVar("targetEulersOffset", get_targetEulersOffset, set_targetEulersOffset);
		L.RegVar("selfEulersOffset", get_selfEulersOffset, set_selfEulersOffset);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTarget(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ObjectFollower obj = (ObjectFollower)ToLua.CheckObject<ObjectFollower>(L, 1);
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.SetTarget(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddTarget(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				ObjectFollower obj = (ObjectFollower)ToLua.CheckObject<ObjectFollower>(L, 1);
				UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
				obj.AddTarget(arg0);
				return 0;
			}
			else if (count == 3)
			{
				ObjectFollower obj = (ObjectFollower)ToLua.CheckObject<ObjectFollower>(L, 1);
				UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
				bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
				obj.AddTarget(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				ObjectFollower obj = (ObjectFollower)ToLua.CheckObject<ObjectFollower>(L, 1);
				UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
				bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
				ObjectFollower.SyncRotationType arg2 = (ObjectFollower.SyncRotationType)ToLua.CheckObject(L, 4, typeof(ObjectFollower.SyncRotationType));
				obj.AddTarget(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ObjectFollower.AddTarget");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearTargets(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ObjectFollower obj = (ObjectFollower)ToLua.CheckObject<ObjectFollower>(L, 1);
			obj.ClearTargets();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveTarget(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ObjectFollower obj = (ObjectFollower)ToLua.CheckObject<ObjectFollower>(L, 1);
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.RemoveTarget(arg0);
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
	static int get_targets(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			System.Collections.Generic.Dictionary<int,UnityEngine.Transform> ret = obj.targets;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index targets on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_centerOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Vector3 ret = obj.centerOffset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index centerOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Transform ret = obj.defaultObject;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index defaultObject on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isGUI(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			bool ret = obj.isGUI;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isGUI on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stageCamera(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Camera ret = obj.stageCamera;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index stageCamera on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_subViewport(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.RectTransform ret = obj.subViewport;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index subViewport on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_syncRotateType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			ObjectFollower.SyncRotationType ret = obj.syncRotateType;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index syncRotateType on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetEulersOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Vector3 ret = obj.targetEulersOffset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index targetEulersOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_selfEulersOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Vector3 ret = obj.selfEulersOffset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index selfEulersOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targets(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			System.Collections.Generic.Dictionary<int,UnityEngine.Transform> arg0 = (System.Collections.Generic.Dictionary<int,UnityEngine.Transform>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,UnityEngine.Transform>));
			obj.targets = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index targets on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_centerOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.centerOffset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index centerOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.defaultObject = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index defaultObject on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isGUI(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.isGUI = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isGUI on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stageCamera(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Camera arg0 = (UnityEngine.Camera)ToLua.CheckObject(L, 2, typeof(UnityEngine.Camera));
			obj.stageCamera = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index stageCamera on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_subViewport(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.RectTransform arg0 = (UnityEngine.RectTransform)ToLua.CheckObject(L, 2, typeof(UnityEngine.RectTransform));
			obj.subViewport = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index subViewport on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_syncRotateType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			ObjectFollower.SyncRotationType arg0 = (ObjectFollower.SyncRotationType)ToLua.CheckObject(L, 2, typeof(ObjectFollower.SyncRotationType));
			obj.syncRotateType = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index syncRotateType on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetEulersOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.targetEulersOffset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index targetEulersOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_selfEulersOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectFollower obj = (ObjectFollower)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.selfEulersOffset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index selfEulersOffset on a nil value");
		}
	}
}

