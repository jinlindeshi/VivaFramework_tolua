---@class AIPathToLua : Pathfinding.AIPath
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
---@param fun LuaInterface.LuaFunction
function m:SetLuaCallBack(fun) end
function m:StopMove() end
function m:OnTargetReached() end
AIPathToLua = m
return m