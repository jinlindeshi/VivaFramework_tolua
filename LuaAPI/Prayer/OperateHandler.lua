---@class Prayer.OperateHandler : UnityEngine.MonoBehaviour
local m = {}
---@param type string
---@param call LuaInterface.LuaFunction
---@return LuaInterface.LuaFunction
function m:AddCall(type, call) end
---@param type string
---@param call LuaInterface.LuaFunction
function m:RemoveCall(type, call) end
Prayer = {}
Prayer.OperateHandler = m
return m