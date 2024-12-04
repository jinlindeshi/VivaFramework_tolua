---@class Pathfinding.AIPath : Pathfinding.AIBase
---@field remainingDistance float
---@field reachedDestination bool
---@field reachedEndOfPath bool
---@field hasPath bool
---@field pathPending bool
---@field steeringTarget UnityEngine.Vector3
---@field maxAcceleration float
---@field rotationSpeed float
---@field slowdownDistance float
---@field pickNextWaypointDist float
---@field endReachedDistance float
---@field alwaysDrawGizmos bool
---@field slowWhenNotFacingTarget bool
---@field whenCloseToDestination Pathfinding.CloseToDestinationMode
---@field constrainInsideGraph bool
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
local m = {}
---@param newPosition UnityEngine.Vector3
---@param clearPath bool
function m:Teleport(newPosition, clearPath) end
---@param buffer table
---@param stale bool
function m:GetRemainingPath(buffer, stale) end
function m:OnTargetReached() end
Pathfinding = {}
Pathfinding.AIPath = m
return m