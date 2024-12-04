---@class VivaFramework.SceneManager : Manager
local m = {}
---@param sceneName string
---@param callBack LuaInterface.LuaFunction
---@param mode UnityEngine.SceneManagement.LoadSceneMode
function m:LoadSceneAsync(sceneName, callBack, mode) end
---@param scene UnityEngine.SceneManagement.Scene
---@param callBack LuaInterface.LuaFunction
function m:UnLoadSceneAsync(scene, callBack) end
VivaFramework = {}
VivaFramework.SceneManager = m
return m