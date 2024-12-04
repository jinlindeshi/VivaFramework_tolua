---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by likai.
--- DateTime: 2024/1/15 15:51
--- 引导列表
---@class StoryListView:LuaObj
---@field New fun(parent:UnityEngine.Transform):StoryListView
local StoryListView = class("StoryListView", LuaObj)
local StoryListItem = require("Module.Common.View.StoryListItem")

function StoryListView:Ctor(parent)
    StoryListView.super.Ctor(self, "Prefabs/Console/StoryListView.prefab", nil, parent)

    self.contentRt = GetComponent.RectTransform(self.transform:Find("Bg/Viewport/Content").gameObject)

    self.contentRt.sizeDelta = Vector2.New(0, #StoryManager.STORY_LIST * 55 - 5)


    Happy.BtnClickDownUP(self.transform:Find("ClickClose").gameObject, happyCall(self, self.Hide), nil, true)
end

function StoryListView:Show()
    StoryListView.super.Show(self)
    --print("你妹啊~StoryListView:Show", StoryManager.curStoryName)
    self.itemList = {} ---@type table<number, StoryListItem>
    for i = 1, #StoryManager.STORY_LIST do
        table.insert(self.itemList, StoryListItem.New(StoryManager.STORY_LIST[i], self.contentRt, StoryManager.curStoryName == StoryManager.STORY_LIST[i]))
    end
end

function StoryListView:Hide()
    StoryListView.super.Hide(self)
    for i = 1, #self.itemList do
        self.itemList[i]:Recycle()
    end
    self.itemList = {}
end

return StoryListView