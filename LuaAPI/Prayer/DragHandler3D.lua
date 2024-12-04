---@class Prayer.DragHandler3D : Prayer.OperateHandler
---@field BEGIN_DRAG string
---@field DRAG string
---@field END_DRAG string
---@field MOUSE_ENTER string
---@field MOUSE_EXIT string
---@field DRAG_ENABLE string
---@field DRAG_DISABLE string
local m = {}
---@overload fun(go:UnityEngine.GameObject):void
---@param go UnityEngine.GameObject
---@param offset UnityEngine.Vector3
function m:SetMovedGameObj(go, offset) end
Prayer = {}
Prayer.DragHandler3D = m
return m