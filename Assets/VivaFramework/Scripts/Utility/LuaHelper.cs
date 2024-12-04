using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using Object = UnityEngine.Object;

namespace VivaFramework {
    public static class LuaHelper {

        /// <summary>
        /// getType
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static System.Type GetType(string classname) {
            Assembly assb = Assembly.GetExecutingAssembly();  //.GetExecutingAssembly();
            System.Type t = null;
            t = assb.GetType(classname);
            if (t == null) {
                t = assb.GetType(classname);
            }
            return t;
        }

        /// <summary>
        /// 资源管理器
        /// </summary>
        public static ResourceManager GetResManager() {
            return AppFacade.Instance.GetManager<ResourceManager>(ManagerName.Resource);
        }

        /// <summary>
        /// 网络管理器
        /// </summary>
        public static NetworkManager GetNetManager() {
            return AppFacade.Instance.GetManager<NetworkManager>(ManagerName.Network);
        }

        /// <summary>
        /// 网络管理器
        /// </summary>
        public static LuaManager GetLuaManager() {
            return AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua);
        }
        
        /// <summary>
        /// 场景管理器
        /// </summary>
		public static SceneManager GetSceneManager() {
			return AppFacade.Instance.GetManager<SceneManager>(ManagerName.Scene);
		}

        /// <summary>
        /// 音效管理器
        /// </summary>
        public static AudioManager GetAudioManager() {
            return AppFacade.Instance.GetManager<AudioManager>(ManagerName.Audio);
        }
        
        /// <summary>
        /// pbc/pblua函数回调
        /// </summary>
        /// <param name="func"></param>
        public static void OnCallLuaFunc(LuaByteBuffer data, LuaFunction func) {
            if (func != null) func.Call(data);
            Debug.LogWarning("OnCallLuaFunc length:>>" + data.buffer.Length);
        }

        /// <summary>
        /// cjson函数回调
        /// </summary>
        /// <param name="data"></param>
        /// <param name="func"></param>
        public static void OnJsonCallFunc(string data, LuaFunction func) {
            Debug.LogWarning("OnJsonCallback data:>>" + data + " lenght:>>" + data.Length);
            if (func != null) func.Call(data);
        }


