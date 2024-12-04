﻿using UnityEngine;
using System;
using System.Collections.Generic;
using VivaFramework;
using UnityEditor;
using Prayer;
using HappyCode;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using BindType = ToLuaMenu.BindType;
using UnityEngine.UI;
using System.Reflection;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
 using BitBenderGames;
 using Coffee.UIExtensions;
 using DG.Tweening;
using DG.Tweening.Core;
 using DG.Tweening.Plugins.Options;
 using Pathfinding;
 using TMPro;
 using UnityEditor.Animations;
 using UnityEngine.AI;
using UnityEngine.Audio;
 using UnityEngine.Playables;
 using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
 using UnityEngine.Video;
 using Action = System.Action;
using Debugger = LuaInterface.Debugger;
 using Path = DG.Tweening.Plugins.Core.PathCore.Path;

public static class CustomSettings
{
    public static string FrameworkPath = AppConst.FrameworkRoot;
    public static string saveDir = FrameworkPath + "/ToLua/Source/Generate/";
	public static string luaDir = Application.dataPath + "/Lua";
    public static string toluaBaseType = FrameworkPath + "/ToLua/BaseType/";
    public static string injectionFilesPath = Application.dataPath + "/ToLua/Injection/";

    //导出时强制做为静态类的类型(注意customTypeList 还要添加这个类型才能导出)
    //unity 有些类作为sealed class, 其实完全等价于静态类
    public static List<Type> staticClassTypes = new List<Type>
    {        
        typeof(UnityEngine.Application),
        typeof(UnityEngine.Time),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.SleepTimeout),
        typeof(UnityEngine.Input),
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.Physics),
        typeof(UnityEngine.Random),
        typeof(UnityEngine.RenderSettings),
        typeof(UnityEngine.QualitySettings),
        typeof(UnityEngine.GL),
		typeof(UnityEngine.Graphics),
    };

    //附加导出委托类型(在导出委托时, customTypeList 中牵扯的委托类型都会导出， 无需写在这里)
    public static DelegateType[] customDelegateList = 
    {        
        _DT(typeof(Action)),                
        _DT(typeof(UnityEngine.Events.UnityAction)),
        _DT(typeof(System.Predicate<int>)),
        _DT(typeof(System.Action<int>)),
        _DT(typeof(System.Comparison<int>)),
        _DT(typeof(System.Func<int, int>)),
    };

    //在这里添加你要导出注册到lua的类型列表
    public static BindType[] customTypeList =
    {                
        //------------------------为例子导出--------------------------------
        //_GT(typeof(TestEventListener)),
        //_GT(typeof(TestProtol)),
        //_GT(typeof(TestAccount)),
        //_GT(typeof(Dictionary<int, TestAccount>)).SetLibName("AccountMap"),
        //_GT(typeof(KeyValuePair<int, TestAccount>)),
        //_GT(typeof(Dictionary<int, TestAccount>.KeyCollection)),
        //_GT(typeof(Dictionary<int, TestAccount>.ValueCollection)),
        //_GT(typeof(TestExport)),
        //_GT(typeof(TestExport.Space)),
        //-------------------------------------------------------------------        
                        
        _GT(typeof(Debugger)).SetNameSpace(null),          

        _GT(typeof(Camera)).AddExtendType (typeof(ShortcutExtensions)),
        _GT(typeof(Light)).AddExtendType (typeof(ShortcutExtensions)),
        _GT(typeof(LineRenderer)).AddExtendType (typeof(ShortcutExtensions)),
        _GT(typeof(Material)).AddExtendType (typeof(ShortcutExtensions)),
        _GT(typeof(Transform)).AddExtendType (typeof(ShortcutExtensions)),
        _GT(typeof(Component)).AddExtendType (typeof(ShortcutExtensions)),
        _GT(typeof(TrailRenderer)).AddExtendType(typeof(ShortcutExtensions)),

        _GT(typeof(CanvasGroup)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(Graphic)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(Image)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(LayoutElement)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(Outline)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(RectTransform)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(ScrollRect)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(Slider)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(Text)).AddExtendType (typeof(DOTweenModuleUI)),
        _GT(typeof(TextMeshPro)).AddExtendType (typeof(ShortcutExtensionsTMPText)),
        _GT(typeof(TextMeshProUGUI)).AddExtendType (typeof(ShortcutExtensionsTMPText)),
        _GT(typeof(TMP_InputField)).AddExtendType (typeof(ShortcutExtensionsTMPText)),

        _GT(typeof(AudioSource)).AddExtendType (typeof(DOTweenModuleAudio)),
        _GT(typeof(AudioMixer)).AddExtendType (typeof(DOTweenModuleAudio)),
        _GT(typeof(Rigidbody)).AddExtendType (typeof(DOTweenModulePhysics)),
        _GT(typeof(Rigidbody2D)).AddExtendType (typeof(DOTweenModulePhysics2D)),
        _GT(typeof(SpriteRenderer)).AddExtendType (typeof(DOTweenModuleSprite)),

        _GT(typeof(Tween)).SetBaseType (typeof(object)).AddExtendType (typeof(TweenExtensions)).AddExtendType (typeof(ShortcutExtensions)).AddExtendType (typeof(TweenSettingsExtensions)),
        _GT(typeof(DG.Tweening.Sequence)).AddExtendType (typeof(TweenSettingsExtensions)),
        _GT(typeof(Tweener)).AddExtendType (typeof(TweenSettingsExtensions)),

        _GT(typeof(TweenerCore<string, string, StringOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_String").SetLibName ("DG_Tweening_Plugins_Options_String"),
        _GT(typeof(TweenerCore<float, float, FloatOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Float").SetLibName ("DG_Tweening_Plugins_Options_Float"),
        _GT(typeof(TweenerCore<uint, uint, UintOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Uint").SetLibName ("DG_Tweening_Plugins_Options_Uint"),
        _GT(typeof(TweenerCore<Color, Color, ColorOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Color").SetLibName ("DG_Tweening_Plugins_Options_Color"),
        _GT(typeof(TweenerCore<Vector3, Vector3, VectorOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Vector").SetLibName ("DG_Tweening_Plugins_Options_Vector"),
        _GT(typeof(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Vector3Array").SetLibName ("DG_Tweening_Plugins_Options_Vector3Array"),
        _GT(typeof(TweenerCore<Vector3, Path, PathOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Path").SetLibName ("DG_Tweening_Plugins_Options_Path"),
        _GT(typeof(TweenerCore<Quaternion, Vector3, QuaternionOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Quaternion").SetLibName ("DG_Tweening_Plugins_Options_Quaternion"),
        _GT(typeof(TweenerCore<Rect, Rect, RectOptions>)).SetWrapName ("DG_Tweening_Plugins_Options_Rect").SetLibName ("DG_Tweening_Plugins_Options_Rect"),

        _GT(typeof(DOTween)),
        _GT(typeof(DOVirtual)),
        _GT(typeof(EaseFactory)),
        _GT(typeof(TweenParams)),
        _GT(typeof(ABSSequentiable)),
        _GT(typeof(DOTweenAnimation)),
        _GT(typeof(DOTweenVisualManager)),

        _GT(typeof(Ease)),
        _GT(typeof(LoopType)),
        _GT(typeof(PathMode)),
        _GT(typeof(PathType)),
        _GT(typeof(RotateMode)),
        _GT(typeof(AutoPlay)),
        _GT(typeof(AxisConstraint)),
        _GT(typeof(LogBehaviour)),
        _GT(typeof(ScrambleMode)),
        _GT(typeof(TweenType)),
        _GT(typeof(UpdateType)),
      
        _GT(typeof(Behaviour)),
        _GT(typeof(MonoBehaviour)),        
        _GT(typeof(GameObject)),
        _GT(typeof(TrackedReference)),
        _GT(typeof(Application)),
        _GT(typeof(Physics)),
        _GT(typeof(UnityEngine.Ray)),
        _GT(typeof(Collider)),
        _GT(typeof(Time)),        
        _GT(typeof(Texture)),
        _GT(typeof(Texture2D)),
        _GT(typeof(Shader)),        
        _GT(typeof(Renderer)),
        _GT(typeof(Screen)),        
        _GT(typeof(CameraClearFlags)),
        _GT(typeof(AudioClip)),        
        _GT(typeof(AssetBundle)),
        _GT(typeof(ParticleSystem)),
        _GT(typeof(AsyncOperation)).SetBaseType(typeof(System.Object)),        
        _GT(typeof(LightType)),
        _GT(typeof(SleepTimeout)),
        _GT(typeof(Canvas)),
        _GT(typeof(Scene)),
        _GT(typeof(AnimationCurve)),
        _GT(typeof(ParticleSystem.MinMaxCurve)),
        _GT(typeof(ParticleSystem.MinMaxGradient)),
        _GT(typeof(ParticleSystem.VelocityOverLifetimeModule)),
        _GT(typeof(ParticleSystem.MainModule)),
        _GT(typeof(ParticleSystemCurveMode)),
        _GT(typeof(TextAnchor)),
        
        
        
        
#if UNITY_5_3_OR_NEWER && !UNITY_5_6_OR_NEWER
        _GT(typeof(UnityEngine.Experimental.Director.DirectorPlayer)),
#endif
        _GT(typeof(Animator)),
        _GT(typeof(Input)),
        _GT(typeof(KeyCode)),
        _GT(typeof(SkinnedMeshRenderer)),
        _GT(typeof(Space)),    
        _GT(typeof(AnimatorClipInfo)), 
        _GT(typeof(AnimatorStateInfo)),  
        _GT(typeof(RuntimeAnimatorController)),
       

        _GT(typeof(MeshRenderer)),
#if !UNITY_5_4_OR_NEWER
        _GT(typeof(ParticleEmitter)),
        _GT(typeof(ParticleRenderer)),
        _GT(typeof(ParticleAnimator)), 
#endif

        _GT(typeof(BoxCollider)),
        _GT(typeof(MeshCollider)),
        _GT(typeof(SphereCollider)),        
        _GT(typeof(CharacterController)),
        _GT(typeof(CapsuleCollider)),
        
        _GT(typeof(Animation)),         
        _GT(typeof(AnimationClip)).SetBaseType(typeof(UnityEngine.Object)),        
        _GT(typeof(AnimationState)),
        _GT(typeof(AnimationBlendMode)),
        _GT(typeof(AnimEvent)),
        _GT(typeof(QueueMode)),  
        _GT(typeof(PlayMode)),
        _GT(typeof(WrapMode)),

        _GT(typeof(QualitySettings)),
        _GT(typeof(RenderSettings)),                                                   
        _GT(typeof(SkinWeights)),           
        _GT(typeof(RenderTexture)), 
		_GT(typeof(Resources)),      
		_GT(typeof(LuaProfiler)),
        _GT(typeof(PlayerPrefs)),
          
        //for VivaFramework
        _GT(typeof(RawImage)),
        _GT(typeof(Color)),
        _GT(typeof(ScreenCapture)),
        _GT(typeof(Sprite)),
        _GT(typeof(Rect)),
        _GT(typeof(RenderMode)),
        
        
        _GT(typeof(LoadSceneMode)),
        _GT(typeof(AssetBundleCreateRequest)),
        


        _GT(typeof(Util)),
        _GT(typeof(AppConst)),
        _GT(typeof(LuaHelper)),
        _GT(typeof(LuaBehaviour)),
        
        
        ///Behavior Designer
        _GT(typeof(BehaviorToLua)),
        _GT(typeof(BehaviorTree)),
        _GT(typeof(TaskStatus)),
        _GT(typeof(LuaAction)),
        _GT(typeof(ExternalBehaviorTree)),
        _GT(typeof(BehaviorManager)),
        _GT(typeof(UpdateIntervalType)),


        _GT(typeof(ManagerHandler)),
        _GT(typeof(OnDestroyHandler)),

        _GT(typeof(GameManager)),
        _GT(typeof(LuaManager)),
        _GT(typeof(NetworkManager)),
		_GT(typeof(ResourceManager)),	
		_GT(typeof(VivaFramework.SceneManager)),
		_GT(typeof(UnityEngine.SceneManagement.SceneManager)),
		_GT(typeof(HappyCamera)),
        _GT(typeof(HappyFuns)),
        _GT(typeof(PointerHandler)),
        _GT(typeof(PointerHandler3D)),
        _GT(typeof(PointerEventData)),
        _GT(typeof(DragHandler)),
        _GT(typeof(DragHandler3D)),
        _GT(typeof(UnityEngine.Random)),
        _GT(typeof(RectTransformUtility)),
        _GT(typeof(TextMesh)),
        _GT(typeof(TouchEvent)),
        _GT(typeof(AudioManager)),
        
        //URP相机相关
        _GT(typeof(Volume)),
        _GT(typeof(UniversalAdditionalCameraData)),
        _GT(typeof(CameraExtensions)),
        _GT(typeof(CameraRenderType)),
        _GT(typeof(VideoPlayer)),


        _GT(typeof(ABPath)),
        _GT(typeof(AstarPath)),
        _GT(typeof(PathLog)),
        _GT(typeof(AIPathToLua)),
        _GT(typeof(NNInfo)),
        _GT(typeof(Seeker)),
        _GT(typeof(GridGraph)),
        _GT(typeof(GraphNode)),
        _GT(typeof(GridNode)),
        _GT(typeof(Int3)),
        _GT(typeof(SeekerToLua)),
        _GT(typeof(MobileTouchCamera)),
        _GT(typeof(NNConstraint)),
        _GT(typeof(NNInfoInternal)),
        _GT(typeof(CloseToDestinationMode)),
        _GT(typeof(UIParticle)),
        _GT(typeof(TMPEffect)),
        _GT(typeof(PlayableDirector)),
        _GT(typeof(PlayableBinding)),
        _GT(typeof(GridLayoutGroup)),
        _GT(typeof(LookAtTarget)),
        _GT(typeof(KeyFrameHandler)),
        _GT(typeof(ObjectFollower)),
    };

    public static List<Type> dynamicList = new List<Type>()
    {
        typeof(MeshRenderer),
#if !UNITY_5_4_OR_NEWER
        typeof(ParticleEmitter),
        typeof(ParticleRenderer),
        typeof(ParticleAnimator),
#endif

        typeof(BoxCollider),
        typeof(MeshCollider),
        typeof(SphereCollider),
        typeof(CharacterController),
        typeof(CapsuleCollider),

        typeof(Animation),
        typeof(AnimationState),

        typeof(SkinWeights),
        typeof(RenderTexture),
        typeof(Rigidbody),
    };

    //重载函数，相同参数个数，相同位置out参数匹配出问题时, 需要强制匹配解决
    //使用方法参见例子14
    public static List<Type> outList = new List<Type>()
    {
        
    };
        
    //ngui优化，下面的类没有派生类，可以作为sealed class
    public static List<Type> sealedList = new List<Type>()
    {
        /*typeof(Transform),
        typeof(UIRoot),
        typeof(UICamera),
        typeof(UIViewport),
        typeof(UIPanel),
        typeof(UILabel),
        typeof(UIAnchor),
        typeof(UIAtlas),
        typeof(UIFont),
        typeof(UITexture),
        typeof(UISprite),
        typeof(UIGrid),
        typeof(UITable),
        typeof(UIWrapGrid),
        typeof(UIInput),
        typeof(UIScrollView),
        typeof(UIEventListener),
        typeof(UIScrollBar),
        typeof(UICenterOnChild),
        typeof(UIScrollView),        
        typeof(UIButton),
        typeof(UITextList),
        typeof(UIPlayTween),
        typeof(UIDragScrollView),
        typeof(UISpriteAnimation),
        typeof(UIWrapContent),
        typeof(TweenWidth),
        typeof(TweenAlpha),
        typeof(TweenColor),
        typeof(TweenRotation),
        typeof(TweenPosition),
        typeof(TweenScale),
        typeof(TweenHeight),
        typeof(TypewriterEffect),
        typeof(UIToggle),
        typeof(Localization),*/
    };

    public static BindType _GT(Type t)
    {
        return new BindType(t);
    }

    public static DelegateType _DT(Type t)
    {
        return new DelegateType(t);
    }    


//    [MenuItem("Lua/Attach Profiler", false, 151)]
    static void AttachProfiler()
    {
        if (!Application.isPlaying)
        {
            EditorUtility.DisplayDialog("警告", "请在运行时执行此功能", "确定");
            return;
        }

        LuaClient.Instance.AttachProfiler();
    }

//    [MenuItem("Lua/Detach Profiler", false, 152)]
    static void DetachProfiler()
    {
        if (!Application.isPlaying)
        {            
            return;
        }

        LuaClient.Instance.DetachProfiler();
    }
}
