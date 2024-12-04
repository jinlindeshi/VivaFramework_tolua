---@class KeyFrameHandler : UnityEngine.MonoBehaviour
---@field LastParticleObjects table
---@field callback LuaInterface.LuaFunction
---@field fxRootArr table
---@field _fxList table
---@field data KeyFrameHandleScriptable
---@field updateData KeyFrameHandleScriptable
---@field NextFx string
---@field _PrefabList table
local m = {}
function m:initFxDic() end
function m:initPrefabDic() end
function m:initAxDic() end
---@param obj KeyFrameHandleScriptable
function m:SetScriptableData(obj) end
---@return KeyFrameHandleScriptable
function m:GetScriptableData() end
---@return KeyFrameHandleScriptable
function m:GetUpdateScriptableData() end
---@return table
function m:GetEffData() end
---@param dataList table
function m:SetEffData(dataList) end
---@param animName string
---@return table
function m:GetEvents(animName) end
---@param lucCallback LuaInterface.LuaFunction
function m:RegisterLuaCallback(lucCallback) end
function m:DestroyLastFxObject() end
function m:ClearFx() end
KeyFrameHandler = m
return m