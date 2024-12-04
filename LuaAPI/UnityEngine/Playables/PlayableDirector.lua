---@class UnityEngine.Playables.PlayableDirector : UnityEngine.Behaviour
---@field state UnityEngine.Playables.PlayState
---@field extrapolationMode UnityEngine.Playables.DirectorWrapMode
---@field playableAsset UnityEngine.Playables.PlayableAsset
---@field playableGraph UnityEngine.Playables.PlayableGraph
---@field playOnAwake bool
---@field timeUpdateMode UnityEngine.Playables.DirectorUpdateMode
---@field time double
---@field initialTime double
---@field duration double
local m = {}
function m:DeferredEvaluate() end
---@overload fun(asset:UnityEngine.Playables.PlayableAsset, mode:UnityEngine.Playables.DirectorWrapMode):void
---@overload fun():void
---@param asset UnityEngine.Playables.PlayableAsset
function m:Play(asset) end
---@param key UnityEngine.Object
---@param value UnityEngine.Object
function m:SetGenericBinding(key, value) end
function m:Evaluate() end
function m:Stop() end
function m:Pause() end
function m:Resume() end
function m:RebuildGraph() end
---@param id UnityEngine.PropertyName
function m:ClearReferenceValue(id) end
---@param id UnityEngine.PropertyName
---@param value UnityEngine.Object
function m:SetReferenceValue(id, value) end
---@param id UnityEngine.PropertyName
---@param idValid bool
---@return UnityEngine.Object
function m:GetReferenceValue(id, idValid) end
---@param key UnityEngine.Object
---@return UnityEngine.Object
function m:GetGenericBinding(key) end
---@param key UnityEngine.Object
function m:ClearGenericBinding(key) end
function m:RebindPlayableGraphOutputs() end
UnityEngine = {}
UnityEngine.Playables = {}
UnityEngine.Playables.PlayableDirector = m
return m