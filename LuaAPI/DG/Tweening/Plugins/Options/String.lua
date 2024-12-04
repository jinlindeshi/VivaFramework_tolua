---@class DG.Tweening.Core.TweenerCore<string,string,DG.Tweening.Plugins.Options.StringOptions> : DG.Tweening.Tweener
---@field startValue string
---@field endValue string
---@field changeValue string
---@field plugOptions DG.Tweening.Plugins.Options.StringOptions
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
---@overload fun(newStartValue:string, newDuration:float):DG.Tweening.Core.TweenerCore
---@param newStartValue object
---@param newDuration float
---@return DG.Tweening.Tweener
function m:ChangeStartValue(newStartValue, newDuration) end
---@overload fun(newEndValue:object, newDuration:float, snapStartValue:bool):DG.Tweening.Tweener
---@overload fun(newEndValue:string, snapStartValue:bool):DG.Tweening.Core.TweenerCore
---@overload fun(newEndValue:string, newDuration:float, snapStartValue:bool):DG.Tweening.Core.TweenerCore
---@param newEndValue object
---@param snapStartValue bool
---@return DG.Tweening.Tweener
function m:ChangeEndValue(newEndValue, snapStartValue) end
---@overload fun(newStartValue:string, newEndValue:string, newDuration:float):DG.Tweening.Core.TweenerCore
---@param newStartValue object
---@param newEndValue object
---@param newDuration float
---@return DG.Tweening.Tweener
function m:ChangeValues(newStartValue, newEndValue, newDuration) end
DG = {}
DG.Tweening = {}
DG.Tweening.Core = {}
DG.Tweening.Core.TweenerCore<string,string,DG = {}
DG.Tweening.Core.TweenerCore<string,string,DG.Tweening = {}
DG.Tweening.Core.TweenerCore<string,string,DG.Tweening.Plugins = {}
DG.Tweening.Core.TweenerCore<string,string,DG.Tweening.Plugins.Options = {}
DG.Tweening.Core.TweenerCore<string,string,DG.Tweening.Plugins.Options.StringOptions> = m
return m