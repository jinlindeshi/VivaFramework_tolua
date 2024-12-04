---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by likai.
--- DateTime: 2018/5/19 下午4:13
---
Game = {}

function Game.Init()
    require "Data.Config.SceneConfig"
    require "Prayer.PrayerConstants"
    require "Prayer.Utils.MathUtil"

    JSON = require("Prayer.Utils.JSON")
    require("Module.Common.Happy")

    require("Manager.SceneManager").New(function ()
        Game.InitUICanvas()
        require("Module.TestUI.TestUI").New() ---测试界面

        ---自动战斗Demo
        --SM.AddScene("War", require("Module.WarScene.WarScene"), nil, true)
    end)
    require("Manager.NetworkManager").New()

    DOTween.SetTweensCapacity(5000, 100)
    --print("Game.Init", require("cjson"), JSON)
    Game.Manager = {}
    local component = AddOrGetComponent(GameObject.Find("GameManager"), Prayer.ManagerHandler)
    component.checkCall = function(funName)
        if Game.Manager[funName] then
            Game.Manager[funName](Game.Manager)
        end
    end
    local bm = AddOrGetComponent(component.gameObject, BehaviorDesigner.Runtime.BehaviorManager) ---@type BehaviorDesigner.Runtime.BehaviorManager
    bm.UpdateInterval = BehaviorDesigner.Runtime.UpdateIntervalType.SpecifySeconds
    bm.UpdateIntervalSeconds = 0.1
    Game.behaviorCalls = {}

    --Prayer.TouchEvent.AddListener("TouchEvent_Begin", Game.AddClickFx)
    Game.touchEventCom = Prayer.TouchEvent
end


function Game.InitUICanvas()
    --print("Game.InitUICanvas")
    Game.UICanvas = GameObject.Instantiate(resMgr:LoadPrefabAtPath("Prefabs/UICanvas.prefab")) ---@type UnityEngine.GameObject
    --Game.UICanvas = GameObject.Instantiate(resMgr:LoadPrefabAtPath("Prefabs/UICanvas3D.prefab")) ---@type UnityEngine.GameObject
    Game.UICanvas.name = "UICanvas"
    LuaHelper.SetDontDestroyOnLoad(Game.UICanvas)
    UIMgr.InitLayer(Game.UICanvas)

    Game.PrefabPool = GameObject.New("PrefabPool") ---@type UnityEngine.GameObject
    local rect = AddOrGetComponent(Game.PrefabPool, UnityEngine.RectTransform) ---@type UnityEngine.RectTransform
    rect.sizeDelta = GetComponent.RectTransform(Game.UICanvas).sizeDelta
    LuaHelper.SetDontDestroyOnLoad(Game.PrefabPool)
    Game.PrefabPool:SetActive(false)


    --local uiCamera = GetComponent.Canvas(Game.UICanvas).worldCamera
    --local mainCamera = GetComponent.Camera(GameObject.FindGameObjectWithTag("MainCamera"))
    --
    --local uiCameraData = CameraExtension.GetUniversalAdditionalCameraData(uiCamera)
    --local mainCameraData = CameraExtension.GetUniversalAdditionalCameraData(mainCamera)

end

local fx_click = "Prefabs/TapEffect.prefab"
function Game.AddClickFx(type, pos)
    local layer = UIMgr.GetLayer(UILayerName.top)
    local uiCamera = GetComponent.Canvas(Game.UICanvas).worldCamera
    local _, p = RectTransformUtility.ScreenPointToLocalPointInRectangle(layer, pos, uiCamera, Vector2.zero)
    local fx = CreatePrefab(fx_click, layer, 1)
    fx.transform.localPosition = p
end

---获得场景上的对象
function GetRootObjByName(name)
    local t = UnityEngine.SceneManagement.SceneManager.GetActiveScene():GetRootGameObjects()
    for i = 0, t.Length-1 do
        if name == t[i].name then
            return t[i]
        end
    end
    return nil
end

