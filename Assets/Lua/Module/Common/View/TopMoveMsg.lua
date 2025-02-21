---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by VIVA.
--- DateTime: 2024/11/6 21:15
---
---@class TopMoveMsg:LuaObj
---@field New fun(msg:string):TopMoveMsg
local TopMoveMsg = class("TopMoveMsg", LuaObj)

function TopMoveMsg:Ctor(msg)
    TopMoveMsg.super.Ctor(self, "Prefabs/Common/TopMoveMsg.prefab", nil, Constants.LAYER_ALERT)

    self.rect = GetComponent.RectTransform(self.gameObject)
    self.contentLb = GetComponent.TextMeshProUGUI(self.transform:Find("contentLb").gameObject)
    self.contentLbRt = GetComponent.RectTransform(self.contentLb.gameObject)

    self.contentLb.text = msg
    self.contentLbRt.sizeDelta = Vector2.New(self.contentLb.preferredWidth, self.contentLbRt.sizeDelta.y)

    self.contentLbRt.anchoredPosition = Vector2.New(self.contentLbRt.sizeDelta.x + self.rect.sizeDelta.x, 0)

    local seq = DOTween.Sequence()
    seq:Append(self.contentLbRt:DOAnchorPosX(0, self.contentLbRt.anchoredPosition.x/50):SetEase(DOTWEEN_EASE.Linear))
    seq:AppendCallback(function()
        self:Recycle()
    end)

    print("你妹啊~", self.contentLb.preferredWidth)
end
return TopMoveMsg