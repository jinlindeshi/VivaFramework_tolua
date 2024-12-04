---@class VivaFramework.LuaHelper
---@field blurDrawing bool
---@field blurRange float
local m = {}
---@param classname string
---@return System.Type
function m.GetType(classname) end
---@return VivaFramework.ResourceManager
function m.GetResManager() end
---@return VivaFramework.NetworkManager
function m.GetNetManager() end
---@return VivaFramework.LuaManager
function m.GetLuaManager() end
---@return VivaFramework.SceneManager
function m.GetSceneManager() end
---@return VivaFramework.AudioManager
function m.GetAudioManager() end
---@param data LuaInterface.LuaByteBuffer
---@param func LuaInterface.LuaFunction
function m.OnCallLuaFunc(data, func) end
---@param data string
---@param func LuaInterface.LuaFunction
function m.OnJsonCallFunc(data, func) end
---@return bool
function m.CheckRayOverObject() end
---@param position UnityEngine.Vector3
---@return UnityEngine.RaycastHit
function m.Raycast(position) end
---@param guiCamera UnityEngine.Camera
---@return UnityEngine.RaycastHit2D
function m.Raycast2D(guiCamera) end
---@param o object
---@return bool
function m.ObjectIsNull(o) end
---@overload fun(original:UnityEngine.Object, parent:UnityEngine.Transform):UnityEngine.Object
---@param original UnityEngine.Object
---@return UnityEngine.Object
function m.Instantiate(original) end
---@param canvas UnityEngine.Canvas
---@param position UnityEngine.Vector3
---@return UnityEngine.Vector3
function m.WorldToCanvasPoint(canvas, position) end
---@param position UnityEngine.Vector3
---@return UnityEngine.Vector3
function m.WorldToScreenPoint(position) end
---@param position UnityEngine.Vector3
---@return UnityEngine.Vector3
function m.ScreenToWorldPoint(position) end
---@param particleSystems table
---@return float
function m.ParticleSystemLength(particleSystems) end
---@param go UnityEngine.GameObject
---@return table
function m.GetAllParticleSystem(go) end
---@param playableDir UnityEngine.Playables.PlayableDirector
---@param name string
---@return UnityEngine.Playables.PlayableBinding
function m.GetPlayableBindingByName(playableDir, name) end
---@param go UnityEngine.GameObject
---@param name string
---@return UnityEngine.GameObject
function m.GetChildByName(go, name) end
function m.SetAutoRotationLandscape() end
function m.SetAutoRotationPortrait() end
---@param origin string
---@param replace string
---@param target string
---@return string
function m.StringRelaceAll(origin, replace, target) end
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
---@param host string
---@return UnityEngine.Ping
function m.NewPing(host) end
---@param co UnityEngine.Collider
---@param pos UnityEngine.Vector3
---@return double
function m.ColliderDistance(co, pos) end
---@param go UnityEngine.GameObject
function m.SetDontDestroyOnLoad(go) end
---@param enabled bool
---@param range float
function m.DrawBlurTextureToggle(enabled, range) end
---@param baseCam UnityEngine.Camera
---@param overlayCam UnityEngine.Camera
---@param checkRepeat bool
---@return bool
function m.AddCameraToStackList(baseCam, overlayCam, checkRepeat) end
---@param baseCam UnityEngine.Camera
---@param overlayCam UnityEngine.Camera
---@return bool
function m.RemoveCameraFromStackList(baseCam, overlayCam) end
VivaFramework = {}
VivaFramework.LuaHelper = m
return m