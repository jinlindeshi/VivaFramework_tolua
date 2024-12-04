---@class BehaviorToLua : BehaviorDesigner.Runtime.BehaviorTree
---@field gizmoViewMode BehaviorDesigner.Runtime.Behavior.GizmoViewMode
---@field showBehaviorDesignerGizmo bool
local m = {}
function m:EditorInit() end
function m:Pause() end
function m:Resume() end
function m:Stop() end
---@param name string
---@param fun LuaInterface.LuaFunction
function m:RegisterLuaFun(name, fun) end
---@param name string
---@param action BehaviorDesigner.Runtime.Tasks.LuaAction
---@param statusFunName string
---@param paramFloat float
---@param paramBool bool
---@param paused bool
function m:InvokeFun(name, action, statusFunName, paramFloat, paramBool, paused) end
BehaviorToLua = m
return m