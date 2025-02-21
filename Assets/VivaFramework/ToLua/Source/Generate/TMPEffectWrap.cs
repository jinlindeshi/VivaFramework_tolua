﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class TMPEffectWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(TMPEffect), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("ResetOrgVector", ResetOrgVector);
		L.RegFunction("PlayWaveEffect", PlayWaveEffect);
		L.RegFunction("PlayFadingType", PlayFadingType);
		L.RegFunction("CompleteFadingType", CompleteFadingType);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("fadingSequence", get_fadingSequence, set_fadingSequence);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetOrgVector(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			TMPEffect obj = (TMPEffect)ToLua.CheckObject<TMPEffect>(L, 1);
			obj.ResetOrgVector();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayWaveEffect(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				TMPEffect obj = (TMPEffect)ToLua.CheckObject<TMPEffect>(L, 1);
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
				obj.PlayWaveEffect(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				TMPEffect obj = (TMPEffect)ToLua.CheckObject<TMPEffect>(L, 1);
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
				obj.PlayWaveEffect(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: TMPEffect.PlayWaveEffect");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayFadingType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			TMPEffect obj = (TMPEffect)ToLua.CheckObject<TMPEffect>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
			obj.PlayFadingType(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompleteFadingType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			TMPEffect obj = (TMPEffect)ToLua.CheckObject<TMPEffect>(L, 1);
			obj.CompleteFadingType();
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
	static int get_fadingSequence(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			TMPEffect obj = (TMPEffect)o;
			DG.Tweening.Sequence ret = obj.fadingSequence;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index fadingSequence on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fadingSequence(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			TMPEffect obj = (TMPEffect)o;
			DG.Tweening.Sequence arg0 = (DG.Tweening.Sequence)ToLua.CheckObject(L, 2, typeof(DG.Tweening.Sequence));
			obj.fadingSequence = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index fadingSequence on a nil value");
		}
	}
}

