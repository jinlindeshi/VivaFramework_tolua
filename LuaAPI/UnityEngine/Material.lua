---@class UnityEngine.Material : UnityEngine.Object
---@field shader UnityEngine.Shader
---@field color UnityEngine.Color
---@field mainTexture UnityEngine.Texture
---@field mainTextureOffset UnityEngine.Vector2
---@field mainTextureScale UnityEngine.Vector2
---@field renderQueue int
---@field enabledKeywords table
---@field globalIlluminationFlags UnityEngine.MaterialGlobalIlluminationFlags
---@field doubleSidedGI bool
---@field enableInstancing bool
---@field passCount int
---@field shaderKeywords table
local m = {}
---@overload fun(endValue:UnityEngine.Color, property:string, duration:float):DG.Tweening.Core.TweenerCore
---@overload fun(endValue:UnityEngine.Color, propertyID:int, duration:float):DG.Tweening.Core.TweenerCore
---@param endValue UnityEngine.Color
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOColor(endValue, duration) end
---@overload fun(endValue:float, property:string, duration:float):DG.Tweening.Core.TweenerCore
---@overload fun(endValue:float, propertyID:int, duration:float):DG.Tweening.Core.TweenerCore
---@param endValue float
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOFade(endValue, duration) end
---@overload fun(endValue:float, propertyID:int, duration:float):DG.Tweening.Core.TweenerCore
---@param endValue float
---@param property string
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOFloat(endValue, property, duration) end
---@overload fun(endValue:UnityEngine.Vector2, property:string, duration:float):DG.Tweening.Core.TweenerCore
---@param endValue UnityEngine.Vector2
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOOffset(endValue, duration) end
---@overload fun(endValue:UnityEngine.Vector2, property:string, duration:float):DG.Tweening.Core.TweenerCore
---@param endValue UnityEngine.Vector2
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOTiling(endValue, duration) end
---@overload fun(endValue:UnityEngine.Vector4, propertyID:int, duration:float):DG.Tweening.Core.TweenerCore
---@param endValue UnityEngine.Vector4
---@param property string
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DOVector(endValue, property, duration) end
---@overload fun(endValue:UnityEngine.Color, property:string, duration:float):DG.Tweening.Tweener
---@overload fun(endValue:UnityEngine.Color, propertyID:int, duration:float):DG.Tweening.Tweener
---@param endValue UnityEngine.Color
---@param duration float
---@return DG.Tweening.Tweener
function m:DOBlendableColor(endValue, duration) end
---@param withCallbacks bool
---@return int
function m:DOComplete(withCallbacks) end
---@param complete bool
---@return int
function m:DOKill(complete) end
---@return int
function m:DOFlip() end
---@param to float
---@param andPlay bool
---@return int
function m:DOGoto(to, andPlay) end
---@return int
function m:DOPause() end
---@return int
function m:DOPlay() end
---@return int
function m:DOPlayBackwards() end
---@return int
function m:DOPlayForward() end
---@param includeDelay bool
---@return int
function m:DORestart(includeDelay) end
---@param includeDelay bool
---@return int
function m:DORewind(includeDelay) end
---@return int
function m:DOSmoothRewind() end
---@return int
function m:DOTogglePause() end
---@overload fun(name:string):bool
---@param nameID int
---@return bool
function m:HasProperty(nameID) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasFloat(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasInt(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasInteger(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasTexture(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasMatrix(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasVector(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasColor(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasBuffer(name) end
---@overload fun(nameID:int):bool
---@param name string
---@return bool
function m:HasConstantBuffer(name) end
---@param passName string
---@param enabled bool
function m:SetShaderPassEnabled(passName, enabled) end
---@param passName string
---@return bool
function m:GetShaderPassEnabled(passName) end
---@param pass int
---@return string
function m:GetPassName(pass) end
---@param passName string
---@return int
function m:FindPass(passName) end
---@param tag string
---@param val string
function m:SetOverrideTag(tag, val) end
---@overload fun(tag:string, searchFallbacks:bool):string
---@param tag string
---@param searchFallbacks bool
---@param defaultValue string
---@return string
function m:GetTag(tag, searchFallbacks, defaultValue) end
---@param start UnityEngine.Material
---@param $end UnityEngine.Material
---@param t float
function m:Lerp(start, $end, t) end
---@param pass int
---@return bool
function m:SetPass(pass) end
---@param mat UnityEngine.Material
function m:CopyPropertiesFromMaterial(mat) end
---@return int
function m:ComputeCRC() end
---@overload fun(outNames:table):void
---@return table
function m:GetTexturePropertyNames() end
---@overload fun(outNames:table):void
---@return table
function m:GetTexturePropertyNameIDs() end
---@overload fun(nameID:int, value:int):void
---@param name string
---@param value int
function m:SetInt(name, value) end
---@overload fun(nameID:int, value:float):void
---@param name string
---@param value float
function m:SetFloat(name, value) end
---@overload fun(nameID:int, value:int):void
---@param name string
---@param value int
function m:SetInteger(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Color):void
---@param name string
---@param value UnityEngine.Color
function m:SetColor(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Vector4):void
---@param name string
---@param value UnityEngine.Vector4
function m:SetVector(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Matrix4x4):void
---@param name string
---@param value UnityEngine.Matrix4x4
function m:SetMatrix(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Texture):void
---@overload fun(name:string, value:UnityEngine.RenderTexture, element:UnityEngine.Rendering.RenderTextureSubElement):void
---@overload fun(nameID:int, value:UnityEngine.RenderTexture, element:UnityEngine.Rendering.RenderTextureSubElement):void
---@param name string
---@param value UnityEngine.Texture
function m:SetTexture(name, value) end
---@overload fun(nameID:int, value:UnityEngine.ComputeBuffer):void
---@overload fun(name:string, value:UnityEngine.GraphicsBuffer):void
---@overload fun(nameID:int, value:UnityEngine.GraphicsBuffer):void
---@param name string
---@param value UnityEngine.ComputeBuffer
function m:SetBuffer(name, value) end
---@overload fun(nameID:int, value:UnityEngine.ComputeBuffer, offset:int, size:int):void
---@overload fun(name:string, value:UnityEngine.GraphicsBuffer, offset:int, size:int):void
---@overload fun(nameID:int, value:UnityEngine.GraphicsBuffer, offset:int, size:int):void
---@param name string
---@param value UnityEngine.ComputeBuffer
---@param offset int
---@param size int
function m:SetConstantBuffer(name, value, offset, size) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetFloatArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetColorArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetVectorArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetMatrixArray(name, values) end
---@overload fun(nameID:int):int
---@param name string
---@return int
function m:GetInt(name) end
---@overload fun(nameID:int):float
---@param name string
---@return float
function m:GetFloat(name) end
---@overload fun(nameID:int):int
---@param name string
---@return int
function m:GetInteger(name) end
---@overload fun(nameID:int):UnityEngine.Color
---@param name string
---@return UnityEngine.Color
function m:GetColor(name) end
---@overload fun(nameID:int):UnityEngine.Vector4
---@param name string
---@return UnityEngine.Vector4
function m:GetVector(name) end
---@overload fun(nameID:int):UnityEngine.Matrix4x4
---@param name string
---@return UnityEngine.Matrix4x4
function m:GetMatrix(name) end
---@overload fun(nameID:int):UnityEngine.Texture
---@param name string
---@return UnityEngine.Texture
function m:GetTexture(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetFloatArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetColorArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetVectorArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetMatrixArray(name) end
---@overload fun(nameID:int, value:UnityEngine.Vector2):void
---@param name string
---@param value UnityEngine.Vector2
function m:SetTextureOffset(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Vector2):void
---@param name string
---@param value UnityEngine.Vector2
function m:SetTextureScale(name, value) end
---@overload fun(nameID:int):UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function m:GetTextureOffset(name) end
---@overload fun(nameID:int):UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function m:GetTextureScale(name) end
UnityEngine = {}
UnityEngine.Material = m
return m