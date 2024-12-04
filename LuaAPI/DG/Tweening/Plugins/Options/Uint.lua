---@class DG.Tweening.Core.TweenerCore<uint,uint,DG.Tweening.Plugins.Options.UintOptions> : DG.Tweening.Tweener
---@field startValue uint
---@field endValue uint
---@field changeValue uint
---@field plugOptions DG.Tweening.Plugins.Options.UintOptions
---@field getter DG.Tweening.Core.DOGetter
---@field setter DG.Tweening.Core.DOSetter
---@field timeScale float
---@field isBackwards bool
---@field id object
---@field stringId string
---@field intId int
---@field target object
---@field onPlay DG.Tweening.TweenCallback
---@field onPause DG.Tweening.TweenCallback
---@field onRewind DG.Tweening.TweenCallback
---@field onUpdate DG.Tweening.TweenCallback
---@field onStepComplete DG.Tweening.TweenCallback
---@field onComplete DG.Tweening.TweenCallback
---@field onKill DG.Tweening.TweenCallback
---@field onWaypointChange DG.Tweening.TweenCallback
---@field easeOvershootOrAmplitude float
---@field easePeriod float
---@field debugTargetId string
local m = {}
---@overload fun(newStartValue:uint, newDuration:float):DG.Tweening.Core.TweenerCore
---@param newStartValue object
---@param newDuration float
---@return DG.Tweening.Tweener
function m:ChangeStartValue(newStartValue, newDuration) end
---@overload fun(newEndValue:object, newDuration:float, snapStartValue:bool):DG.Tweening.Tweener
---@overload fun(newEndValue:uint, snapStartValue:bool):DG.Tweening.Core.TweenerCore
---@overload fun(newEndValue:uint, newDuration:float, snapStartValue:bool):DG.Tweening.Core.TweenerCore
---@param newEndValue object
---@param snapStartValue bool
---@return DG.Tweening.Tweener
function m:ChangeEndValue(newEndValue, snapStartValue) end
---@overload fun(newStartValue:uint, newEndValue:uint, newDuration:float):DG.Tweening.Core.TweenerCore
---@param newStartValue object
---@param newEndValue object
---@param newDuration float
---@return DG.Tweening.Tweener
function m:ChangeValues(newStartValue, newEndValue, newDuration) end
DG = {}
DG.Tweening = {}
DG.Tweening.Core = {}
DG.Tweening.Core.TweenerCore<uint,uint,DG = {}
DG.Tweening.Core.TweenerCore<uint,uint,DG.Tweening = {}
DG.Tweening.Core.TweenerCore<uint,uint,DG.Tweening.Plugins = {}
DG.Tweening.Core.TweenerCore<uint,uint,DG.Tweening.Plugins.Options = {}
DG.Tweening.Core.TweenerCore<uint,uint,DG.Tweening.Plugins.Options.UintOptions> = m
return m