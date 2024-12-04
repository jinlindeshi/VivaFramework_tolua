---@class Pathfinding.GridNode : Pathfinding.GridNodeBase
---@field HasConnectionsToAllEightNeighbours bool
---@field EdgeNode bool
---@field connections table
---@field position Pathfinding.Int3
local m = {}
---@param graphIndex uint
---@return Pathfinding.GridGraph
function m.GetGridGraph(graphIndex) end
---@param graphIndex int
---@param graph Pathfinding.GridGraph
function m.SetGridGraph(graphIndex, graph) end
---@param dir int
---@return bool
function m:HasConnectionInDirection(dir) end
---@param dir int
---@param value bool
function m:SetConnectionInternal(dir, value) end
---@param connections int
function m:SetAllConnectionInternal(connections) end
function m:ResetConnectionsInternal() end
---@param direction int
---@return Pathfinding.GridNodeBase
function m:GetNeighbourAlongDirection(direction) end
---@param alsoReverse bool
function m:ClearConnections(alsoReverse) end
---@param action System.Action
function m:GetConnections(action) end
---@param p UnityEngine.Vector3
---@return UnityEngine.Vector3
function m:ClosestPointOnNode(p) end
---@param other Pathfinding.GraphNode
---@param left table
---@param right table
---@param backwards bool
---@return bool
function m:GetPortal(other, left, right, backwards) end
---@param path Pathfinding.Path
---@param pathNode Pathfinding.PathNode
---@param handler Pathfinding.PathHandler
function m:UpdateRecursiveG(path, pathNode, handler) end
---@param path Pathfinding.Path
---@param pathNode Pathfinding.PathNode
---@param handler Pathfinding.PathHandler
function m:Open(path, pathNode, handler) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:SerializeNode(ctx) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:DeserializeNode(ctx) end
Pathfinding = {}
Pathfinding.GridNode = m
return m