Stage = {}
function AddEventListener(target, behaviorName, callBack, caller)
    if target ~= Stage then
        EventMgr.AddEventListener(target, behaviorName, callBack, caller)
        return
    end
    --print("你妹啊~0", caller, debug.traceback())
    if not Game.behaviorCalls[behaviorName] then
        Game.behaviorCalls[behaviorName] = {}
    end
    local list = Game.behaviorCalls[behaviorName]
    table.insert(list, {target = target, callBack = callBack, caller = caller})
    if not Game.Manager[behaviorName] then
        Game.Manager[behaviorName] = function()
            --local cloneList = clone(list)
            for i = 1, #list do
                if list[i] then
                    --print("你妹啊~1", caller)
                    list[i].callBack(list[i].caller)
                end
            end
        end
    end

    return callBack
end

function RemoveEventListener(target, behaviorName, callBack, caller)
    if target ~= Stage then
        EventMgr.RemoveEventListener(target, behaviorName, callBack, caller)
        return
    end
    if not Game.behaviorCalls[behaviorName] then
        return
    end
    local list = Game.behaviorCalls[behaviorName]
    local removeIndex
    for i = 1, #list do
        if list[i].target == target and list[i].callBack == callBack and list[i].caller == caller then
            removeIndex = i
            break
        end
    end
    if removeIndex then
        table.remove(list, removeIndex)
    end
end

---派发事件
---@param target table 事件派发主体
---@param type string 事件类型
---@param data any 事件数据
---@param isRecycle boolean 是否需要回收事件 默认为true
function DispatchEvent(target, type, data, isRecycle)
    EventMgr.DispatchEvent(target, type, data, isRecycle)
end

---延迟指定帧数调用
---@param frameCount number 延迟的帧数
function DelayedFrameCall(callBack, frameCount)

    local frameCall
    frameCount = frameCount or 1
    local count = 0
    frameCall = function ()
        count = count + 1
        if count == frameCount then
            RemoveEventListener(Stage, Event.UPDATE, frameCall)
            callBack()
        end
    end
    AddEventListener(Stage, Event.UPDATE, frameCall)
end

local delayinfos = {}
---延迟指定时间调用
---@param delayTime number 单位秒
---@return table
function DelayedCall(delayTime, callBack)
    local delayCheck
    local startTime = Time.time
    --print("DelayedCall1", startTime)
    delayCheck = function()
        if not delayinfos[delayCheck] then
            RemoveEventListener(Stage, Event.UPDATE, delayCheck)
            return
        end
        if (Time.time - startTime) > delayTime then
            RemoveEventListener(Stage, Event.UPDATE, delayCheck)
            delayinfos[delayCheck] = nil
            --print("DelayedCall2", callBack)
            callBack()
        end
    end
    AddEventListener(Stage, Event.UPDATE, delayCheck)
    delayinfos[delayCheck] = {call=delayCheck, startTime=startTime, delayTime=delayTime, callBack=callBack}
    return delayinfos[delayCheck]
end

---暂停延迟时间调用
function CancelDelayedCall(delayInfo)
    if (Time.time - delayInfo.startTime) > delayInfo.delayTime then
        return
    end
    delayInfo.pause = true
    delayinfos[delayInfo.call] = nil
    RemoveEventListener(Stage, Event.UPDATE, delayInfo.call)
    delayInfo.delayTime = delayInfo.delayTime - (Time.time - delayInfo.startTime)
    delayInfo.startTime = nil
    return delayInfo
end

---恢复暂停的延迟调用
function ResumeDelayedCall(delayInfo)
    if delayinfos[delayInfo.call] then
        return
    end
    delayInfo.pause = false
    DelayedCall(delayInfo.delayTime, delayInfo.callBack)
end


function Log(text)
    Util.Log(text)
end
function LogError(text)
    Util.LogError(text)
end
function LogWarning(text)
    Util.LogWarning(text)
end

