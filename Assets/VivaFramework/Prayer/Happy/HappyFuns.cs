using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace HappyCode
{
    public static class HappyFuns
    {
        /// <summary>
        /// 查找当前场景的所有顶级对象
        /// </summary>
        public static GameObject[] FindRootObj(string name)
        {
            List<GameObject> resList = new List<GameObject>();
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                List<GameObject> rootObjects = new List<GameObject>();
                scene.GetRootGameObjects(rootObjects);
                for (int j = 0; j < rootObjects.Count; j++)
                {
                    if (rootObjects[j].name == name)
                    {
                        resList.Add(rootObjects[j]);
                    }
                }
            }
            return resList.ToArray();
        }

        /*
         * 指定相机截图
         */
        public static Texture2D ScreenShotCamera(Camera cam)
        {
            RenderTexture renTex = new RenderTexture(Screen.width, Screen.height, 16);

            cam.targetTexture = renTex;

            cam.Render();

            RenderTexture.active = renTex;

            //读取像素

            Texture2D tex = new Texture2D(Screen.width, Screen.height);

            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

            tex.Apply();

            //读取目标相机像素结束，渲染恢复原先的方式

            cam.targetTexture = null;

            RenderTexture.active = null;

            UnityEngine.Object.Destroy(renTex);

            return tex;
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

        public static int GetLayerMask(int layer)
        {
            return 1 << layer;
        }

        /*
         * 二进制 或
         */
        public static int Bit_Or(int bit1, int bit2)
        {
            return bit1 | bit2;
        }

        /*
         * 二进制 与
         */
        public static int Bit_And(int bit1, int bit2)
        {
            return bit1 & bit2;
        }
    }
}
