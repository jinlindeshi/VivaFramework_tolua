---@class Pathfinding.NavGraph : object
---@field active AstarPath
---@field guid Pathfinding.Util.Guid
---@field initialPenalty uint
---@field open bool
---@field graphIndex uint
---@field name string
---@field drawGizmos bool
---@field infoScreenOpen bool
local m = {}
---@return int
function m:CountNodes() end
---@overload fun(action:System.Action):void
---@param action System.Func
function m:GetNodes(action) end
---@param deltaMatrix UnityEngine.Matrix4x4
function m:RelocateNodes(deltaMatrix) end
---@overload fun(position:UnityEngine.Vector3, constraint:Pathfinding.NNConstraint):Pathfinding.NNInfoInternal
---@overload fun(position:UnityEngine.Vector3, constraint:Pathfinding.NNConstraint, hint:Pathfinding.GraphNode):Pathfinding.NNInfoInternal
---@param position UnityEngine.Vector3
---@return Pathfinding.NNInfoInternal
function m:GetNearest(position) end
---@param position UnityEngine.Vector3
---@param constraint Pathfinding.NNConstraint
---@return Pathfinding.NNInfoInternal
function m:GetNearestForce(position, constraint) end
function m:Scan() end
---@param gizmos Pathfinding.Util.RetainedGizmos
---@param drawNodes bool
function m:OnDrawGizmos(gizmos, drawNodes) end
Pathfinding = {}
Pathfinding.NavGraph = m
return m