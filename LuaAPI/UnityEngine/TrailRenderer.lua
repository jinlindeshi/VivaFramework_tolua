---@class UnityEngine.TrailRenderer : UnityEngine.Renderer
---@field time float
---@field startWidth float
---@field endWidth float
---@field widthMultiplier float
---@field autodestruct bool
---@field emitting bool
---@field numCornerVertices int
---@field numCapVertices int
---@field minVertexDistance float
---@field startColor UnityEngine.Color
---@field endColor UnityEngine.Color
---@field positionCount int
---@field shadowBias float
---@field generateLightingData bool
---@field textureMode UnityEngine.LineTextureMode
---@field alignment UnityEngine.LineAlignment
---@field widthCurve UnityEngine.AnimationCurve
---@field colorGradient UnityEngine.Gradient
local m = {}
---@param toStartWidth float
---@param toEndWidth float
---@param duration float
---@return DG.Tweening.Tweener
function m:DOResize(toStartWidth, toEndWidth, duration) end
---@param endValue float
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOTime(endValue, duration) end
---@param index int
---@param position UnityEngine.Vector3
function m:SetPosition(index, position) end
---@param index int
---@return UnityEngine.Vector3
function m:GetPosition(index) end
function m:Clear() end
---@overload fun(mesh:UnityEngine.Mesh, camera:UnityEngine.Camera, useTransform:bool):void
---@param mesh UnityEngine.Mesh
---@param useTransform bool
function m:BakeMesh(mesh, useTransform) end
---@overload fun(positions:Unity.Collections.NativeArray):void
---@overload fun(positions:Unity.Collections.NativeSlice):void
---@param positions table
function m:SetPositions(positions) end
---@param position UnityEngine.Vector3
function m:AddPosition(position) end
UnityEngine = {}
UnityEngine.TrailRenderer = m
return m