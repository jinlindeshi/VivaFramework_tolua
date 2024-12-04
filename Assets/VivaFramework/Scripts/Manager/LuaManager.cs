using UnityEngine;
using System.Collections;
using System.Net.Security;
using LuaInterface;

namespace VivaFramework {
    public class LuaManager : Manager {
        private LuaState lua;
        private LuaLoader loader;

        // Use this for initialization
        void Awake() {
            loader = new LuaLoader();
            lua = new LuaState();
            this.OpenLibs();
            lua.LuaSetTop(0);

            LuaBinder.Bind(lua);
            DelegateFactory.Init();
            LuaCoroutine.Register(lua, this);
        }

        public void InitStart() {
            if (AppConst.UseBundle == true)
            {
//                print("InitStart " + LuaConst.luaDir + " - " + LuaConst.toluaDir);
//                lua.AddSearchPath(LuaConst.luaDir);
                InitLuaBundle();
            }
            else
            {
                lua.AddSearchPath(LuaConst.luaDir);
                lua.AddSearchPath(LuaConst.toluaDir);
            }
            lua.Start();    //启动LUAVM
            lua.DoFile("Main.lua");
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        protected void OpenCJson() {
            lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.LuaSetField(-2, "cjson");

            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.LuaSetField(-2, "cjson.safe");
        }

        
        /// <summary>
        /// 初始化加载第三方库
        /// </summary>
        void OpenLibs() {
            lua.OpenLibs(LuaDLL.luaopen_pb);      
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            lua.OpenLibs(LuaDLL.luaopen_socket_core);

            this.OpenCJson();
        }

        /// <summary>
        /// 初始化LuaBundle
        /// </summary>
        void InitLuaBundle() {
            if (loader.beZip) {
//                Debug.LogWarning("InitLuaBundle");
                loader.AddBundle("lua/lua.unity3d");
            }
        }

        public void DoFile(string filename) {
            lua.DoFile(filename);
        }

        // Update is called once per frame
        public object[] CallFunction(string funcName, params object[] args) {
            LuaFunction func = lua.GetFunction(funcName);
            if (func != null) {
                return func.LazyCall(args);
            }
            return null;
        }

        public void LuaGC() {
            lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public void Close() {
            if (lua != null)
            {
                lua.Dispose();
                lua = null;  
            }
            loader = null;
        }
    }
}