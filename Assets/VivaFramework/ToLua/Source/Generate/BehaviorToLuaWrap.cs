﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class BehaviorToLuaWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(BehaviorToLua), typeof(BehaviorDesigner.Runtime.BehaviorTree));
		L.RegFunction("EditorInit", EditorInit);
		L.RegFunction("Pause", Pause);
		L.RegFunction("Resume", Resume);
		L.RegFunction("Stop", Stop);
		L.RegFunction("RegisterLuaFun", RegisterLuaFun);
		L.RegFunction("InvokeFun", InvokeFun);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EditorInit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
			obj.EditorInit();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pause(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
			obj.Pause();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Resume(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
			obj.Resume();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
			obj.Stop();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterLuaFun(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			obj.RegisterLuaFun(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InvokeFun(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 6)
			{
				BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				BehaviorDesigner.Runtime.Tasks.LuaAction arg1 = (BehaviorDesigner.Runtime.Tasks.LuaAction)ToLua.CheckObject<BehaviorDesigner.Runtime.Tasks.LuaAction>(L, 3);
				string arg2 = ToLua.CheckString(L, 4);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 5);
				bool arg4 = LuaDLL.luaL_checkboolean(L, 6);
				obj.InvokeFun(arg0, arg1, arg2, arg3, arg4);
				return 0;
			}
			else if (count == 7)
			{
				BehaviorToLua obj = (BehaviorToLua)ToLua.CheckObject<BehaviorToLua>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				BehaviorDesigner.Runtime.Tasks.LuaAction arg1 = (BehaviorDesigner.Runtime.Tasks.LuaAction)ToLua.CheckObject<BehaviorDesigner.Runtime.Tasks.LuaAction>(L, 3);
				string arg2 = ToLua.CheckString(L, 4);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 5);
				bool arg4 = LuaDLL.luaL_checkboolean(L, 6);
				bool arg5 = LuaDLL.luaL_checkboolean(L, 7);
				obj.InvokeFun(arg0, arg1, arg2, arg3, arg4, arg5);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: BehaviorToLua.InvokeFun");
			}
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

