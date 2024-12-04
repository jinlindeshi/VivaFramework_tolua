
using LuaInterface;
using Pathfinding;

public class AIPathToLua:AIPath
{
    private LuaFunction _moveEndCall;
    public void SetLuaCallBack(LuaFunction fun)
    {
        _moveEndCall = fun;
//        this.ClearPath();
    }

    //终止正在进行的移动
    public void StopMove()
    {
        ClearPath();
    }
    
    public override void OnTargetReached()
    {
        if (_moveEndCall != null)
        {
            _moveEndCall.Call();
        }
    }
}