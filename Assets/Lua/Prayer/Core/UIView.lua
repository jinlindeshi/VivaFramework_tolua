---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by VIVA.
--- DateTime: 2024/7/27 18:15
---
---@class UIView:LuaObj
---@field New fun(prefabPath:string, parent:UnityEngine.Transform):UIView
local UIView = class("UIView", LuaObj)

function UIView:Ctor(prefabPath, parent)
    UIView.super.Ctor(self, prefabPath, nil, parent)
    self:OnInitialize()
end

function UIView:OnInitialize()

end

function UIView:Show()
    UIView.super.Show(self)
    self:OnOpen()
end

function UIView:Hide()
    UIView.super.Hide(self)
    self:OnClose()
end
-- 打开窗口时触发
function UIView:OnOpen()

end

-- 窗口被动打开触发
function UIView:OnFocus()
end

-- 关闭窗口时触发
function UIView:OnClose()
end

return UIView