---@class UnityEngine.ParticleSystem.MinMaxCurve
---@field mode UnityEngine.ParticleSystemCurveMode
---@field curveMultiplier float
---@field curveMax UnityEngine.AnimationCurve
---@field curveMin UnityEngine.AnimationCurve
---@field constantMax float
---@field constantMin float
---@field constant float
---@field curve UnityEngine.AnimationCurve
local m = {}
---@overload fun(time:float, lerpFactor:float):float
---@param time float
---@return float
function m:Evaluate(time) end
---@param constant float
---@return UnityEngine.ParticleSystem.MinMaxCurve
function m.op_Implicit(constant) end
UnityEngine = {}
UnityEngine.ParticleSystem = {}
UnityEngine.ParticleSystem.MinMaxCurve = m
return m