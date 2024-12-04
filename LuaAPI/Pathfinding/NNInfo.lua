---@class Pathfinding.NNInfo
---@field node Pathfinding.GraphNode
---@field position UnityEngine.Vector3
local m = {}
---@overload fun(ob:Pathfinding.NNInfo):Pathfinding.GraphNode
---@param ob Pathfinding.NNInfo
---@return UnityEngine.Vector3
function m.op_Explicit(ob) end
Pathfinding = {}
Pathfinding.NNInfo = m
return m