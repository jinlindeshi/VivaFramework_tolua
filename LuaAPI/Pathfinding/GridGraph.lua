---@class Pathfinding.GridGraph : Pathfinding.NavGraph
---@field uniformWidthDepthGrid bool
---@field LayerCount int
---@field size UnityEngine.Vector2
---@field transform Pathfinding.Util.GraphTransform
---@field Width int
---@field Depth int
---@field inspectorGridMode Pathfinding.InspectorGridMode
---@field inspectorHexagonSizeMode Pathfinding.InspectorGridHexagonNodeSize
---@field width int
---@field depth int
---@field aspectRatio float
---@field isometricAngle float
---@field uniformEdgeCosts bool
---@field rotation UnityEngine.Vector3
---@field center UnityEngine.Vector3
---@field unclampedSize UnityEngine.Vector2
---@field nodeSize float
---@field collision Pathfinding.GraphCollision
---@field maxClimb float
---@field maxSlope float
---@field erodeIterations int
---@field erosionUseTags bool
---@field erosionFirstTag int
---@field neighbours Pathfinding.NumNeighbours
---@field cutCorners bool
---@field penaltyPositionOffset float
---@field penaltyPosition bool
---@field penaltyPositionFactor float
---@field penaltyAngle bool
---@field penaltyAngleFactor float
---@field penaltyAnglePower float
---@field showMeshOutline bool
---@field showNodeConnections bool
---@field showMeshSurface bool
---@field neighbourOffsets table
---@field neighbourCosts table
---@field neighbourXOffsets table
---@field neighbourZOffsets table
---@field getNearestForceOverlap int
---@field nodes table
---@field active AstarPath
---@field guid Pathfinding.Util.Guid
---@field initialPenalty uint
---@field open bool
---@field graphIndex uint
---@field name string
---@field drawGizmos bool
---@field infoScreenOpen bool
local m = {}
---@return int
function m:CountNodes() end
---@param action System.Action
function m:GetNodes(action) end
---@overload fun(center:UnityEngine.Vector3, rotation:UnityEngine.Quaternion, nodeSize:float, aspectRatio:float, isometricAngle:float):void
---@param deltaMatrix UnityEngine.Matrix4x4
function m:RelocateNodes(deltaMatrix) end
---@param x int
---@param z int
---@param height float
---@return Pathfinding.Int3
function m:GraphPointToWorld(x, z, height) end
---@param mode Pathfinding.InspectorGridHexagonNodeSize
---@param value float
---@return float
function m.ConvertHexagonSizeToNodeSize(mode, value) end
---@param mode Pathfinding.InspectorGridHexagonNodeSize
---@param value float
---@return float
function m.ConvertNodeSizeToHexagonSize(mode, value) end
---@param dir int
---@return uint
function m:GetConnectionCost(dir) end
---@param node Pathfinding.GridNode
---@param dir int
---@return Pathfinding.GridNode
function m:GetNodeConnection(node, dir) end
---@overload fun(index:int, x:int, z:int, dir:int):bool
---@param node Pathfinding.GridNode
---@param dir int
---@return bool
function m:HasNodeConnection(node, dir) end
---@overload fun(index:int, x:int, z:int, dir:int, value:bool):void
---@param node Pathfinding.GridNode
---@param dir int
---@param value bool
function m:SetNodeConnection(node, dir, value) end
---@param width int
---@param depth int
---@param nodeSize float
function m:SetDimensions(width, depth, nodeSize) end
function m:UpdateTransform() end
---@return Pathfinding.Util.GraphTransform
function m:CalculateTransform() end
---@param position UnityEngine.Vector3
---@param constraint Pathfinding.NNConstraint
---@param hint Pathfinding.GraphNode
---@return Pathfinding.NNInfoInternal
function m:GetNearest(position, constraint, hint) end
---@param position UnityEngine.Vector3
---@param constraint Pathfinding.NNConstraint
---@return Pathfinding.NNInfoInternal
function m:GetNearestForce(position, constraint) end
function m:SetUpOffsetsAndCosts() end
---@param x int
---@param z int
---@param resetPenalties bool
---@param resetTags bool
function m:RecalculateCell(x, z, resetPenalties, resetTags) end
---@overload fun(xmin:int, zmin:int, xmax:int, zmax:int):void
function m:ErodeWalkableArea() end
---@param node1 Pathfinding.GridNodeBase
---@param node2 Pathfinding.GridNodeBase
---@return bool
function m:IsValidConnection(node1, node2) end
---@param x int
---@param z int
function m:CalculateConnectionsForCellAndNeighbours(x, z) end
---@overload fun(x:int, z:int):void
---@param node Pathfinding.GridNodeBase
function m:CalculateConnections(node) end
---@param gizmos Pathfinding.Util.RetainedGizmos
---@param drawNodes bool
function m:OnDrawGizmos(gizmos, drawNodes) end
---@overload fun(shape:Pathfinding.GraphUpdateShape):table
---@overload fun(rect:Pathfinding.IntRect):table
---@overload fun(rect:Pathfinding.IntRect, buffer:table):int
---@param bounds UnityEngine.Bounds
---@return table
function m:GetNodesInRegion(bounds) end
---@param x int
---@param z int
---@return Pathfinding.GridNodeBase
function m:GetNode(x, z) end
---@param node Pathfinding.GridNode
---@param dir int
---@return bool
function m:CheckConnection(node, dir) end
Pathfinding = {}
Pathfinding.GridGraph = m
return m