---@class VivaFramework.NetworkManager : Manager
---@field pushCall LuaInterface.LuaFunction
local m = {}
function m:Connect() end
---@param action string
---@param content string
---@param callBack LuaInterface.LuaFunction
function m:Send(action, content, callBack) end
function m:Unload() end
VivaFramework = {}
VivaFramework.NetworkManager = m
return m