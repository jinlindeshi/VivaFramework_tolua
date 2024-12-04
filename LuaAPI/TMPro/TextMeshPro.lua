---@class TMPro.TextMeshPro : TMPro.TMP_Text
---@field sortingLayerID int
---@field sortingOrder int
---@field autoSizeTextContainer bool
---@field transform UnityEngine.Transform
---@field renderer UnityEngine.Renderer
---@field mesh UnityEngine.Mesh
---@field meshFilter UnityEngine.MeshFilter
---@field maskType TMPro.MaskingTypes
local m = {}
---@overload fun(type:TMPro.MaskingTypes, maskCoords:UnityEngine.Vector4, softnessX:float, softnessY:float):void
---@param type TMPro.MaskingTypes
---@param maskCoords UnityEngine.Vector4
function m:SetMask(type, maskCoords) end
function m:SetVerticesDirty() end
function m:SetLayoutDirty() end
function m:SetMaterialDirty() end
function m:SetAllDirty() end
---@param update UnityEngine.UI.CanvasUpdate
function m:Rebuild(update) end
function m:UpdateMeshPadding() end
---@param ignoreActiveState bool
---@param forceTextReparsing bool
function m:ForceMeshUpdate(ignoreActiveState, forceTextReparsing) end
---@param text string
---@return TMPro.TMP_TextInfo
function m:GetTextInfo(text) end
---@param updateMesh bool
function m:ClearMesh(updateMesh) end
---@param mesh UnityEngine.Mesh
---@param index int
function m:UpdateGeometry(mesh, index) end
---@overload fun():void
---@param flags TMPro.TMP_VertexDataUpdateFlags
function m:UpdateVertexData(flags) end
function m:UpdateFontAsset() end
function m:CalculateLayoutInputHorizontal() end
function m:CalculateLayoutInputVertical() end
function m:ComputeMarginSize() end
TMPro = {}
TMPro.TextMeshPro = m
return m