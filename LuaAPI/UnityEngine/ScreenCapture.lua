---@class UnityEngine.ScreenCapture
local m = {}
---@overload fun(filename:string, superSize:int):void
---@overload fun(filename:string, stereoCaptureMode:UnityEngine.ScreenCapture.StereoScreenCaptureMode):void
---@param filename string
function m.CaptureScreenshot(filename) end
---@overload fun(superSize:int):UnityEngine.Texture2D
---@overload fun(stereoCaptureMode:UnityEngine.ScreenCapture.StereoScreenCaptureMode):UnityEngine.Texture2D
---@return UnityEngine.Texture2D
function m.CaptureScreenshotAsTexture() end
---@param renderTexture UnityEngine.RenderTexture
function m.CaptureScreenshotIntoRenderTexture(renderTexture) end
UnityEngine = {}
UnityEngine.ScreenCapture = m
return m