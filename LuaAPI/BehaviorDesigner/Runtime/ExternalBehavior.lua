---@class BehaviorDesigner.Runtime.ExternalBehavior : UnityEngine.ScriptableObject
---@field BehaviorSource BehaviorDesigner.Runtime.BehaviorSource
local m = {}
---@return BehaviorDesigner.Runtime.BehaviorSource
function m:GetBehaviorSource() end
---@param behaviorSource BehaviorDesigner.Runtime.BehaviorSource
function m:SetBehaviorSource(behaviorSource) end
---@return UnityEngine.Object
function m:GetObject() end
---@return string
function m:GetOwnerName() end
---@param name string
---@return BehaviorDesigner.Runtime.SharedVariable
function m:GetVariable(name) end
---@param name string
---@param item BehaviorDesigner.Runtime.SharedVariable
function m:SetVariable(name, item) end
---@param name string
---@param value object
function m:SetVariableValue(name, value) end
---@param taskName string
---@return BehaviorDesigner.Runtime.Tasks.Task
function m:FindTaskWithName(taskName) end
---@param taskName string
---@return table
function m:FindTasksWithName(taskName) end
BehaviorDesigner = {}
BehaviorDesigner.Runtime = {}
BehaviorDesigner.Runtime.ExternalBehavior = m
return m