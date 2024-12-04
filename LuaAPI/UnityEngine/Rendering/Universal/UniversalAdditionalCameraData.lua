---@class UnityEngine.Rendering.Universal.UniversalAdditionalCameraData : UnityEngine.MonoBehaviour
---@field version float
---@field renderShadows bool
---@field requiresDepthOption UnityEngine.Rendering.Universal.CameraOverrideOption
---@field requiresColorOption UnityEngine.Rendering.Universal.CameraOverrideOption
---@field renderType UnityEngine.Rendering.Universal.CameraRenderType
---@field cameraStack table
---@field clearDepth bool
---@field requiresDepthTexture bool
---@field requiresColorTexture bool
---@field scriptableRenderer UnityEngine.Rendering.Universal.ScriptableRenderer
---@field volumeLayerMask UnityEngine.LayerMask
---@field volumeTrigger UnityEngine.Transform
---@field requiresVolumeFrameworkUpdate bool
---@field volumeStack UnityEngine.Rendering.VolumeStack
---@field renderPostProcessing bool
---@field antialiasing UnityEngine.Rendering.Universal.AntialiasingMode
---@field antialiasingQuality UnityEngine.Rendering.Universal.AntialiasingQuality
---@field stopNaN bool
---@field dithering bool
---@field allowXRRendering bool
local m = {}
---@param index int
function m:SetRenderer(index) end
function m:OnBeforeSerialize() end
function m:OnAfterDeserialize() end
function m:OnDrawGizmos() end
UnityEngine = {}
UnityEngine.Rendering = {}
UnityEngine.Rendering.Universal = {}
UnityEngine.Rendering.Universal.UniversalAdditionalCameraData = m
return m