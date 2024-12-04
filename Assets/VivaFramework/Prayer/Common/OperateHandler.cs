using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using LuaInterface;

namespace Prayer
{
    public class OperateHandler : MonoBehaviour
    {
        protected Dictionary<string, List<LuaFunction>> funDic = new Dictionary<string, List<LuaFunction>>();

        public LuaFunction AddCall(string type, LuaFunction call)
        {
            if(funDic.ContainsKey(type) == false)
            {
                funDic[type] = new List<LuaFunction>();
            }
            funDic[type].Add(call);

            return call;
        }

        public void RemoveCall(string type, LuaFunction call)
        {
            var removeIndex = -1;
            if (funDic.ContainsKey(type) == false) return;
            List<LuaFunction> list = funDic[type];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == call)
                {
                    removeIndex = i;
                    break;
                }
            }
            if (removeIndex >= 0)
            {
                funDic[type].RemoveAt(removeIndex);
            }
        }

        protected void TakeCall(string type, PointerEventData eventData)
        {
            if (funDic.ContainsKey(type) == false || funDic[type] == null)
            {
                return;
            }

            List<LuaFunction> list = new List<LuaFunction>();
            for (int i = 0; i < funDic[type].Count; i++)
            {
                list.Add(funDic[type][i]);
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetLuaState() != null)
                {
                    list[i].Call(eventData);
                }
            }
        }

        protected void TakeCall(string type)
        {
            TakeCall(type, null);
        }
    }
}