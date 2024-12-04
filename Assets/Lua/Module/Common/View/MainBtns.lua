---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by likai.
--- DateTime: 2024/6/25 14:16
--- 主要入口按钮
---@class MainBtns:LuaObj
---@field New fun(isWorld:boolean):MainBtns
local MainBtns = class("MainBtns",LuaObj)

function MainBtns:Ctor(isWorld)
    MainBtns.super.Ctor(self, "Prefabs/Common/MainBtns.prefab", nil, Constants.LAYER_UI)

    self.cityBtn = self.transform:Find("CityBtn").gameObject
    self.worldBtn = self.transform:Find("WorldBtn").gameObject
    self.dungeonBtn = self.transform:Find("DungeonBtn").gameObject

    self.cityBtn:SetActive(isWorld == true)
    self.worldBtn:SetActive(isWorld ~= true)

    self.worldBtnClickFun = nil

    Happy.BtnClickDownUP(self.cityBtn, function()
        SM.AddScene("MainCity", require("Module.City.CityScene"), function()

        end)
    end)
    Happy.BtnClickDownUP(self.worldBtn, function()
        if self.worldBtnClickFun then
            self.worldBtnClickFun()
            return
        end
        SM.AddScene("World", require("Module.World.WorldScene"), function()

        end)
    end)

    Happy.BtnClickDownUP(self.dungeonBtn, happyCall(self, self.OpenDungeon))
end

function MainBtns:OpenDungeon()

    if not self.dungeonView then
        self.dungeonView = require("Module.Dungeon.DungeonView").New("Prefabs/Dungeon/DungeonView.prefab", Constants.LAYER_WINDOW)
    end
    self.dungeonView:Show()
end

function MainBtns:PlayUnlock(btn)

    btn.transform:Find("nameLb").gameObject:SetActive(true)
    self:SetBtnEnabled(btn, true)

    local seq = DOTween.Sequence()
    seq:Append(btn.transform:DOScale(Vector3.one * 1.3, 0.2):SetEase(DOTWEEN_EASE.Linear))
    seq:Append(btn.transform:DOScale(Vector3.one, 0.2):SetEase(DOTWEEN_EASE.Linear))
end

function MainBtns:SetBtnEnabled(btn, enabled)

    GetComponent.Image(btn).raycastTarget = enabled
    Happy.ChangeUIGray(btn, not enabled)
end

---@param btn UnityEngine.GameObject
function MainBtns:SetBtnRed(btn, enabled)
    local red = btn.transform:Find("red").gameObject
    red:SetActive(enabled)
end

return MainBtns