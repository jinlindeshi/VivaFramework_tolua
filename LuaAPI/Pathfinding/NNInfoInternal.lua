---@class Pathfinding.NNInfoInternal
---@field node Pathfinding.GraphNode
---@field constrainedNode Pathfinding.GraphNode
---@field clampedPosition UnityEngine.Vector3
---@field constClampedPosition UnityEngine.Vector3
local m = {}
function m:UpdateInfo() end
Pathfinding = {}
Pathfinding.NNInfoInternal = m
return m