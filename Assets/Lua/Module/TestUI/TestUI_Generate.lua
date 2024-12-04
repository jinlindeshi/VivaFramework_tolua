---@class  TestUI_Generate:LuaObj
local TestUI_Generate = class("TestUI_Generate", LuaObj)
function TestUI_Generate:Ctor(prefabPath, gameObject, parent)
	TestUI_Generate.super.Ctor(self,prefabPath, gameObject, parent)
	local Root = self.gameObject.transform
	local tmp

	
	local tmp = Root:Find("closeBtn").gameObject ---@type UnityEngine.GameObject
	if tolua.getpeer(tmp) == nil then
		tolua.setpeer(tmp, {})
	end

	self.closeBtn = tmp


end

return TestUI_Generate
