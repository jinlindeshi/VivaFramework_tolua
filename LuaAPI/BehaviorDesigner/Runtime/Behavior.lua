---@class BehaviorDesigner.Runtime.Behavior : UnityEngine.MonoBehaviour
---@field StartWhenEnabled bool
---@field PauseWhenDisabled bool
---@field RestartWhenComplete bool
---@field LogTaskChanges bool
---@field Group int
---@field ResetValuesOnRestart bool
---@field ExternalBehavior BehaviorDesigner.Runtime.ExternalBehavior
---@field HasInheritedVariables bool
---@field BehaviorName string
---@field BehaviorDescription string
---@field ExecutionStatus BehaviorDesigner.Runtime.Tasks.TaskStatus
---@field HasEvent table
---@field gizmoViewMode BehaviorDesigner.Runtime.Behavior.GizmoViewMode
---@field showBehaviorDesignerGizmo bool
local m = {}
---@return BehaviorDesigner.Runtime.BehaviorSource
function m:GetBehaviorSource() end
---@param behaviorSource BehaviorDesigner.Runtime.BehaviorSource
function m:SetBehaviorSource(behaviorSource) end
---@return UnityEngine.Object
function m:GetObject() end
---@return string
function m:GetOwnerName() end
function m:Start() end
function m:EnableBehavior() end
---@overload fun(pause:bool):void
function m:DisableBehavior() end
function m:OnEnable() end
function m:OnDisable() end
function m:OnDestroy() end
---@param name string
---@return BehaviorDesigner.Runtime.SharedVariable
function m:GetVariable(name) end
---@param name string
---@param item BehaviorDesigner.Runtime.SharedVariable
function m:SetVariable(name, item) end
---@param name string
---@param value object
function m:SetVariableValue(name, value) end
---@return table
function m:GetAllVariables() end
function m:CheckForSerialization() end
---@param collision UnityEngine.Collision
function m:OnCollisionEnter(collision) end
---@param collision UnityEngine.Collision
function m:OnCollisionExit(collision) end
---@param other UnityEngine.Collider
function m:OnTriggerEnter(other) end
---@param other UnityEngine.Collider
function m:OnTriggerExit(other) end
---@param collision UnityEngine.Collision2D
function m:OnCollisionEnter2D(collision) end
---@param collision UnityEngine.Collision2D
function m:OnCollisionExit2D(collision) end
---@param other UnityEngine.Collider2D
function m:OnTriggerEnter2D(other) end
---@param other UnityEngine.Collider2D
function m:OnTriggerExit2D(other) end
---@param hit UnityEngine.ControllerColliderHit
function m:OnControllerColliderHit(hit) end
function m:OnDrawGizmos() end
function m:OnDrawGizmosSelected() end
---@param taskName string
---@return BehaviorDesigner.Runtime.Tasks.Task
function m:FindTaskWithName(taskName) end
---@param taskName string
---@return table
function m:FindTasksWithName(taskName) end
---@return table
function m:GetActiveTasks() end
---@overload fun(task:BehaviorDesigner.Runtime.Tasks.Task, methodName:string, value:object):UnityEngine.Coroutine
---@param task BehaviorDesigner.Runtime.Tasks.Task
---@param methodName string
---@return UnityEngine.Coroutine
function m:StartTaskCoroutine(task, methodName) end
---@param methodName string
function m:StopTaskCoroutine(methodName) end
function m:StopAllTaskCoroutines() end
---@param taskCoroutine BehaviorDesigner.Runtime.TaskCoroutine
---@param coroutineName string
function m:TaskCoroutineEnded(taskCoroutine, coroutineName) end
function m:OnBehaviorStarted() end
function m:OnBehaviorRestarted() end
function m:OnBehaviorEnded() end
---@param name string
---@param handler System.Action
function m:RegisterEvent(name, handler) end
---@param name string
function m:SendEvent(name) end
---@param name string
---@param handler System.Action
function m:UnregisterEvent(name, handler) end
function m:SaveResetValues() end
---@return string
function m:ToString() end
---@return BehaviorDesigner.Runtime.BehaviorManager
function m.CreateBehaviorManager() end
BehaviorDesigner = {}
BehaviorDesigner.Runtime = {}
BehaviorDesigner.Runtime.Behavior = m
return m