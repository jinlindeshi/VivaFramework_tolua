using UnityEngine;
using System.Collections;
using LuaInterface;

namespace Prayer
{
    public class ManagerHandler : MonoBehaviour
    {
        public LuaFunction checkCall;
        private void CallLua(string funName)
        {
            if (checkCall == null)
            {
                return;
            }
            checkCall.Call(funName);
        }
        // Use this for initialization
        void Start()
        {
            CallLua("Start");
        }

        // Update is called once per frame
        void Update()
        {
            CallLua("Update");
            TouchEvent._Update();
        }

        void FixedUpdate()
        {
            CallLua("FixedUpdate");
        }

        void LateUpdate()
        {
            CallLua("LateUpdate");
        }

        void OnDestroy()
        {
            CallLua("OnDestroy");
        }
        
        void OnRenderObject()
        {
            CallLua("OnRenderObject");
        }
    }
}
