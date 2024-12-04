---@class VivaFramework.LuaBehaviour : View
---@field DisplayName string
---@field displayName string
local m = {}
---@param funcNameArr table
---@param funcArr table
---@param displayName string
function m:SetLuaBehaviourFunc(funcNameArr, funcArr, displayName) end
function m:OnAwake() end
---@param go UnityEngine.GameObject
---@param luafunc LuaInterface.LuaFunction
function m:AddClick(go, luafunc) end
---@param go UnityEngine.GameObject
function m:RemoveClick(go) end
function m:ClearClick() end
VivaFramework = {}
VivaFramework.LuaBehaviour = m
return m