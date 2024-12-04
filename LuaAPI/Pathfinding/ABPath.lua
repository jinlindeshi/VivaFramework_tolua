---@class Pathfinding.ABPath : Pathfinding.Path
---@field startNode Pathfinding.GraphNode
---@field endNode Pathfinding.GraphNode
---@field originalStartPoint UnityEngine.Vector3
---@field originalEndPoint UnityEngine.Vector3
---@field startPoint UnityEngine.Vector3
---@field endPoint UnityEngine.Vector3
---@field startIntPoint Pathfinding.Int3
---@field calculatePartial bool
---@field callback Pathfinding.OnPathDelegate
---@field immediateCallback Pathfinding.OnPathDelegate
---@field traversalProvider Pathfinding.ITraversalProvider
---@field path table
---@field vectorPath table
---@field duration float
---@field nnConstraint Pathfinding.NNConstraint
---@field heuristic Pathfinding.Heuristic
---@field heuristicScale float
---@field enabledTags int
local m = {}
---@param start UnityEngine.Vector3
---@param $end UnityEngine.Vector3
---@param callback Pathfinding.OnPathDelegate
---@return Pathfinding.ABPath
function m.Construct(start, $end, callback) end
---@param vectorPath table
---@param nodePath table
---@return Pathfinding.ABPath
function m.FakePath(vectorPath, nodePath) end
Pathfinding = {}
Pathfinding.ABPath = m
return m