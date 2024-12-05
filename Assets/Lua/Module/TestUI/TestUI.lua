local TestUI_Generate = require "Module.TestUI.TestUI_Generate"
---@class TestUI:TestUI_Generate
local TestUI = class("TestUI", TestUI_Generate)

local INFO_HIDE_X = 875
local BAR_HIDE_X = -1750
local ANI_DUR = 0.3
-- 初始化 （注册按钮监听等）
function TestUI:Ctor()
	TestUI.super.Ctor(self, "Prefabs/TestUI/TestUI.prefab", nil, Constants.LAYER_WINDOW)

	Happy.BtnClickDownUP(self.closeBtn, happyCall(self, self.BackToList))
	self.nativeBarX = self.bar.rectTransform.anchoredPosition.x
	self.nativeInfoX = self.info.rectTransform.anchoredPosition.x

	for i = 0, self.content.transform.childCount-1 do
		local imgObj = self.content.transform:GetChild(i).gameObject
		local img = GetComponent.Image(imgObj)
		Happy.BtnClickDownUP(imgObj, function()

			Happy.SetColorAlpha(img, 1)
			self.info:SetActive(true)
			self.img.image.sprite = Res.LoadSprite("UI/bigPic/"..imgObj.name..".png")
			self.info.rectTransform.anchoredPosition = Vector2.New(INFO_HIDE_X, self.info.rectTransform.anchoredPosition.y)
			self.info.canvasGroup.alpha = 0
			self.info.canvasGroup.blocksRaycasts = false
			self.bar.canvasGroup.blocksRaycasts = false
			local seq = DOTween.Sequence()
			seq:Append(self.info.rectTransform:DOAnchorPosX(self.nativeInfoX, ANI_DUR))
			seq:Join(self.info.canvasGroup:DOFade(1, ANI_DUR))
			seq:Join(self.bar.rectTransform:DOAnchorPosX(BAR_HIDE_X, ANI_DUR))
			seq:Join(self.bar.canvasGroup:DOFade(0, ANI_DUR))
			seq:AppendCallback(function()
				self.info.canvasGroup.blocksRaycasts = true
				self.bar.canvasGroup.blocksRaycasts = true
				self.info:SetActive(true)
				self.bar:SetActive(false)
				self.closeBtn:SetActive(true)
			end)
			--self.scroll.scrollRect.enabled = true
		end, function()
			Happy.SetColorAlpha(img, 0.5)
			--self.scroll.scrollRect.enabled = false
		end, function()
			Happy.SetColorAlpha(img, 1)
		end, true)
	end

	self:BackToList(true)
end

function TestUI:BackToList(noTween)
	if noTween == true then
		self.info:SetActive(false)
		self.bar:SetActive(true)
		self.closeBtn:SetActive(false)
		return
	end
	self.closeBtn:SetActive(false)
	self.bar:SetActive(true)
	self.info.rectTransform.anchoredPosition = Vector2.New(self.nativeInfoX, self.info.rectTransform.anchoredPosition.y)
	self.bar.canvasGroup.alpha = 0
	self.info.canvasGroup.blocksRaycasts = false
	self.bar.canvasGroup.blocksRaycasts = false
	local seq = DOTween.Sequence()
	seq:Append(self.info.rectTransform:DOAnchorPosX(INFO_HIDE_X, ANI_DUR))
	seq:Join(self.info.canvasGroup:DOFade(0, ANI_DUR))
	seq:Join(self.bar.rectTransform:DOAnchorPosX(self.nativeBarX, ANI_DUR))
	seq:Join(self.bar.canvasGroup:DOFade(1, ANI_DUR))
	seq:AppendCallback(function()
		self.info.canvasGroup.blocksRaycasts = true
		self.bar.canvasGroup.blocksRaycasts = true
		self.info:SetActive(false)
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
