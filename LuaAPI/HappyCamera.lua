---@class HappyCamera : UnityEngine.MonoBehaviour
---@field cameraObj UnityEngine.GameObject
---@field attachObj UnityEngine.GameObject
---@field directionObj UnityEngine.GameObject
---@field lockY bool
---@field tweenResumeSpeed float
---@field resumCallBack LuaInterface.LuaFunction
---@field autoUpdate bool
---@field vRotateAngle float
---@field hRotateAngle float
---@field followParams HappyCamera.FollowVO
local m = {}
function m:EditorInit() end
---@param obj UnityEngine.GameObject
function m:SetAttachObj(obj) end
---@param obj UnityEngine.GameObject
function m:SetDirectionObj(obj) end
---@param obj UnityEngine.GameObject
function m:SetCameraObj(obj) end
---@param v UnityEngine.Vector3
function m:SetOffsetV(v) end
---@return UnityEngine.Vector3
function m:GetOffsetV() end
---@param offset UnityEngine.Vector3
---@param lookAt bool
---@param globalReference bool
---@param checkCover bool
---@param attachOffset UnityEngine.Vector3
---@param checkOverFun LuaInterface.LuaFunction
function m:TakeFollow(offset, lookAt, globalReference, checkCover, attachOffset, checkOverFun) end
---@param onlyGetValue bool
---@param call LuaInterface.LuaFunction
function m:FixTransform(onlyGetValue, call) end
---@param fun LuaInterface.LuaFunction
function m:SetCheckOverFun(fun) end
HappyCamera = m
return m