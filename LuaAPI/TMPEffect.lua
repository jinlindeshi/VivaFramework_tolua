---@class TMPEffect : UnityEngine.MonoBehaviour
---@field fadingSequence DG.Tweening.Sequence
local m = {}
function m:ResetOrgVector() end
---@param time float
---@param moveY float
---@param waveY float
function m:PlayWaveEffect(time, moveY, waveY) end
---@param text string
---@param singleTime float
function m:PlayFadingType(text, singleTime) end
function m:CompleteFadingType() end
TMPEffect = m
return m