---重载游戏
function Game.Relaunch()
    DelayedFrameCall(function()
        delayinfos = {}
        DestroyImmediate(Game.UICanvas)
        DestroyImmediate(Game.PrefabPool)
    end)
    GameObject.Find("GameManager"):GetComponent(typeof(VivaFramework.GameManager)):ReLaunch()
end

local prefabsPool = {}
local prefabsInitProps = {}

---实例化
---@param path string prefab路径
---@param parent UnityEngine.Transform 父节点
---@param noPool boolean 是否加入缓存池
---@param recycleDelay number 需要自动回收的话，填一个倒计时间
---@return UnityEngine.GameObject
function CreatePrefab(path, parent, recycleDelay, noPool)
    local prefab = resMgr:LoadPrefabAtPath(path) ---@type UnityEngine.GameObject
    if not prefabsInitProps[path] then
        prefabsInitProps[path] = {localScale = prefab.transform.localScale, localPosition = prefab.transform.localPosition,
                                  localEulerAngles = prefab.transform.localEulerAngles}
        local rect = GetComponent.RectTransform(prefab)
        if rect then
            prefabsInitProps[path].anchoredPosition = rect.anchoredPosition
        end
    end
    if not prefabsPool[path] then
        prefabsPool[path] = {}
    end
    if type(parent) == "string" then
        parent = UIMgr.GetLayer(parent)
    end
    local obj
    if #prefabsPool[path] == 0 or noPool then
        obj = GameObject.Instantiate(prefab, parent)
        --print("你妹啊~", path)
    else
        obj = table.remove(prefabsPool[path], 1) ---@type UnityEngine.GameObject
        obj:SetActive(true)
        obj.transform:SetParent(parent)
        local initInfo = prefabsInitProps[path]
        obj.transform.localScale = initInfo.localScale
        obj.transform.localPosition = initInfo.localPosition
        obj.transform.localEulerAngles = initInfo.localEulerAngles
        if initInfo.anchoredPosition then
            GetComponent.RectTransform(obj).anchoredPosition = initInfo.anchoredPosition
        end

    end

    if recycleDelay and recycleDelay > 0 then
        DelayedCall(recycleDelay, function()
            RecyclePrefab(obj, path)
        end)
    end
    return obj
end


local preCreateList = {}
local preCreateRunning = false
---预实例化进缓存池，自动分帧进行，减少卡顿
---@param pathOrGO string|UnityEngine.GameObject prefab路径或要复制的样本
function PreInstantiate(pathOrGO, noCreateIns)
    table.insert(preCreateList, {pathOrGO=pathOrGO})
    if preCreateRunning == false then
        local checkFun
        checkFun = function()
            if #preCreateList == 0 then
                preCreateRunning = false
                return
            end
            preCreateRunning = true
            DelayedFrameCall(function()
                local info = preCreateList[1]
                table.remove(preCreateList, 1)

                local prefab, gameObj
                if type(info.pathOrGO) == "string" then
                    local completeFun = function()

                        prefab = resMgr:LoadPrefabAtPath(info.pathOrGO)


                        if not prefabsInitProps[info.pathOrGO] then
                            --print("你妹啊~", info.pathOrGO)
                            prefabsInitProps[info.pathOrGO] = {localScale = prefab.transform.localScale, localPosition = prefab.transform.localPosition,
                                                               localEulerAngles = prefab.transform.localEulerAngles}
                            local rect = GetComponent.RectTransform(prefab)
                            if rect then
                                prefabsInitProps[info.pathOrGO].anchoredPosition = rect.anchoredPosition
                            end
                        end

                        if noCreateIns ~= true then
                            if not prefabsPool[info.pathOrGO] then
                                prefabsPool[info.pathOrGO] = {}
                            end
                            gameObj = GameObject.Instantiate(prefab, Game.PrefabPool.transform)
                            table.insert(prefabsPool[info.pathOrGO], gameObj)
                        end
                        --print("你妹啊", info.pathOrGO, gameObj)
                        checkFun()
                    end
                    if AppConst.UseBundle ~= true then
                        completeFun()
                    else
                        resMgr:LoadAssetAtPathAsync(info.pathOrGO, completeFun)
                    end
                end
            end)
        end
        checkFun()
    end
