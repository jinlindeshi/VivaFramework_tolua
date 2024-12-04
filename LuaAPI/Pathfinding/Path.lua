---@class Pathfinding.Path : object
---@field PipelineState Pathfinding.PathState
---@field CompleteState Pathfinding.PathCompleteState
---@field error bool
---@field errorLog string
---@field pathID ushort
---@field tagPenalties table
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
---@return float
function m:GetTotalLength() end
---@return System.Collections.IEnumerator
function m:WaitForPath() end
function m:BlockUntilCalculated() end
---@param tag int
---@return uint
function m:GetTagPenalty(tag) end
---@return bool
function m:IsDone() end
function m:Error() end
---@param o object
function m:Claim(o) end
---@param o object
---@param silent bool
function m:Release(o, silent) end
Pathfinding = {}
Pathfinding.Path = m
return m