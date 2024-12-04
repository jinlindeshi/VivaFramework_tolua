---@class Pathfinding.GridNodeBase : Pathfinding.GraphNode
---@field NodeInGridIndex int
---@field XCoordinateInGrid int
---@field ZCoordinateInGrid int
---@field WalkableErosion bool
---@field TmpWalkable bool
---@field HasConnectionsToAllEightNeighbours bool
---@field connections table
---@field position Pathfinding.Int3
local m = {}
---@return float
function m:SurfaceArea() end
---@return UnityEngine.Vector3
function m:RandomPointOnSurface() end
---@return int
function m:GetGizmoHashCode() end
---@param direction int
---@return Pathfinding.GridNodeBase
function m:GetNeighbourAlongDirection(direction) end
---@param node Pathfinding.GraphNode
---@return bool
function m:ContainsConnection(node) end
---@param alsoReverse bool
function m:ClearCustomConnections(alsoReverse) end
---@param alsoReverse bool
function m:ClearConnections(alsoReverse) end
---@param action System.Action
function m:GetConnections(action) end
---@param path Pathfinding.Path
---@param pathNode Pathfinding.PathNode
---@param handler Pathfinding.PathHandler
function m:UpdateRecursiveG(path, pathNode, handler) end
---@param path Pathfinding.Path
---@param pathNode Pathfinding.PathNode
---@param handler Pathfinding.PathHandler
function m:Open(path, pathNode, handler) end
---@param node Pathfinding.GraphNode
---@param cost uint
function m:AddConnection(node, cost) end
---@param node Pathfinding.GraphNode
function m:RemoveConnection(node) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:SerializeReferences(ctx) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:DeserializeReferences(ctx) end
Pathfinding = {}
Pathfinding.GridNodeBase = m
return m