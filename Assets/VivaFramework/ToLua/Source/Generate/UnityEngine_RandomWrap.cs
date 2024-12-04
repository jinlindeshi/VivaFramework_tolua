﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_RandomWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Random");
		L.RegFunction("InitState", InitState);
		L.RegFunction("Range", Range);
		L.RegFunction("ColorHSV", ColorHSV);
		L.RegVar("state", get_state, set_state);
		L.RegVar("value", get_value, null);
		L.RegVar("insideUnitSphere", get_insideUnitSphere, null);
		L.RegVar("insideUnitCircle", get_insideUnitCircle, null);
		L.RegVar("onUnitSphere", get_onUnitSphere, null);
		L.RegVar("rotation", get_rotation, null);
		L.RegVar("rotationUniform", get_rotationUniform, null);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitState(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			UnityEngine.Random.InitState(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Range(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 1);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
			float o = UnityEngine.Random.Range(arg0, arg1);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ColorHSV(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				UnityEngine.Color o = UnityEngine.Random.ColorHSV();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2)
			{
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 1);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Color o = UnityEngine.Random.ColorHSV(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4)
			{
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 1);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 4);
				UnityEngine.Color o = UnityEngine.Random.ColorHSV(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 6)
			{
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 1);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 4);
				float arg4 = (float)LuaDLL.luaL_checknumber(L, 5);
				float arg5 = (float)LuaDLL.luaL_checknumber(L, 6);
				UnityEngine.Color o = UnityEngine.Random.ColorHSV(arg0, arg1, arg2, arg3, arg4, arg5);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 8)
			{
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 1);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 4);
				float arg4 = (float)LuaDLL.luaL_checknumber(L, 5);
				float arg5 = (float)LuaDLL.luaL_checknumber(L, 6);
				float arg6 = (float)LuaDLL.luaL_checknumber(L, 7);
				float arg7 = (float)LuaDLL.luaL_checknumber(L, 8);
				UnityEngine.Color o = UnityEngine.Random.ColorHSV(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Random.ColorHSV");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_state(IntPtr L)
	{
		try
		{
			ToLua.PushValue(L, UnityEngine.Random.state);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_value(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushnumber(L, UnityEngine.Random.value);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_insideUnitSphere(IntPtr L)
	{
		try
		{
			ToLua.Push(L, UnityEngine.Random.insideUnitSphere);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_insideUnitCircle(IntPtr L)
	{
		try
		{
			ToLua.Push(L, UnityEngine.Random.insideUnitCircle);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onUnitSphere(IntPtr L)
	{
		try
		{
			ToLua.Push(L, UnityEngine.Random.onUnitSphere);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotation(IntPtr L)
	{
		try
		{
			ToLua.Push(L, UnityEngine.Random.rotation);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotationUniform(IntPtr L)
	{
		try
		{
			ToLua.Push(L, UnityEngine.Random.rotationUniform);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_state(IntPtr L)
	{
		try
		{
			UnityEngine.Random.State arg0 = StackTraits<UnityEngine.Random.State>.Check(L, 2);
			UnityEngine.Random.state = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

