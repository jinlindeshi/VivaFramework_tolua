---@class VivaFramework.ResourceManager : Manager
local m = {}
function m:Initialize() end
---@param assetPath string
---@return UnityEngine.GameObject
function m:LoadPrefabAtPath(assetPath) end
---@param assetPath string
---@param callBack LuaInterface.LuaFunction
function m:LoadAssetAtPathAsync(assetPath, callBack) end
---@param assetPath string
---@return UnityEngine.Object
function m:LoadAssetAtPath(assetPath) end
---@param assetPath string
---@return UnityEngine.Material
function m:LoadMaterialAtPath(assetPath) end
---@param assetPath string
---@return UnityEngine.Sprite
function m:LoadSpriteAtPath(assetPath) end
---@param assetPath string
---@param withDepends bool
---@return bool
function m:UnLoadBundleByAssetPath(assetPath, withDepends) end
---@param key string
function m:UnLoadBundleByFuzzyKey(key) end
---@param abname string
---@param async bool
---@param callBack LuaInterface.LuaFunction
---@return UnityEngine.AssetBundle
function m:LoadAssetBundle(abname, async, callBack) end
---@param abname string
---@return UnityEngine.AssetBundleCreateRequest
function m:GetLoadingRequestInfo(abname) end
---@param abname string
---@return table
function m:GetLoadingDependsList(abname) end
function m:UnloadAllBundles() end
VivaFramework = {}
VivaFramework.ResourceManager = m
return m