---@class BehaviorDesigner.Runtime.BehaviorManager : UnityEngine.MonoBehaviour
---@field UpdateInterval BehaviorDesigner.Runtime.UpdateIntervalType
---@field UpdateIntervalSeconds float
---@field ExecutionsPerTick BehaviorDesigner.Runtime.BehaviorManager.ExecutionsPerTickType
---@field MaxTaskExecutionsPerTick int
---@field OnEnableBehavior BehaviorDesigner.Runtime.BehaviorManager.BehaviorManagerHandler
---@field OnTaskBreakpoint BehaviorDesigner.Runtime.BehaviorManager.BehaviorManagerHandler
---@field BehaviorTrees table
---@field BreakpointTree BehaviorDesigner.Runtime.Behavior
---@field Dirty bool
---@field instance BehaviorDesigner.Runtime.BehaviorManager
---@field onEnableBehavior BehaviorDesigner.Runtime.BehaviorManager.BehaviorManagerHandler
---@field onTaskBreakpoint BehaviorDesigner.Runtime.BehaviorManager.BehaviorManagerHandler
local m = {}
function m:Awake() end
function m:OnDestroy() end
function m:OnApplicationQuit() end
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:EnableBehavior(behavior) end
---@overload fun(behavior:BehaviorDesigner.Runtime.Behavior, paused:bool):void
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:DisableBehavior(behavior) end
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:DestroyBehavior(behavior) end
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:RestartBehavior(behavior) end
---@param behavior BehaviorDesigner.Runtime.Behavior
---@return bool
function m:IsBehaviorEnabled(behavior) end
function m:Update() end
function m:LateUpdate() end
function m:FixedUpdate() end
---@overload fun(behavior:BehaviorDesigner.Runtime.Behavior):void
function m:Tick() end
---@overload fun(behavior:BehaviorDesigner.Runtime.Behavior, task:BehaviorDesigner.Runtime.Tasks.Task, interruptionTask:BehaviorDesigner.Runtime.Tasks.Task):void
---@param behavior BehaviorDesigner.Runtime.Behavior
---@param task BehaviorDesigner.Runtime.Tasks.Task
function m:Interrupt(behavior, task) end
---@param behaviorTree BehaviorDesigner.Runtime.BehaviorManager.BehaviorTree
---@param taskIndex int
function m:StopThirdPartyTask(behaviorTree, taskIndex) end
---@param task BehaviorDesigner.Runtime.Tasks.Task
function m:RemoveActiveThirdPartyTask(task) end
---@param behavior BehaviorDesigner.Runtime.Behavior
---@return table
function m:GetActiveTasks(behavior) end
---@param collision UnityEngine.Collision
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnCollisionEnter(collision, behavior) end
---@param collision UnityEngine.Collision
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnCollisionExit(collision, behavior) end
---@param other UnityEngine.Collider
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnTriggerEnter(other, behavior) end
---@param other UnityEngine.Collider
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnTriggerExit(other, behavior) end
---@param collision UnityEngine.Collision2D
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnCollisionEnter2D(collision, behavior) end
---@param collision UnityEngine.Collision2D
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnCollisionExit2D(collision, behavior) end
---@param other UnityEngine.Collider2D
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnTriggerEnter2D(other, behavior) end
---@param other UnityEngine.Collider2D
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnTriggerExit2D(other, behavior) end
---@param hit UnityEngine.ControllerColliderHit
---@param behavior BehaviorDesigner.Runtime.Behavior
function m:BehaviorOnControllerColliderHit(hit, behavior) end
---@param objectKey object
---@param task BehaviorDesigner.Runtime.Tasks.Task
---@param objectType BehaviorDesigner.Runtime.BehaviorManager.ThirdPartyObjectType
---@return bool
function m:MapObjectToTask(objectKey, task, objectType) end
---@param objectKey object
---@return BehaviorDesigner.Runtime.Tasks.Task
function m:TaskForObject(objectKey) end
---@param behavior BehaviorDesigner.Runtime.Behavior
---@return table
function m:GetTaskList(behavior) end
BehaviorDesigner = {}
BehaviorDesigner.Runtime = {}
BehaviorDesigner.Runtime.BehaviorManager = m
return m