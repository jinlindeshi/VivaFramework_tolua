---@class UnityEngine.LineRenderer : UnityEngine.Renderer
---@field startWidth float
---@field endWidth float
---@field widthMultiplier float
---@field numCornerVertices int
---@field numCapVertices int
---@field useWorldSpace bool
---@field loop bool
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
---@param startValue DG.Tweening.Color2
---@param endValue DG.Tweening.Color2
---@param duration float
---@return DG.Tweening.Tweener
function m:DOColor(startValue, endValue, duration) end
---@param index int
---@param position UnityEngine.Vector3
function m:SetPosition(index, position) end
---@param index int
---@return UnityEngine.Vector3
function m:GetPosition(index) end
---@param tolerance float
function m:Simplify(tolerance) end
---@overload fun(mesh:UnityEngine.Mesh, camera:UnityEngine.Camera, useTransform:bool):void
---@param mesh UnityEngine.Mesh
---@param useTransform bool
function m:BakeMesh(mesh, useTransform) end
---@overload fun(positions:Unity.Collections.NativeArray):void
---@overload fun(positions:Unity.Collections.NativeSlice):void
---@param positions table
function m:SetPositions(positions) end
UnityEngine = {}
UnityEngine.LineRenderer = m
return m