        /// <summary>
        /// 检查是否点击到ui控件
        /// </summary>
        /// <returns><c>true</c>, if ray over object was checked, <c>false</c> otherwise.</returns>
        public static bool CheckRayOverObject()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        /// <summary>
        /// 3d射线
        /// </summary>
        /// <param name="position">Position.</param>
        public static RaycastHit Raycast(Vector3 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            return hit;
        }
        /// <summary>
        /// 2d射线
        /// </summary>
        /// <returns>The d.</returns>
        /// <param name="guiCamera">GUI camera.</param>
        public static RaycastHit2D Raycast2D(Camera guiCamera)
        {
            RaycastHit2D hit = Physics2D.Raycast(guiCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            return hit;
        }

        /// <summary>
        /// C#对象判空
        /// </summary>
        /// <returns><c>true</c>, if is null was objected, <c>false</c> otherwise.</returns>
        /// <param name="o">O.</param>
        public static bool ObjectIsNull(object o)
        {
            if (o == null || o.Equals(null))
                return true;
            return false;
        }

        /// <summary>
        /// 初始化预设
        /// </summary>
        /// <param name="original">Original.</param>
        public static UnityEngine.Object Instantiate(UnityEngine.Object original)
        {
            return UnityEngine.Object.Instantiate(original);
        }

        /// <summary>
        /// 初始化预设
        /// </summary>
        /// <param name="original">Original.</param>
        /// <param name="parent">Parent.</param>
        public static UnityEngine.Object Instantiate(UnityEngine.Object original, Transform parent)
        {
            return UnityEngine.Object.Instantiate(original, parent);
        }


        /// <summary>
        ///从世界坐标转换成画布坐标
        /// </summary>
        /// <returns>The to user interface point.</returns>
        /// <param name="canvas">Canvas.</param>
        /// <param name="worldGo">World go.</param>
        public static Vector3 WorldToCanvasPoint(Canvas canvas, Vector3 position)
        {
            Vector3 v_v3 = Camera.main.WorldToScreenPoint(position);
            Vector3 v_ui = canvas.worldCamera.ScreenToWorldPoint(v_v3);
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector3 scale = canvasRect.localScale;
            Vector3 v_new = new Vector3(v_ui.x / scale.x, v_ui.y / scale.y, canvasRect.anchoredPosition3D.z);
            return v_new;
        }

        /// <summary>ray
        /// 世界坐标转换成屏幕坐标
        /// </summary>
        /// <returns>The to screen point.</returns>
        /// <param name="position">Position.</param>
        public static Vector3 WorldToScreenPoint(Vector3 position)
        {
            return Camera.main.WorldToScreenPoint(position);
        }
        /// <summary>
        /// 屏幕坐标转换到世界坐标
        /// </summary>
        /// <returns>The to world point.</returns>
        /// <param name="position">Position.</param>
        public static Vector3 ScreenToWorldPoint(Vector3 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }
        
        /// <summary>
        /// 粒子系统的播放总时长
        /// </summary>
        /// <returns>The system length.</returns>
        /// <param name="transform">Transform.</param>
        public static float ParticleSystemLength(ParticleSystem[] particleSystems)
        {
            float maxDuration = 0;
            foreach (ParticleSystem ps in particleSystems)
            {
                if (ps.emission.enabled)
                {
                    if (ps.loop)
                    {
                        return -1f;
                    }
                    float dunration = 0f;
                    if (ps.emissionRate <= 0)
                    {
                        dunration = ps.startDelay + ps.startLifetime;
                    }
                    else
                    {
                        dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
                    }
                    if (dunration > maxDuration)
                    {
                        maxDuration = dunration;
                    }
                }
            }
            return maxDuration;
        }

        /// <summary>
        /// 获取节点及其子节点内的所有粒子系统
        /// </summary>
        /// <returns>The all particle system.</returns>
        /// <param name="go">Go.</param>
        public static ParticleSystem[] GetAllParticleSystem(GameObject go)
        {
            ParticleSystem[] childrenPSArray = go.GetComponentsInChildren<ParticleSystem>();
            ParticleSystem ps = go.GetComponent<ParticleSystem>();

            int length = childrenPSArray.Length;
            if (ps != null)
            {
                length++;
            }
            ParticleSystem[] psArray = new ParticleSystem[length];
            for (int i = 0; i < childrenPSArray.Length; i++)
            {
                psArray[i] = childrenPSArray[i];
            }
            if (ps != null)
            {
                psArray[childrenPSArray.Length] = ps;
            }
            return psArray;
        }


        /**
         * 根据名字获取timeline绑定轨道
         */
        public static PlayableBinding GetPlayableBindingByName(PlayableDirector playableDir, string name)
        {
            PlayableBinding res = new PlayableBinding();
            foreach (var item in playableDir.playableAsset.outputs)
            {
                if (item.streamName == name)
                {
                    res = item;
                    break;
                }
            }
            return res;
        }


        /// <summary>
        /// 查找第一个名称相符的节点，包括父节点
        /// </summary>
        /// <param go="go">节点对象</param>
        /// <param name="name">需要查找的节点名称</param>
        /// <returns>目标节点.</returns>
        public static GameObject GetChildByName(GameObject go, string name)
        {
            if (go.name == name)
                return go;
            Transform[] transArray = go.GetComponentsInChildren<Transform>();
            for (int i = 0; i < transArray.Length; i++)
            {
                GameObject obj = transArray[i].gameObject;
                if (obj.name == name)
                    return obj;
            }
            return null;
        }

        /// <summary>
        /// 设置横屏支持旋转
        /// </summary>
        public static void SetAutoRotationLandscape()
        {
            // 设置屏幕自动旋转， 并置支持的方向
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
        }

        /// <summary>
        /// 设置竖屏支持旋转
        /// </summary>
        public static void SetAutoRotationPortrait()
        {
            // 设置屏幕自动旋转， 并置支持的方向
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
        }

        /// <summary>
        /// 字符串全部替换
        /// </summary>
        /// <returns>The relace all.</returns>
        /// <param name="origin">String.</param> 源字符串
        /// <param name="replace">Replace.</param> 被替换的字符串
        /// <param name="target">Target.</param> 替换的字符串
        public static string StringRelaceAll(string origin, string replace, string target)
        {
            StringBuilder sb = new StringBuilder(origin);
            sb.Replace(replace, target);
            return sb.ToString();
        }
        
        /// <summary>
        ///  设置对象Layer(包含子对象)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="layer"></param>
        public static void SetLayerRecursive(this GameObject o, int layer)
        {
            SetLayerInternal(o.transform, layer);
        }

        private static void SetLayerInternal(Transform t, int layer)
        {
            t.gameObject.layer = layer;

            foreach (Transform o in t)
            {
                SetLayerInternal(o, layer);
            }
        }

        /// <summary>
        /// 获取层的二进制
        /// </summary>
        public static int GetLayerMask(int layer) {
            return 1 << layer;
        }
        /// <summary>
        /// 二进制_或
        /// </summary>
        public static int Bit_Or(int bit1, int bit2)
        {
            return bit1 | bit2;
        }
        /// <summary>
        /// 二进制_与
        /// </summary>
        public static int Bit_And(int bit1, int bit2)
        {
            return bit1 & bit2;
        }
        /// <summary>
        /// 获取ping值
        /// </summary>
        public static Ping NewPing(string host)
        {
            return new Ping(host);
        }
        
        /// <summary>
        /// 获取一个世界坐标点到指定Collider边界框之间的最小距离
        /// </summary>
        public static double ColliderDistance(Collider co, Vector3 pos)
        {
            return Math.Sqrt(co.bounds.SqrDistance(pos));
        }

        
        /// <summary>
        /// 设置对象不被销毁
        /// </summary>
        public static void SetDontDestroyOnLoad(GameObject go)
        {
            Object.DontDestroyOnLoad(go);
        }



        /// <summary>
        /// 画模糊Texture的开关
        /// </summary>
        public static bool blurDrawing;

        public static float blurRange = 2;
        
        public static void DrawBlurTextureToggle(bool enabled, float range = -1)
        {
            blurDrawing = enabled;
            if (range >= 0)
            {
                blurRange = range;
            }
        }

        /***
         * 把一个Overlay模式相机 加入 指定Base模式相机的栈列表
         */
        public static bool AddCameraToStackList(Camera baseCam, Camera overlayCam, bool checkRepeat = true)
        {
            if (baseCam == null || overlayCam == null)
            {
                return false;
            }
            UniversalAdditionalCameraData baseData = CameraExtensions.GetUniversalAdditionalCameraData(baseCam);
            if (baseData.renderType != CameraRenderType.Base)
            {
                return false;
            }
            UniversalAdditionalCameraData overlayData = CameraExtensions.GetUniversalAdditionalCameraData(overlayCam);
            if (overlayData.renderType != CameraRenderType.Overlay)
            {
                return false;
            }

            List<Camera> cameraStack = baseData.cameraStack;
            if (checkRepeat == true)
            {
                for (int i = 0; i < cameraStack.Count - 1; i++)
                {
                    if (cameraStack[i] == overlayCam)
                    {
                        return false;
                    }
                }
            }
            cameraStack.Add(overlayCam);
            return true;
        }
        
        /***
         * 把一个相机从指定Base模式相机的栈列表 删除
         */
        public static bool RemoveCameraFromStackList(Camera baseCam, Camera overlayCam)
        {
            
            if (baseCam == null || overlayCam == null)
            {
                return false;
            }
            UniversalAdditionalCameraData baseData = CameraExtensions.GetUniversalAdditionalCameraData(baseCam);
            if (baseData.renderType != CameraRenderType.Base)
            {
                return false;
            }
            
            List<Camera> cameraStack = baseData.cameraStack;
            return cameraStack.Remove(overlayCam);
        }
    }
}