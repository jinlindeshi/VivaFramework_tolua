---@class ObjectFollower : UnityEngine.MonoBehaviour
---@field targets table
---@field centerOffset UnityEngine.Vector3
---@field defaultObject UnityEngine.Transform
---@field isGUI bool
---@field stageCamera UnityEngine.Camera
---@field subViewport UnityEngine.RectTransform
---@field syncRotateType ObjectFollower.SyncRotationType
---@field targetEulersOffset UnityEngine.Vector3
---@field selfEulersOffset UnityEngine.Vector3
local m = {}
---@param target UnityEngine.Transform
function m:SetTarget(target) end
---@param target UnityEngine.Transform
---@param isGUI bool
---@param syncRotateType ObjectFollower.SyncRotationType
function m:AddTarget(target, isGUI, syncRotateType) end
function m:ClearTargets() end
---@param target UnityEngine.Transform
function m:RemoveTarget(target) end
ObjectFollower = m
return m