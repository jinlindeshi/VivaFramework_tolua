---@class SeekerToLua : Pathfinding.Seeker
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
function m:EditorInit() end
---@param i3 Pathfinding.Int3
---@return UnityEngine.Vector3
function m.IntToVector(i3) end
---@param goal UnityEngine.Vector3
---@param callBack LuaInterface.LuaFunction
function m:TakeMove(goal, callBack) end
SeekerToLua = m
return m