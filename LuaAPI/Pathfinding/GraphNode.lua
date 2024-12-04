---@class Pathfinding.GraphNode : object
---@field Graph Pathfinding.NavGraph
---@field Destroyed bool
---@field NodeIndex int
---@field Flags uint
---@field Penalty uint
---@field Walkable bool
---@field Area uint
---@field GraphIndex uint
---@field Tag uint
---@field position Pathfinding.Int3
---@field MaxHierarchicalNodeIndex uint
---@field MaxGraphIndex uint
local m = {}
function m:Destroy() end
function m:SetConnectivityDirty() end
---@param path Pathfinding.Path
---@param pathNode Pathfinding.PathNode
---@param handler Pathfinding.PathHandler
function m:UpdateRecursiveG(path, pathNode, handler) end
---@param action System.Action
function m:GetConnections(action) end
---@param node Pathfinding.GraphNode
---@param cost uint
function m:AddConnection(node, cost) end
---@param node Pathfinding.GraphNode
function m:RemoveConnection(node) end
---@param alsoReverse bool
function m:ClearConnections(alsoReverse) end
---@param node Pathfinding.GraphNode
---@return bool
function m:ContainsConnection(node) end
---@param other Pathfinding.GraphNode
---@param left table
---@param right table
---@param backwards bool
---@return bool
function m:GetPortal(other, left, right, backwards) end
---@param path Pathfinding.Path
---@param pathNode Pathfinding.PathNode
---@param handler Pathfinding.PathHandler
function m:Open(path, pathNode, handler) end
---@return float
function m:SurfaceArea() end
---@return UnityEngine.Vector3
function m:RandomPointOnSurface() end
---@return int
function m:GetGizmoHashCode() end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:SerializeNode(ctx) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:DeserializeNode(ctx) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:SerializeReferences(ctx) end
---@param ctx Pathfinding.Serialization.GraphSerializationContext
function m:DeserializeReferences(ctx) end
Pathfinding = {}
Pathfinding.GraphNode = m
return m