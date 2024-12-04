---@class UnityEngine.Audio.AudioMixer : UnityEngine.Object
---@field outputAudioMixerGroup UnityEngine.Audio.AudioMixerGroup
---@field updateMode UnityEngine.Audio.AudioMixerUpdateMode
local m = {}
---@param floatName string
---@param endValue float
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOSetFloat(floatName, endValue, duration) end
---@param withCallbacks bool
---@return int
function m:DOComplete(withCallbacks) end
---@param complete bool
---@return int
function m:DOKill(complete) end
---@return int
function m:DOFlip() end
---@param to float
---@param andPlay bool
---@return int
function m:DOGoto(to, andPlay) end
---@return int
function m:DOPause() end
---@return int
function m:DOPlay() end
---@return int
function m:DOPlayBackwards() end
---@return int
function m:DOPlayForward() end
---@return int
function m:DORestart() end
---@return int
function m:DORewind() end
---@return int
function m:DOSmoothRewind() end
---@return int
function m:DOTogglePause() end
---@param name string
---@return UnityEngine.Audio.AudioMixerSnapshot
function m:FindSnapshot(name) end
---@param subPath string
---@return table
function m:FindMatchingGroups(subPath) end
---@param snapshots table
---@param weights table
---@param timeToReach float
function m:TransitionToSnapshots(snapshots, weights, timeToReach) end
---@param name string
---@param value float
---@return bool
function m:SetFloat(name, value) end
---@param name string
---@return bool
function m:ClearFloat(name) end
---@param name string
---@param value float
---@return bool
function m:GetFloat(name, value) end
UnityEngine = {}
UnityEngine.Audio = {}
UnityEngine.Audio.AudioMixer = m
return m