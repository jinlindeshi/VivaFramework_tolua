---@class BehaviorDesigner.Runtime.Tasks.LuaAction : BehaviorDesigner.Runtime.Tasks.Action
---@field paramFloat BehaviorDesigner.Runtime.SharedFloat
---@field paramBool BehaviorDesigner.Runtime.SharedBool
local m = {}
---@param status BehaviorDesigner.Runtime.Tasks.TaskStatus
function m:SetUpdateStatus(status) end
---@return BehaviorDesigner.Runtime.Tasks.TaskStatus
function m:GetUpdateStatus() end
---@param status BehaviorDesigner.Runtime.Tasks.TaskStatus
---@return bool
function m:CheckUpdateStatus(status) end
function m:OnAwake() end
function m:OnStart() end
---@return BehaviorDesigner.Runtime.Tasks.TaskStatus
function m:OnUpdate() end
---@param paused bool
function m:OnPause(paused) end
function m:OnEnd() end
function m:OnReset() end
function m:OnBehaviorComplete() end
function m:OnBehaviorRestart() end
BehaviorDesigner = {}
BehaviorDesigner.Runtime = {}
BehaviorDesigner.Runtime.Tasks = {}
BehaviorDesigner.Runtime.Tasks.LuaAction = m
return m