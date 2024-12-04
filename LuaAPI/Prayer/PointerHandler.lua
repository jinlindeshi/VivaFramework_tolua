---@class Prayer.PointerHandler : Prayer.OperateHandler
---@field ENTER string
---@field EXIT string
---@field DOWN string
---@field UP string
---@field CLICK string
---@field DOUBLE_CLICK string
local m = {}
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnPointerEnter(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnPointerExit(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnPointerDown(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnPointerUp(eventData) end
---@param eventData UnityEngine.EventSystems.PointerEventData
function m:OnPointerClick(eventData) end
Prayer = {}
Prayer.PointerHandler = m
return m