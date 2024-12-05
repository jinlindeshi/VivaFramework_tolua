local TestUI_Define = require "Module.TestUI.TestUI_Define"
---@class  TestUI_Generate:TestUI_Define
local TestUI_Generate = class("TestUI_Generate", TestUI_Define)
function TestUI_Generate:Ctor(prefabPath, gameObject, parent)
	TestUI_Generate.super.Ctor(self,prefabPath, gameObject, parent)
	local Root = self.gameObject.transform
	local tmp

	
	local tmp = Root:Find("closeBtn").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.closeBtn = tmp


	local tmp = Root:Find("bar/tab/scroll/Viewport/Content").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.content = tmp


	local tmp = Root:Find("bar").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.bar = tmp



	tmp.rectTransform = tmp:GetComponent(TypeInfo.RectTransform)



	tmp.canvasGroup = tmp:GetComponent(TypeInfo.CanvasGroup)


	local tmp = Root:Find("bar/tab/scroll").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.scroll = tmp



	tmp.scrollRect = tmp:GetComponent(TypeInfo.ScrollRect)


	local tmp = Root:Find("Info").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.info = tmp



	tmp.canvasGroup = tmp:GetComponent(TypeInfo.CanvasGroup)



	tmp.rectTransform = tmp:GetComponent(TypeInfo.RectTransform)


	local tmp = Root:Find("Info/img").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.img = tmp



	tmp.image = tmp:GetComponent(TypeInfo.Image)


end

return TestUI_Generate
