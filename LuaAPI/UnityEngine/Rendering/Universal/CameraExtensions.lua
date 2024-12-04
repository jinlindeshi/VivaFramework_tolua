---@class UnityEngine.Rendering.Universal.CameraExtensions
local m = {}
---@param camera UnityEngine.Camera
---@return UnityEngine.Rendering.Universal.UniversalAdditionalCameraData
function m.GetUniversalAdditionalCameraData(camera) end
---@param camera UnityEngine.Camera
---@return UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode
function m.GetVolumeFrameworkUpdateMode(camera) end
---@param camera UnityEngine.Camera
---@param mode UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode
function m.SetVolumeFrameworkUpdateMode(camera, mode) end
---@overload fun(camera:UnityEngine.Camera, cameraData:UnityEngine.Rendering.Universal.UniversalAdditionalCameraData):void
---@param camera UnityEngine.Camera
function m.UpdateVolumeStack(camera) end
---@overload fun(camera:UnityEngine.Camera, cameraData:UnityEngine.Rendering.Universal.UniversalAdditionalCameraData):void
---@param camera UnityEngine.Camera
function m.DestroyVolumeStack(camera) end
UnityEngine = {}
UnityEngine.Rendering = {}
UnityEngine.Rendering.Universal = {}
UnityEngine.Rendering.Universal.CameraExtensions = m
return m