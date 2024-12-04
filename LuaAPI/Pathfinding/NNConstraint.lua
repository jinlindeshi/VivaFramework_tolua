---@class Pathfinding.NNConstraint : object
---@field Default Pathfinding.NNConstraint
---@field None Pathfinding.NNConstraint
---@field graphMask Pathfinding.GraphMask
---@field constrainArea bool
---@field area int
---@field constrainWalkability bool
---@field walkable bool
---@field distanceXZ bool
---@field constrainTags bool
---@field tags int
---@field constrainDistance bool
local m = {}
---@param graphIndex int
---@param graph Pathfinding.NavGraph
---@return bool
function m:SuitableGraph(graphIndex, graph) end
---@param node Pathfinding.GraphNode
---@return bool
function m:Suitable(node) end
Pathfinding = {}
Pathfinding.NNConstraint = m
return m