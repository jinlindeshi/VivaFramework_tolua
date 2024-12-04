---@class BehaviorDesigner.Runtime.Tasks.Task : object
---@field GameObject UnityEngine.GameObject
---@field Transform UnityEngine.Transform
---@field NodeData BehaviorDesigner.Runtime.NodeData
---@field Owner BehaviorDesigner.Runtime.Behavior
---@field ID int
---@field FriendlyName string
---@field IsInstant bool
---@field ReferenceID int
---@field Disabled bool
local m = {}
function m:OnAwake() end
function m:OnStart() end
---@return BehaviorDesigner.Runtime.Tasks.TaskStatus
function m:OnUpdate() end
function m:OnLateUpdate() end
function m:OnFixedUpdate() end
function m:OnEnd() end
---@param paused bool
function m:OnPause(paused) end
---@return float
function m:GetPriority() end
---@return float
function m:GetUtility() end
function m:OnBehaviorRestart() end
function m:OnBehaviorComplete() end
function m:OnReset() end
function m:OnDrawGizmos() end
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
BehaviorDesigner = {}
BehaviorDesigner.Runtime = {}
BehaviorDesigner.Runtime.Tasks = {}
BehaviorDesigner.Runtime.Tasks.Task = m
return m