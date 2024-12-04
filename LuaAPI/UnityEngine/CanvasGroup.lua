---@class UnityEngine.CanvasGroup : UnityEngine.Behaviour
---@field alpha float
---@field interactable bool
---@field blocksRaycasts bool
---@field ignoreParentGroups bool
local m = {}
---@param endValue float
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOFade(endValue, duration) end
---@param sp UnityEngine.Vector2
---@param eventCamera UnityEngine.Camera
---@return bool
function m:IsRaycastLocationValid(sp, eventCamera) end
UnityEngine = {}
UnityEngine.CanvasGroup = m
return m