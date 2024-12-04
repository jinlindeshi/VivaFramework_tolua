---@class Coffee.UIExtensions.UIParticle : UnityEngine.UI.MaskableGraphic
---@field raycastTarget bool
---@field ignoreCanvasScaler bool
---@field shrinkByMaterial bool
---@field scale float
---@field scale3D UnityEngine.Vector3
---@field particles table
---@field materials System.Collections.Generic.IEnumerable
---@field materialForRendering UnityEngine.Material
---@field activeMeshIndices table
local m = {}
function m:Play() end
function m:Pause() end
function m:Stop() end
function m:Clear() end
---@overload fun(instance:UnityEngine.GameObject, destroyOldParticles:bool):void
---@param instance UnityEngine.GameObject
function m:SetParticleSystemInstance(instance) end
---@param prefab UnityEngine.GameObject
function m:SetParticleSystemPrefab(prefab) end
---@overload fun(root:UnityEngine.GameObject):void
function m:RefreshParticles() end
Coffee = {}
Coffee.UIExtensions = {}
Coffee.UIExtensions.UIParticle = m
return m