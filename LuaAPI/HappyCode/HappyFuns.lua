---@class HappyCode.HappyFuns
local m = {}
---@param name string
---@return table
function m.FindRootObj(name) end
---@param cam UnityEngine.Camera
---@return UnityEngine.Texture2D
function m.ScreenShotCamera(cam) end
---@param o UnityEngine.GameObject
---@param layer int
function m.SetLayerRecursive(o, layer) end
---@param layer int
---@return int
function m.GetLayerMask(layer) end
---@param bit1 int
---@param bit2 int
---@return int
function m.Bit_Or(bit1, bit2) end
---@param bit1 int
---@param bit2 int
---@return int
function m.Bit_And(bit1, bit2) end
HappyCode = {}
HappyCode.HappyFuns = m
return m