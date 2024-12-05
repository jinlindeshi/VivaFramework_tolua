---@class  TestUI_Define:LuaObj
local TestUI_Define = class("TestUI_Define", LuaObj)
function TestUI_Define:Ctor(prefabPath, gameObject, parent)
	TestUI_Define.super.Ctor(self, prefabPath, gameObject, parent)
	local Root = self.gameObject.transform
	local tmp

		self.bar = {

		---@type UnityEngine.RectTransform
		rectTransform = {},
		---@type UnityEngine.CanvasGroup
		canvasGroup = {}
	}

end

return TestUI_Define
