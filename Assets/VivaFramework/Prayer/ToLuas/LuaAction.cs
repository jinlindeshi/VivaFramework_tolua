using BehaviorDesigner.Runtime.Tasks.Basic.UnityString;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    //执行Lua回调的Action
    public class LuaAction:Action
    {
        public SharedFloat paramFloat;
        public SharedBool paramBool;
        private BehaviorToLua btl;

        private TaskStatus _updateStatus;
        private bool inPause = false; //记录是否在暂停中
        public void SetUpdateStatus(TaskStatus status)
        {
            _updateStatus = status;
        }
        public TaskStatus GetUpdateStatus()
        {
            return　_updateStatus;
        }
        public bool CheckUpdateStatus(TaskStatus status)
        {
            return _updateStatus == status;
        }

        private void InvokeFun(string funName, bool paused = false)
        {
            btl.InvokeFun(FriendlyName, this, funName, paramFloat.Value, paramBool.Value, paused);
        }

        public override void OnAwake()
        {
            btl = this.gameObject.GetComponent<BehaviorToLua>();
            InvokeFun("OnAwake");
        }

        public override void OnStart()
        {
            base.OnStart();
            InvokeFun("OnStart");
        }

        public override TaskStatus OnUpdate()
        {
//            Debug.Log("LuaAction - OnUpdate");
            InvokeFun("OnUpdate");
            return _updateStatus;
        }
        
         public override void OnPause(bool paused)
        {
            if (paused && _updateStatus != TaskStatus.Inactive)
            {
                inPause = true;
                InvokeFun("OnPause", paused);
            }
            else if (paused == false && inPause)
            {
                inPause = false;
                InvokeFun("OnPause", paused);
            }
        }
        
        public override void OnEnd()
        {
            InvokeFun("OnEnd");
        }

        public override void OnReset()
        {
//            Debug.Log("LuaAction - OnReset");
            InvokeFun("OnReset");
        }

        public override void OnBehaviorComplete()
        {
            InvokeFun("OnBehaviorComplete");
        }

        public override void OnBehaviorRestart()
        {
            InvokeFun("OnBehaviorRestart");
        }
    }
}