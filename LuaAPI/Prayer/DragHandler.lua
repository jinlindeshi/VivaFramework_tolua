---@class Prayer.DragHandler : Prayer.OperateHandler
---@field BEGIN_DRAG string
---@field DRAG string
---@field END_DRAG string
---@field DROP string
local m = {}
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnBeginDrag(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnDrag(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnEndDrag(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnDrop(eventData) end
Prayer = {}
Prayer.DragHandler = m
return m