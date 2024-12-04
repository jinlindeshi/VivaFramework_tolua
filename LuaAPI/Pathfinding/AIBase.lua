---@class Pathfinding.AIBase : Pathfinding.VersionedMonoBehaviour
---@field position UnityEngine.Vector3
---@field rotation UnityEngine.Quaternion
---@field destination UnityEngine.Vector3
---@field velocity UnityEngine.Vector3
---@field desiredVelocity UnityEngine.Vector3
---@field isStopped bool
---@field onSearchPath System.Action
---@field radius float
---@field height float
---@field repathRate float
---@field canSearch bool
---@field canMove bool
---@field maxSpeed float
---@field gravity UnityEngine.Vector3
---@field groundMask UnityEngine.LayerMask
---@field orientation Pathfinding.OrientationMode
---@field enableRotation bool
---@field movementPlane Pathfinding.Util.IMovementPlane
---@field updatePosition bool
---@field updateRotation bool
---@field ShapeGizmoColor UnityEngine.Color
local m = {}
function m:FindComponents() end
---@param newPosition UnityEngine.Vector3
---@param clearPath bool
function m:Teleport(newPosition, clearPath) end
---@param deltaTime float
---@param nextPosition UnityEngine.Vector3
---@param nextRotation UnityEngine.Quaternion
function m:MovementUpdate(deltaTime, nextPosition, nextRotation) end
function m:SearchPath() end
---@return UnityEngine.Vector3
function m:GetFeetPosition() end
---@param path Pathfinding.Path
function m:SetPath(path) end
---@param direction UnityEngine.Vector3
---@param maxDegrees float
---@return UnityEngine.Quaternion
function m:SimulateRotationTowards(direction, maxDegrees) end
---@param deltaPosition UnityEngine.Vector3
function m:Move(deltaPosition) end
---@param nextPosition UnityEngine.Vector3
---@param nextRotation UnityEngine.Quaternion
function m:FinalizeMovement(nextPosition, nextRotation) end
Pathfinding = {}
Pathfinding.AIBase = m
return m