end

---克隆一个现有的对象 并且支持对象池
---@param gameObj UnityEngine.GameObject 想要复制的GameObject
---@param parent UnityEngine.Transform 父节点
---@param poolKey string 缓存池用的key
---@return UnityEngine.GameObject
function ClonePrefab(gameObj, parent, poolKey)
    if poolKey and not prefabsPool[poolKey] then
        prefabsPool[poolKey] = {}
    end
    if type(parent) == "string" then
        parent = UIMgr.GetLayer(parent)
    end
    if not poolKey or #prefabsPool[poolKey] == 0 then
        return GameObject.Instantiate(gameObj, parent)
    else
        local obj = prefabsPool[poolKey][1] ---@type UnityEngine.GameObject
        obj.transform:SetParent(parent)
        table.remove(prefabsPool[poolKey], 1)
        return obj
    end
end

---@param obj UnityEngine.GameObject
---@param pathOrKey string 缓存池用的path或key
function RecyclePrefab(obj, pathOrKey)
    if not pathOrKey or obj == nil or isnull(obj) == true then
        return
    end
    if not prefabsPool[pathOrKey] then
        prefabsPool[pathOrKey] = {}
    end
    obj.transform:SetParent(Game.PrefabPool.transform)
    obj:SetActive(true)
    table.insert(prefabsPool[pathOrKey], obj)
end

---添加按钮相关事件
---@param go UnityEngine.GameObject
---@return function
function AddButtonHandler(go, type, callBack, caller)
    local pointerHandler = AddOrGetComponent(go, Prayer.PointerHandler)
    return pointerHandler:AddCall(type, function(eventData)
        if caller then
            callBack(caller, eventData)
        else
            callBack(eventData)
        end
    end)
end

---移除按钮相关事件
---@param go UnityEngine.GameObject
function RemoveButtonHandler(go, type, callBack)
    if not go or isnull(go) == true then
        return
    end
    local pointerHandler = GetComponent.PointerHandler(go)
    if not pointerHandler then
        return
    end
    pointerHandler:RemoveCall(type, callBack)
    return callBack
end

---添加按钮相关事件
---@param go UnityEngine.GameObject
---@return function
function AddButtonHandler3D(go, type, callBack, caller)
    local pointerHandler = AddOrGetComponent(go, Prayer.PointerHandler3D)
    return pointerHandler:AddCall(type, function(eventData)
        if caller then
            callBack(caller, eventData)
        else
            callBack(eventData)
        end
    end)
end

---移除按钮相关事件
---@param go UnityEngine.GameObject
function RemoveButtonHandler3D(go, type, callBack)
    if not go or isnull(go) == true then
        return
    end
    local pointerHandler = GetComponent.PointerHandler3D(go)
    if not pointerHandler then
        return
    end
    pointerHandler:RemoveCall(type, callBack)
    return callBack
end

---为对象添加唯一组件，已有则Return
---@param go UnityEngine.GameObject
---@param type string
function AddOrGetComponent(go, type)
    local t = typeof(type)
    local com = go:GetComponent(t)
    if not com then
        com = go:AddComponent(t)
    end
    --if com.enabled then
    --    com.enabled = true
    --end
    return com
end

---
---@param go UnityEngine.GameObject
function isnull(go)
    return tolua.isnull(go)
end

---销毁 GameObject
---@param go UnityEngine.GameObject
---@param delay number
function Destroy(go, delay)
    GameObject.Destroy(go, delay or 0)
end
---立即销毁 GameObject
---@param go UnityEngine.GameObject
function DestroyImmediate(go)
    GameObject.DestroyImmediate(go)
end

GetComponent = {}

---获得 RectTransform 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.RectTransform
function GetComponent.RectTransform(go)
    return go:GetComponent(typeof(UnityEngine.RectTransform))
end

---获得 Canvas 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Canvas
function GetComponent.Canvas(go)
    return go:GetComponent(typeof(UnityEngine.Canvas))
