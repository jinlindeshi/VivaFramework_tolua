﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Prayer_PointerHandler3DWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Prayer.PointerHandler3D), typeof(Prayer.OperateHandler));
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("ENTER", get_ENTER, null);
		L.RegVar("EXIT", get_EXIT, null);
		L.RegVar("DOWN", get_DOWN, null);
		L.RegVar("UP", get_UP, null);
		L.RegVar("CLICK", get_CLICK, null);
		L.RegVar("DOUBLE_CLICK", get_DOUBLE_CLICK, null);
		L.RegVar("ENABLE", get_ENABLE, null);
		L.RegVar("DISABLE", get_DISABLE, null);
		L.EndClass();
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
	static int get_ENTER(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.ENTER);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EXIT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.EXIT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DOWN(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.DOWN);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UP(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.UP);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CLICK(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.CLICK);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DOUBLE_CLICK(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.DOUBLE_CLICK);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ENABLE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.ENABLE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DISABLE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Prayer.PointerHandler3D.DISABLE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

