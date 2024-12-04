using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace VivaFramework {
    public class LuaBehaviour : View {

        public string displayName = "Undefine";

        public string DisplayName
        {
            get { return displayName; }
        }

        private Dictionary<string, LuaFunction> buttons = new Dictionary<string, LuaFunction>();

        private Dictionary<string, LuaFunction> luaBehaviourFuncDic = new Dictionary<string, LuaFunction>();

        public void SetLuaBehaviourFunc(string[] funcNameArr, LuaFunction[] funcArr, string displayName)
        {
            for (int i = 0; i < funcArr.Length; i++)
            {
                luaBehaviourFuncDic.Add(funcNameArr[i], funcArr[i]);
            }
            this.displayName = displayName;
        }

        /// <summary>
        /// 对象开始绑定时调用
        /// </summary>
        public void OnAwake()
        {
            if (luaBehaviourFuncDic.ContainsKey("OnAwake"))
            {
                LuaCall(luaBehaviourFuncDic["OnAwake"]);
            }

        }


        /// <summary>
        /// 统一调用lua函数
        /// </summary>
        /// <param name="func">Func.</param>
        private void LuaCall(LuaFunction func)
        {
            if (func.GetLuaState() == null)
            {
                return;
            }
            func.BeginPCall();
            func.PCall();
            func.EndPCall();
        }

        protected void Awake() {
            Util.CallMethod(name, "Awake", gameObject);
        }

        protected void Start() {
            Util.CallMethod(name, "Start");
        }

        protected void OnClick() {
            Util.CallMethod(name, "OnClick");
        }

        protected void OnClickEvent(GameObject go) {
            Util.CallMethod(name, "OnClick", go);
        }

        /// <summary>
        /// 添加单击事件
        /// </summary>
        public void AddClick(GameObject go, LuaFunction luafunc) {
            if (go == null || luafunc == null) return;
            buttons.Add(go.name, luafunc);
            go.GetComponent<Button>().onClick.AddListener(
                delegate() {
                    luafunc.Call(go);
                }
            );
        }

        /// <summary>
        /// 删除单击事件
        /// </summary>
        /// <param name="go"></param>
        public void RemoveClick(GameObject go) {
            if (go == null) return;
            LuaFunction luafunc = null;
            if (buttons.TryGetValue(go.name, out luafunc)) {
                luafunc.Dispose();
                luafunc = null;
                buttons.Remove(go.name);
            }
        }

        /// <summary>
        /// 清除单击事件
        /// </summary>
        public void ClearClick() {
            foreach (var de in buttons) {
                if (de.Value != null) {
                    de.Value.Dispose();
                }
            }
            buttons.Clear();
        }

        //-----------------------------------------------------------------
        protected void OnDestroy() {
            ClearClick();
#if ASYNC_MODE
            string abName = name.ToLower().Replace("panel", "");
            ResManager.UnloadAssetBundle(abName + AppConst.ExtName);
#endif
            Util.ClearMemory();
            Debug.Log("~" + name + " was destroy!");
        }
    }
}