end

---获得 CanvasGroup 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.CanvasGroup
function GetComponent.CanvasGroup(go)
    return go:GetComponent(typeof(UnityEngine.CanvasGroup))
end

---获得 Text 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.UI.Text
function GetComponent.Text(go)
    return go:GetComponent(typeof(UnityEngine.UI.Text))
end

---获得 Image 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.UI.Image
function GetComponent.Image(go)
    return go:GetComponent(typeof(UnityEngine.UI.Image))
end

---获得 TextMeshPro 组件
---@param go UnityEngine.GameObject
---@return TMPro.TextMeshPro
function GetComponent.TextMeshPro(go)
    return go:GetComponent(typeof(TMPro.TextMeshPro))
end

---获得 TextMeshProUGUI 组件
---@param go UnityEngine.GameObject
---@return TMPro.TextMeshProUGUI
function GetComponent.TextMeshProUGUI(go)
    return go:GetComponent(typeof(TMPro.TextMeshProUGUI))
end

---获得 TMP_InputField 组件
---@param go UnityEngine.GameObject
---@return TMPro.TMP_InputField
function GetComponent.TMP_InputField(go)
    return go:GetComponent(typeof(TMPro.TMP_InputField))
end

---获得 SpriteRenderer 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.SpriteRenderer
function GetComponent.SpriteRenderer(go)
    return go:GetComponent(typeof(UnityEngine.SpriteRenderer))
end

---获得 Collider 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Collider
function GetComponent.Collider(go)
    return go:GetComponent(typeof(UnityEngine.Collider))
end


---获得 PointerHandler 组件
---@param go UnityEngine.GameObject
---@return Prayer.PointerHandler
function GetComponent.PointerHandler(go)
    return go:GetComponent(typeof(Prayer.PointerHandler))
end

---获得 PointerHandler3D 组件
---@param go UnityEngine.GameObject
---@return Prayer.PointerHandler3D
function GetComponent.PointerHandler3D(go)
    return go:GetComponent(typeof(Prayer.PointerHandler3D))
end

---获得 DragHandler 组件
---@param go UnityEngine.GameObject
---@return Prayer.DragHandler
function GetComponent.DragHandler(go)
    return go:GetComponent(typeof(Prayer.DragHandler))
end

---获得 DragHandler3D 组件
---@param go UnityEngine.GameObject
---@return Prayer.DragHandler3D
function GetComponent.DragHandler3D(go)
    return go:GetComponent(typeof(Prayer.DragHandler3D))
end

---获得 Camera 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Camera
function GetComponent.Camera(go)
    return go:GetComponent(typeof(UnityEngine.Camera))
end

---获得 Animator 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Animator
function GetComponent.Animator(go)
    return go:GetComponent(typeof(UnityEngine.Animator))
end

---获得 Animation 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Animation
function GetComponent.Animation(go)
    return go:GetComponent(typeof(UnityEngine.Animation))
end

---获得 Light 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Light
function GetComponent.Light(go)
    return go:GetComponent(typeof(UnityEngine.Light))
end

---获得 CharacterController 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.CharacterController
function GetComponent.CharacterController(go)
    return go:GetComponent(typeof(UnityEngine.CharacterController))
end

---获得 HappyCamera 组件
---@param go UnityEngine.GameObject
---@return HappyCamera
function GetComponent.HappyCamera(go)
    return go:GetComponent(typeof(HappyCamera))
end

---获得 BehaviorToLua 组件
---@param go UnityEngine.GameObject
---@return BehaviorToLua
function GetComponent.BehaviorToLua(go)
    return go:GetComponent(typeof(BehaviorToLua))
end

---获得 BehaviorTree 组件
---@param go UnityEngine.GameObject
---@return BehaviorDesigner.Runtime.BehaviorTree
function GetComponent.BehaviorTree(go)
    return go:GetComponent(typeof(BehaviorDesigner.Runtime.BehaviorTree))
end

