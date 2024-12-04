---@class UnityEngine.ParticleSystem.MinMaxGradient
---@field mode UnityEngine.ParticleSystemGradientMode
---@field gradientMax UnityEngine.Gradient
---@field gradientMin UnityEngine.Gradient
---@field colorMax UnityEngine.Color
---@field colorMin UnityEngine.Color
---@field color UnityEngine.Color
---@field gradient UnityEngine.Gradient
local m = {}
---@overload fun(time:float, lerpFactor:float):UnityEngine.Color
---@param time float
---@return UnityEngine.Color
function m:Evaluate(time) end
---@overload fun(gradient:UnityEngine.Gradient):UnityEngine.ParticleSystem.MinMaxGradient
---@param color UnityEngine.Color
---@return UnityEngine.ParticleSystem.MinMaxGradient
function m.op_Implicit(color) end
UnityEngine = {}
UnityEngine.ParticleSystem = {}
UnityEngine.ParticleSystem.MinMaxGradient = m
return m