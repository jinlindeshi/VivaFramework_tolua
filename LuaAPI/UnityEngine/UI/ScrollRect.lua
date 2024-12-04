---@class UnityEngine.UI.ScrollRect : UnityEngine.EventSystems.UIBehaviour
---@field content UnityEngine.RectTransform
---@field horizontal bool
---@field vertical bool
---@field movementType UnityEngine.UI.ScrollRect.MovementType
---@field elasticity float
---@field inertia bool
---@field decelerationRate float
---@field scrollSensitivity float
---@field viewport UnityEngine.RectTransform
---@field horizontalScrollbar UnityEngine.UI.Scrollbar
---@field verticalScrollbar UnityEngine.UI.Scrollbar
---@field horizontalScrollbarVisibility UnityEngine.UI.ScrollRect.ScrollbarVisibility
---@field verticalScrollbarVisibility UnityEngine.UI.ScrollRect.ScrollbarVisibility
---@field horizontalScrollbarSpacing float
---@field verticalScrollbarSpacing float
---@field onValueChanged UnityEngine.UI.ScrollRect.ScrollRectEvent
---@field velocity UnityEngine.Vector2
---@field normalizedPosition UnityEngine.Vector2
---@field horizontalNormalizedPosition float
---@field verticalNormalizedPosition float
---@field minWidth float
---@field preferredWidth float
---@field flexibleWidth float
---@field minHeight float
---@field preferredHeight float
---@field flexibleHeight float
---@field layoutPriority int
local m = {}
---@param endValue UnityEngine.Vector2
---@param duration float
---@param snapping bool
---@return DG.Tweening.Tweener
function m:DONormalizedPos(endValue, duration, snapping) end
---@param endValue float
---@param duration float
---@param snapping bool
---@return DG.Tweening.Tweener
function m:DOHorizontalNormalizedPos(endValue, duration, snapping) end
---@param endValue float
---@param duration float
---@param snapping bool
---@return DG.Tweening.Tweener
function m:DOVerticalNormalizedPos(endValue, duration, snapping) end
---@param executing UnityEngine.UI.CanvasUpdate
function m:Rebuild(executing) end
function m:LayoutComplete() end
function m:GraphicUpdateComplete() end
---@return bool
function m:IsActive() end
function m:StopMovement() end
---@param data UnityEngine.EventSystems.PointerEventData
function m:OnScroll(data) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnInitializePotentialDrag(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnBeginDrag(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnEndDrag(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnDrag(eventData) end
function m:CalculateLayoutInputHorizontal() end
function m:CalculateLayoutInputVertical() end
function m:SetLayoutHorizontal() end
function m:SetLayoutVertical() end
UnityEngine = {}
UnityEngine.UI = {}
UnityEngine.UI.ScrollRect = m
return m