---获得 SeekerToLua 组件
---@param go UnityEngine.GameObject
---@return SeekerToLua
function GetComponent.SeekerToLua(go)
    return go:GetComponent(typeof(SeekerToLua))
end

---获得 MobileTouchCamera 组件
---@param go UnityEngine.GameObject
---@return BitBenderGames.MobileTouchCamera
function GetComponent.MobileTouchCamera(go)
    return go:GetComponent(typeof(BitBenderGames.MobileTouchCamera))
end

---获得 Volume 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Rendering.Volume
function GetComponent.Volume(go)
    return go:GetComponent(typeof(UnityEngine.Rendering.Volume))
end

---获得 AnimEvent 组件
---@param go UnityEngine.GameObject
---@return AnimEvent
function GetComponent.AnimEvent(go)
    return go:GetComponent(typeof(AnimEvent))
end

---获得 Renderer 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Renderer
function GetComponent.Renderer(go)
    return go:GetComponent(typeof(UnityEngine.Renderer))
end

---获得 MeshRenderer 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.MeshRenderer
function GetComponent.MeshRenderer(go)
    return go:GetComponent(typeof(UnityEngine.MeshRenderer))
end

---获得 DOTweenAnimation 组件
---@param go UnityEngine.GameObject
---@return DG.Tweening.DOTweenAnimation
function GetComponent.DOTweenAnimation(go)
    return go:GetComponent(typeof(DG.Tweening.DOTweenAnimation))
end

---获得 DOTweenVisualManager 组件
---@param go UnityEngine.GameObject
---@return DG.Tweening.DOTweenVisualManager
function GetComponent.DOTweenVisualManager(go)
    return go:GetComponent(typeof(DG.Tweening.DOTweenVisualManager))
end

---获得 UIParticle 组件
---@param go UnityEngine.GameObject
---@return Coffee.UIExtensions.UIParticle
function GetComponent.UIParticle(go)
    return go:GetComponent(typeof(Coffee.UIExtensions.UIParticle))
end

---获得 VideoPlayer 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.Video.VideoPlayer
function GetComponent.VideoPlayer(go)
    return go:GetComponent(typeof(UnityEngine.Video.VideoPlayer))
end

---获得 Slider 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.UI.Slider
function GetComponent.Slider(go)
    return go:GetComponent(typeof(UnityEngine.UI.Slider))
end

---获得 AudioSource 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.AudioSource
function GetComponent.AudioSource(go)
    return go:GetComponent(typeof(UnityEngine.AudioSource))
end

---获得 SkeletonGraphicToLua 组件
---@param go UnityEngine.GameObject
---@return SkeletonGraphicToLua
function GetComponent.SkeletonGraphicToLua(go)
    return go:GetComponent(typeof(SkeletonGraphicToLua))
end

---获得 ParticleSystem 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.ParticleSystem
function GetComponent.ParticleSystem(go)
    return go:GetComponent(typeof(UnityEngine.ParticleSystem))
end

---获得 GridLayoutGroup 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.UI.GridLayoutGroup
function GetComponent.GridLayoutGroup(go)
    return go:GetComponent(typeof(UnityEngine.UI.GridLayoutGroup))
end

---获得 BoxCollider 组件
---@param go UnityEngine.GameObject
---@return UnityEngine.BoxCollider
function GetComponent.BoxCollider(go)
    return go:GetComponent(typeof(UnityEngine.BoxCollider))
end

---获得 LookAtTarget 组件
---@param go UnityEngine.GameObject
---@return LookAtTarget
function GetComponent.LookAtTarget(go)
    return go:GetComponent(typeof(LookAtTarget))
end

---获得 ObjectFollower 组件
---@param go UnityEngine.GameObject
---@return ObjectFollower
function GetComponent.ObjectFollower(go)
    return go:GetComponent(typeof(ObjectFollower))
end

---获得 KeyFrameHandler 组件
---@param go UnityEngine.GameObject
---@return KeyFrameHandler
function GetComponent.KeyFrameHandler(go)
    return go:GetComponent(typeof(KeyFrameHandler))
end

return Game