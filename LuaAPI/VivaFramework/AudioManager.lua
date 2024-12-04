---@class VivaFramework.AudioManager : Manager
---@field effectAudioSource UnityEngine.AudioSource
---@field bgmAudioSource UnityEngine.AudioSource
local m = {}
---@overload fun(clipName:string):UnityEngine.AudioSource
---@return UnityEngine.AudioSource
function m:GetAudioSource() end
function m:CleanIdleAudioSource() end
---@param clipPath string
---@return UnityEngine.AudioClip
function m:GetAudioClip(clipPath) end
function m:PauseAllAudio() end
function m:PlayAllAudio() end
function m:StopAllAudio() end
---@param clipPath string
---@param volume float
---@param isLoop bool
function m:PlayBGM(clipPath, volume, isLoop) end
function m:StopBGM() end
---@param clipPath string
---@param volumeScale float
function m:PlayEffectAudio(clipPath, volumeScale) end
VivaFramework = {}
VivaFramework.AudioManager = m
return m