---@class VivaFramework.GameManager : Manager
local m = {}
---@return System.Collections.IEnumerator
function m:CreateProgressUI() end
function m:CheckExtractResource() end
function m:OnResourceInited() end
function m:ReLaunch() end
VivaFramework = {}
VivaFramework.GameManager = m
return m