---@class Prayer.TouchEvent
local m = {}
---@param type string
---@param call LuaInterface.LuaFunction
---@return LuaInterface.LuaFunction
function m.AddListener(type, call) end
---@param type string
---@param call LuaInterface.LuaFunction
function m.RemoveListener(type, call) end
function m.Clear() end
function m._Update() end
Prayer = {}
Prayer.TouchEvent = m
return m