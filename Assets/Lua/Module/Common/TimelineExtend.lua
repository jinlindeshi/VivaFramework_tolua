---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by sunshuo.
--- DateTime: 2019/6/19 14:21
--- timeline扩展使用

---@class TimelineExtend : LuaObj
---@field New fun(timelineObj : UnityEngine.GameObject):TimelineExtend
local TimelineExtend =  class("Module.Common.TimelineExtend", LuaObj)
function TimelineExtend:Ctor(timelineObj)
    TimelineExtend.super.Ctor(self)
    self.gameObject = timelineObj
    self:OnInitialize()
end

function TimelineExtend:OnInitialize()
    self.playableDirector = self.gameObject:GetComponent(typeof(UnityEngine.Playables.PlayableDirector)) ---@type UnityEngine.Playables.PlayableDirector
    if self.playableDirector == nil or isnull(self.playableDirector) then
        error("TimelineExtend 没有获取到PlayableDirector")
    end
    self.duration = self.playableDirector.duration
end

--播放
function TimelineExtend:Play(callBack)
    self.playableDirector:Play()
    if self.playCall then
        CancelDelayedCall(self.playCall)
    end
    if callBack then
        --结束回调
        self.callBack = callBack
        local curTime = self.playableDirector.time or 0
        self.playCall = DelayedCall(self.duration - curTime, function()
            if isnull(self.gameObject) then
                return
            end
            self.callBack = nil
            callBack()
            self.playCall = nil
        end)
    end
end

--停止
function TimelineExtend:Stop()
    self.playableDirector:Stop()
    if self.playCall then
        CancelDelayedCall(self.playCall)
        self.playCall = nil
    end
end

--暂停
function TimelineExtend:Pause()
    self.playableDirector:Pause()
    if self.playCall then
        CancelDelayedCall(self.playCall)
    end
end

--恢复
function TimelineExtend:Resume()
    self.playableDirector:Resume()
    if self.playCall then
        --若已经添加了结束回调 则在恢复播放时重新添加
        local curTime = self.playableDirector.time or 0
        self.playCall = DelayedCall(self.duration - curTime, function()
            if isnull(self.gameObject) then
                return
            end
            local callBack = self.callBack
            self.callBack = nil
            callBack()
            TryCallHandler(self.handle)
            self.playCall = nil
        end)
    end
end

--跳转
function TimelineExtend:Evaluate(time)
    if time then
        self.playableDirector.initialTime = time
    end
    self.playableDirector:Evaluate()
end

function TimelineExtend:SetCurrentTime(time)
    if time then
        self.playableDirector.time = time
    end
end

function TimelineExtend:SetPlayOnAwake(bool)
    if not bool then
        bool = false
    end
    self.playableDirector.playOnAwake = bool
end

---将指定轨道绑定到新对象
---@param name string timeline轨道名
---@param obj UnityEngine.Object 需要绑定的物体
function TimelineExtend:SetBinding(name, obj)
    if self.playableDirector == nil then
        return
    end
    local binding = LuaHelper.GetPlayableBindingByName(self.playableDirector, name)
    print("TimelineExtend:SetBinding", name, binding, binding.streamName)
    if binding ~= nil and binding.streamName == name then
        self.playableDirector:SetGenericBinding(binding.sourceObject, obj)
    end
end

---获取指定轨道绑定对象
---@param name string timeline轨道名
function TimelineExtend:GetBindingObj(name)
    if self.playableDirector == nil then
        return
    end
    local binding = LuaHelper.GetPlayableBindingByName(self.playableDirector, name)
    if binding ~= nil and binding.streamName == name then
        return self.playableDirector:GetGenericBinding(binding.sourceObject)
    end
end

---取消指定轨道绑定
---@param name string timeline轨道名
function TimelineExtend:ClearBindingObj(name)
    if self.playableDirector == nil then
        return
    end
    local binding = LuaHelper.GetPlayableBindingByName(self.playableDirector, name)
    if binding ~= nil and binding.streamName == name then
        self.playableDirector:ClearGenericBinding(binding.sourceObject)
    end
end

function TimelineExtend:PlayDollyCart(cartPath, trackPath, duration, isLoop)
    local cartObj = self.transform:Find(cartPath).gameObject
    local trackObj = self.transform:Find(trackPath).gameObject
    self.dollyCart = cartObj:GetComponent(typeof(Cinemachine.CinemachineDollyCart))
    self.dollyTrack = trackObj:GetComponent(typeof(Cinemachine.CinemachineSmoothPath))
    if self.dollyCart == nil then
        logError("PlayDollyCart CinemachineDollyCart is nil")
        return
    end
    if self.dollyTrack == nil then
        logError("PlayDollyCart CinemachineSmoothPath is nil")
        return
    end
    local length = self.dollyTrack.m_Waypoints.Length - 1
    local tween = UIUtils.DOTweenFloat(0, length, duration or 8, function(value)
        if self.dollyCart.m_Position then
            self.dollyCart.m_Position = value
        end
    end)
    if isLoop then
        tween:SetLoops(-1, DOTween_Enum.LoopType.Restart)
    end
    self.dollyCartTween = tween
end

function TimelineExtend:StopDollyCart()
    if self.dollyCartTween then
        self.dollyCartTween:Kill()
    end
end

function TimelineExtend:OnDestroy()
    TimelineExtend.super.OnDestroy(self)
    self:StopDollyCart()
end

return TimelineExtend