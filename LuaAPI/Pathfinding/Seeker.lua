---@class Pathfinding.Seeker : Pathfinding.VersionedMonoBehaviour
---@field drawGizmos bool
---@field detailedGizmos bool
---@field startEndModifier Pathfinding.StartEndModifier
---@field traversableTags int
---@field tagPenalties table
---@field graphMask Pathfinding.GraphMask
---@field pathCallback Pathfinding.OnPathDelegate
---@field preProcessPath Pathfinding.OnPathDelegate
---@field postProcessPath Pathfinding.OnPathDelegate
local m = {}
---@return Pathfinding.Path
function m:GetCurrentPath() end
---@param pool bool
function m:CancelCurrentPathRequest(pool) end
function m:OnDestroy() end
---@param modifier Pathfinding.IPathModifier
function m:RegisterModifier(modifier) end
---@param modifier Pathfinding.IPathModifier
function m:DeregisterModifier(modifier) end
---@param path Pathfinding.Path
function m:PostProcess(path) end
---@param pass Pathfinding.Seeker.ModifierPass
---@param path Pathfinding.Path
function m:RunModifiers(pass, path) end
---@return bool
function m:IsDone() end
---@overload fun(start:UnityEngine.Vector3, $end:UnityEngine.Vector3, callback:Pathfinding.OnPathDelegate):Pathfinding.Path
---@overload fun(start:UnityEngine.Vector3, $end:UnityEngine.Vector3, callback:Pathfinding.OnPathDelegate, graphMask:Pathfinding.GraphMask):Pathfinding.Path
---@overload fun(p:Pathfinding.Path, callback:Pathfinding.OnPathDelegate):Pathfinding.Path
---@overload fun(p:Pathfinding.Path, callback:Pathfinding.OnPathDelegate, graphMask:Pathfinding.GraphMask):Pathfinding.Path
---@param start UnityEngine.Vector3
---@param $end UnityEngine.Vector3
---@return Pathfinding.Path
function m:StartPath(start, $end) end
function m:OnDrawGizmos() end
Pathfinding = {}
Pathfinding.Seeker = m
return m