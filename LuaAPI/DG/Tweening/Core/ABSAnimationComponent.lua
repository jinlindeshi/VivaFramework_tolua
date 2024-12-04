---@class DG.Tweening.Core.ABSAnimationComponent : UnityEngine.MonoBehaviour
---@field updateType DG.Tweening.UpdateType
---@field isSpeedBased bool
---@field hasOnStart bool
---@field hasOnPlay bool
---@field hasOnUpdate bool
---@field hasOnStepComplete bool
---@field hasOnComplete bool
---@field hasOnTweenCreated bool
---@field hasOnRewind bool
---@field onStart UnityEngine.Events.UnityEvent
---@field onPlay UnityEngine.Events.UnityEvent
---@field onUpdate UnityEngine.Events.UnityEvent
---@field onStepComplete UnityEngine.Events.UnityEvent
---@field onComplete UnityEngine.Events.UnityEvent
---@field onTweenCreated UnityEngine.Events.UnityEvent
---@field onRewind UnityEngine.Events.UnityEvent
---@field tween DG.Tweening.Tween
local m = {}
function m:DOPlay() end
function m:DOPlayBackwards() end
function m:DOPlayForward() end
function m:DOPause() end
function m:DOTogglePause() end
function m:DORewind() end
---@overload fun(fromHere:bool):void
function m:DORestart() end
function m:DOComplete() end
function m:DOKill() end
DG = {}
DG.Tweening = {}
DG.Tweening.Core = {}
DG.Tweening.Core.ABSAnimationComponent = m
return m