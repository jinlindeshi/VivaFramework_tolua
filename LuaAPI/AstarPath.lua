---@class AstarPath : Pathfinding.VersionedMonoBehaviour
---@field graphs table
---@field maxNearestNodeDistanceSqr float
---@field lastScanTime float
---@field isScanning bool
---@field NumParallelThreads int
---@field IsUsingMultithreading bool
---@field IsAnyGraphUpdateQueued bool
---@field IsAnyGraphUpdateInProgress bool
---@field IsAnyWorkItemInProgress bool
---@field Version System.Version
---@field Distribution AstarPath.AstarDistribution
---@field Branch string
---@field data Pathfinding.AstarData
---@field active AstarPath
---@field showNavGraphs bool
---@field showUnwalkableNodes bool
---@field debugMode Pathfinding.GraphDebugMode
---@field debugFloor float
---@field debugRoof float
---@field manualDebugFloorRoof bool
---@field showSearchTree bool
---@field unwalkableNodeDebugSize float
---@field logPathResults Pathfinding.PathLog
---@field maxNearestNodeDistance float
---@field scanOnStartup bool
---@field fullGetNearestSearch bool
---@field prioritizeGraphs bool
---@field prioritizeGraphsLimit float
---@field colorSettings Pathfinding.AstarColor
---@field heuristic Pathfinding.Heuristic
---@field heuristicScale float
---@field threadCount Pathfinding.ThreadCount
---@field maxFrameTime float
---@field batchGraphUpdates bool
---@field graphUpdateBatchingInterval float
---@field debugPathData Pathfinding.PathHandler
---@field debugPathID ushort
---@field OnAwakeSettings System.Action
---@field OnGraphPreScan Pathfinding.OnGraphDelegate
---@field OnGraphPostScan Pathfinding.OnGraphDelegate
---@field OnPathPreSearch Pathfinding.OnPathDelegate
---@field OnPathPostSearch Pathfinding.OnPathDelegate
---@field OnPreScan Pathfinding.OnScanDelegate
---@field OnPostScan Pathfinding.OnScanDelegate
---@field OnLatePostScan Pathfinding.OnScanDelegate
---@field OnGraphsUpdated Pathfinding.OnScanDelegate
---@field On65KOverflow System.Action
---@field navmeshUpdates Pathfinding.NavmeshUpdates
---@field euclideanEmbedding Pathfinding.EuclideanEmbedding
---@field showGraphs bool
local m = {}
---@return table
function m:GetTagNames() end
function m.FindAstarPath() end
---@return table
function m.FindTagNames() end
---@overload fun(callback:System.Action):void
---@overload fun(item:Pathfinding.AstarWorkItem):void
---@param callback System.Action
function m:AddWorkItem(callback) end
function m:QueueGraphUpdates() end
---@overload fun(ob:Pathfinding.GraphUpdateObject, delay:float):void
---@overload fun(bounds:UnityEngine.Bounds):void
---@overload fun(ob:Pathfinding.GraphUpdateObject):void
---@param bounds UnityEngine.Bounds
---@param delay float
function m:UpdateGraphs(bounds, delay) end
function m:FlushGraphUpdates() end
function m:FlushWorkItems() end
---@param count Pathfinding.ThreadCount
---@return int
function m.CalculateThreadCount(count) end
function m:ConfigureReferencesInternal() end
---@return Pathfinding.PathProcessor.GraphUpdateLock
function m:PausePathfinding() end
---@overload fun(graphsToScan:table):void
---@param graphToScan Pathfinding.NavGraph
function m:Scan(graphToScan) end
---@overload fun(graphsToScan:table):System.Collections.Generic.IEnumerable
---@param graphToScan Pathfinding.NavGraph
---@return System.Collections.Generic.IEnumerable
function m:ScanAsync(graphToScan) end
---@param path Pathfinding.Path
function m.BlockUntilCalculated(path) end
---@param path Pathfinding.Path
---@param pushToFront bool
function m.StartPath(path, pushToFront) end
---@overload fun(position:UnityEngine.Vector3, constraint:Pathfinding.NNConstraint):Pathfinding.NNInfo
---@overload fun(position:UnityEngine.Vector3, constraint:Pathfinding.NNConstraint, hint:Pathfinding.GraphNode):Pathfinding.NNInfo
---@overload fun(ray:UnityEngine.Ray):Pathfinding.GraphNode
---@param position UnityEngine.Vector3
---@return Pathfinding.NNInfo
function m:GetNearest(position) end
AstarPath = m
return m