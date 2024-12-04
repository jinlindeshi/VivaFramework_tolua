local TestUI_Generate = require "Module.TestUI.TestUI_Generate"
---@class TestUI:TestUI_Generate
local TestUI = class("TestUI", TestUI_Generate)

-- 初始化 （注册按钮监听等）
function TestUI:Ctor()
	TestUI.super.Ctor(self, "Prefabs/TestUI/TestUI.prefab", nil, Constants.LAYER_WINDOW)

	Happy.BtnClickDownUP(self.closeBtn, function()
		print("关闭")
	end)
end

-- 关闭窗口时触发 
function TestUI:OnRecycle()
	TestUI.super.OnRecycle(self)
end

--销毁窗口触发（注销按钮监听等）
function TestUI:OnDestroy()
	TestUI.super.OnDestroy(self)
end

return TestUI
