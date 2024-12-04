---@class UnityEngine.SpriteRenderer : UnityEngine.Renderer
---@field sprite UnityEngine.Sprite
---@field drawMode UnityEngine.SpriteDrawMode
---@field size UnityEngine.Vector2
---@field adaptiveModeThreshold float
---@field tileMode UnityEngine.SpriteTileMode
---@field color UnityEngine.Color
---@field maskInteraction UnityEngine.SpriteMaskInteraction
---@field flipX bool
---@field flipY bool
---@field spriteSortPoint UnityEngine.SpriteSortPoint
local m = {}
---@param endValue UnityEngine.Color
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOColor(endValue, duration) end
---@param endValue float
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOFade(endValue, duration) end
---@param gradient UnityEngine.Gradient
---@param duration float
---@return DG.Tweening.Sequence
function m:DOGradientColor(gradient, duration) end
---@param endValue UnityEngine.Color
---@param duration float
---@return DG.Tweening.Tweener
function m:DOBlendableColor(endValue, duration) end
---@param callback UnityEngine.Events.UnityAction
function m:RegisterSpriteChangeCallback(callback) end
---@param callback UnityEngine.Events.UnityAction
function m:UnregisterSpriteChangeCallback(callback) end
UnityEngine = {}
UnityEngine.SpriteRenderer = m
return m