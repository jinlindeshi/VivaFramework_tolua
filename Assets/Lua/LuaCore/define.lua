

---@class Vector2:Vector2
---@field one Vector2 Vector2.New(1,1)
---@field zero Vector2 Vector2.New(0,0)
---@field x number
---@field y number

---@class Vector3:Vector3
---@field one Vector3 Vector3.New(1,1,1)
---@field zero Vector3 Vector3.New(0,0,0)
---@field x number
---@field y number
---@field z number

---@class Vector4:Vector4
---@field one Vector4 Vector3.New(1,1,1,1)
---@field zero Vector4 Vector3.New(0,0,0,0)
---@field x number
---@field y number
---@field z number
---@field w number

---@class Color:Color
---@field black Color 纯黑色。RGBA 为 (0, 0, 0, 1)。
---@field blue Color 纯蓝色。RGBA 为 (0, 0, 1, 1)。
---@field clear Color 完全透明。RGBA 为 (0, 0, 0, 0)。
---@field cyan Color 青色。RGBA 为 (0, 1, 1, 1)。
---@field gray Color 灰色。RGBA 为 (0.5, 0.5, 0.5, 1)。
---@field green Color 纯绿色。RGBA 为 (0, 1, 0, 1)。
---@field magenta Color 洋红色。RGBA 为 (1, 0, 1, 1)。
---@field red Color 纯红色。RGBA 为 (1, 0, 0, 1)。
---@field white Color 纯白色。RGBA 为 (1, 1, 1, 1)。
---@field yellow Color 黄色。RGBA 为 (1, 0.92, 0.016, 1)，但该颜色很好看！

Util = VivaFramework.Util;
AppConst = VivaFramework.AppConst;
LuaHelper = VivaFramework.LuaHelper;
ByteBuffer = VivaFramework.ByteBuffer;

resMgr = LuaHelper.GetResManager()
networkMgr = LuaHelper.GetNetManager()
sceneMgr = LuaHelper.GetSceneManager()
audioMgr = LuaHelper.GetAudioManager()
require("Module.Common.AudioHelper")
require("Module.Common.Res")
require("Data.Config.TypeInfo")
AudioConfig = require("Data.Config.AudioConfig")

PointerHandler = Prayer.PointerHandler
PointerHandler3D = Prayer.PointerHandler3D
PlayerPrefs = UnityEngine.PlayerPrefs


WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;
--Color = UnityEngine.Color;
Rect = UnityEngine.Rect;
Sprite = UnityEngine.Sprite;
Shader = UnityEngine.Shader;
Material = UnityEngine.Material;
Screen = UnityEngine.Screen;
Application = UnityEngine.Application;
ScreenCapture = UnityEngine.ScreenCapture;
Camera = UnityEngine.Camera;
SceneManager = UnityEngine.SceneManagement.SceneManager
Input = UnityEngine.Input
Physics = UnityEngine.Physics ---@type UnityEngine.Physics
Random = UnityEngine.Random
Time = UnityEngine.Time

DOTWEEN_EASE = DG.Tweening.Ease ---@type DG.Tweening.Ease
DOTWEEN_LOOP_TYPE = DG.Tweening.LoopType ---@type DG.Tweening.LoopType
DOTWEEN_ROTATE_MODE = DG.Tweening.RotateMode ---@type DG.Tweening.RotateMode

---Behavior Desiner
TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus

CameraExtensions = UnityEngine.Rendering.Universal.CameraExtensions ---@type UnityEngine.Rendering.Universal.CameraExtensions
CameraRenderType = UnityEngine.Rendering.Universal.CameraRenderType
RectTransformUtility = UnityEngine.RectTransformUtility
DOTween = DG.Tweening.DOTween
LuaObj = require("Prayer.Core.LuaObj")
LuaScene = require("Prayer.Core.LuaScene")
SceneUI = require("Prayer.Core.SceneUI")
AniEffectObj = require("Module.Common.View.AniEffectObj")
UIView = require("Prayer.Core.UIView")
---UI管理器
UIMgr = require("Manager/UIManager")
---Event管理器
EventMgr = require("Manager/EventManager")
Talk = require("Module.Common.View.Talk")
TalkerConfig = require("Module.Common.Model.TalkerConfig")
Message = require("Module.Common.View.Message")

Common = require("Module.Common.Model.Common")
require("Prayer.PrayerConstants")
Constants = require("Module.Common.Model.Constants")

PrefabConfig = require("Module.Common.Model.PrefabConfig")
CountDownTime = require("Prayer.Components.CountDownTime")
StoryManager = require("Module.Story.StoryManager")
UserData = require("Module.Common.Model.UserData")

GuideHand = require("Module.Common.View.GuideHand")
--ReadSData = require("Data.ReadSData")
ResConfig = require("Data.Config.ResConfig")

Alert = require("Module.Common.View.Alert")
