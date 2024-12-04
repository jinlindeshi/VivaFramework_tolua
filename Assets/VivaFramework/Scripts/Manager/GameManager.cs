﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
using UnityEngine.SceneManagement;
using System.IO;
using System.Net;
 using DG.Tweening;
 using Prayer;
 using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


 namespace VivaFramework
{
    public class GameManager : Manager
    {

        void Start()
        {
            
            
            DontDestroyOnLoad(gameObject); //防止销毁自己


            CheckExtractResource(); //释放资源
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = AppConst.GameFrameRate;
            // DG.Tweening.DOTween.SetTweensCapacity(800,200);
        }

        
        /// <summary>
        /// 创建进度条
        /// </summary>
        public IEnumerator CreateProgressUI()
        {
            
            ResourceRequest req = Resources.LoadAsync<GameObject>("UpdateCanvas");
            yield return req;
//                        print("UpdateCanvas加载完毕" + req.asset + " " +req.isDone);
            GameObject canvas = GameObject.Instantiate((GameObject)req.asset);
            _updateLoadingUI = canvas.transform.Find("UpdateLoading").gameObject;
            _updateLoadingUI.SetActive(true);
        }
        
        
        /// <summary>
        /// 释放资源
        /// </summary>
        public void CheckExtractResource()
        {
            print("CheckExtractResource " + Util.DataPath);
            print("Version Check - " + PlayerPrefs.GetString("lastVersion") + " - " + Application.version);
            //更新模式从网上下资源
            if (AppConst.UpdateMode == true)
            {
                StartCoroutine(OnUpdateResource());
            }
            //为了测试方便 兼容本地模式 从本地解压资源
            else if(AppConst.UseBundle == true)
            {
                bool isExists = Directory.Exists(Util.DataPath) &&
                                Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt");
                if(isExists && PlayerPrefs.GetString("lastVersion") == Application.version)
                {
                    OnResourceInited();
                    return; //文件已经解压过了，自己可添加检查文件列表逻辑
                }

                if (Directory.Exists(Util.DataPath) == true)
                {
                    Directory.Delete(Util.DataPath, true);
                }
                StartCoroutine(OnExtractResource()); //启动释放协成
            }
            else
            {
                OnResourceInited();
            }
        }

        IEnumerator OnExtractResource()
        {
            string dataPath = Util.DataPath; //数据目录
            string resPath = Util.AppContentPath(); //游戏包资源目录

            print("OnExtractResource " + dataPath + " " + resPath);

            if (Directory.Exists(dataPath)) Directory.Delete(dataPath, true);
            Directory.CreateDirectory(dataPath);

            string infile = resPath + "files.txt";
            string outfile = dataPath + "files.txt";
            if (File.Exists(outfile)) File.Delete(outfile);

            Debug.Log(infile);
            Debug.Log(outfile);
            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone)
                {
                    File.WriteAllBytes(outfile, www.bytes);
                }

                yield return 0;
            }
            else File.Copy(infile, outfile, true);

            yield return new WaitForEndOfFrame();

            //释放所有文件到数据目录
            string[] files = File.ReadAllLines(outfile);
            
            
            yield return CreateProgressUI();
            for (int i = 0; i < files.Length - 1; i++)
            {
                string[] fs = files[i].Split('|');
                infile = resPath + fs[0]; //
                outfile = dataPath + fs[0];
                
                _extractProgress = (float)i/(float)files.Length;
                _extractFileName = fs[0];

                Debug.Log("正在解包文件:>" + infile + " - " + i + " - " + files.Length + " - " + _extractProgress);

                string dir = Path.GetDirectoryName(outfile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (Application.platform == RuntimePlatform.Android)
                {
                    WWW www = new WWW(infile);
                    yield return www;

                    if (www.isDone)
                    {
                        File.WriteAllBytes(outfile, www.bytes);
                    }

                    yield return 0;
                }
                else
                {
                    if (File.Exists(outfile))
                    {
                        File.Delete(outfile);
                    }

                    File.Copy(infile, outfile, true);
                }

                
                yield return new WaitForEndOfFrame();
            }

            _extractProgress = 1;
            yield return new WaitForSeconds(0.2f);
            
            if (_updateLoadingUI != null)
            {
                _updateLoadingUI.transform.parent.gameObject.SetActive(false);
                _updateLoadingUI = null;
            }
            
            PlayerPrefs.SetString("lastVersion", Application.version);
            PlayerPrefs.Save();
            OnResourceInited();
        }

        /// <summary>
        /// 启动更新下载
        /// </summary>
        IEnumerator OnUpdateResource()
        {
            if (!AppConst.UseBundle)
            {
                OnResourceInited();
                yield break;
            }

            string dataPath = Util.DataPath; //数据目录
            string url = "http://" + AppConst.GameServerIP + "/";
#if UNITY_EDITOR
            url = "http://127.0.0.1/";
#endif
            string random = DateTime.Now.ToString("yyyymmddhhmmss");
            // string listUrl = url + "files.txt?v=" + random;
            string listUrl = url + "files.txt";
            Debug.Log("LoadUpdate---->>>" + listUrl);

            // WWW www = new WWW(listUrl);
            UnityWebRequest uwr = UnityWebRequest.Get(listUrl);
            uwr.timeout = 3;
            yield return uwr.SendWebRequest();
            if (uwr.isHttpError || uwr.isNetworkError)
            {
                Debug.LogError("OnUpdateResource更新失败!>" + uwr.error);
                yield break;
            }

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            // Debug.LogWarning("下载完毕 " + uwr.isDone + " - " + uwr.downloadHandler);
            File.WriteAllBytes(dataPath + "files.txt", uwr.downloadHandler.data);
            string filesText = uwr.downloadHandler.text;
            string[] files = filesText.Split('\n');

            //TEST 测试更新界面的可见
//            ResourceRequest reqq = Resources.LoadAsync<GameObject>("UpdateCanvas");
//            yield return reqq;
//            GameObject canvass = GameObject.Instantiate((GameObject)reqq.asset);
//            GameObject g = canvass.transform.Find("UpdateLoading").gameObject;
//            g.SetActive(true);
//            yield return new WaitForSeconds(200);
            //TEST
            
            
            for (int i = 0; i < files.Length; i++)
            {
                if (string.IsNullOrEmpty(files[i])) continue;
                string[] keyValue = files[i].Split('|');
                string f = keyValue[0];
                string localfile = (dataPath + f).Trim();
                string path = Path.GetDirectoryName(localfile);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileUrl = url + f + "?v=" + random;
                bool canUpdate = !File.Exists(localfile);
                if (!canUpdate)
                {
                    string remoteMd5 = keyValue[1].Trim();
                    string localMd5 = Util.md5file(localfile);
                    canUpdate = !remoteMd5.Equals(localMd5);
                    if (canUpdate) File.Delete(localfile);
                    Debug.Log("OnUpdateResource - " + f + " - " + remoteMd5 + " - " + localMd5);
                }

                if (canUpdate)
                {
                    if (_updateLoadingUI == null)
                    {
                        yield return CreateProgressUI();
                    }
                    
                    //本地缺少文件
                    Debug.Log("OnUpdateResource更新的文件 - " + fileUrl + " - " + localfile);
                    
                    _updateRequest = new WWW(fileUrl);
                    _updateFileName = f;
                    yield return _updateRequest;
                    if (_updateRequest.error != null) {
                        Debug.LogError("OnUpdateResource更新失败!>" + path);
                        yield break;
                    }
                    File.WriteAllBytes(localfile, _updateRequest.bytes);
                    _updateRequest = null;
                    Debug.Log("OnUpdateResource更新完毕 - " + fileUrl + " - " + localfile);
                     
                }
            }

            yield return new WaitForEndOfFrame();

            if (_updateLoadingUI != null)
            {
                _updateLoadingUI.transform.parent.gameObject.SetActive(false);
                _updateLoadingUI = null;
            }

            OnResourceInited();
        }


        private WWW _updateRequest;
        private string _updateFileName;
        private GameObject _updateLoadingUI;

        private string _extractFileName;
        private float _extractProgress = 0;
        private void Update()
        {
            if (_updateLoadingUI == null) return;
            
//            print("你妹啊 "+ _extractFileName + _extractProgress);
            if (_updateRequest != null)
            {
                Text infoText = _updateLoadingUI.transform.Find("bg/text").gameObject.GetComponent<Text>();
                Image barImg = _updateLoadingUI.transform.Find("bg/bar").gameObject.GetComponent<Image>();
                barImg.fillAmount = _updateRequest.progress;
                infoText.text = _updateFileName + " " + Math.Floor(_updateRequest.progress * 100) + "%";
            }
            else if (_extractProgress > 0)
            {
                Text infoText = _updateLoadingUI.transform.Find("bg/text").gameObject.GetComponent<Text>();
                Image barImg = _updateLoadingUI.transform.Find("bg/bar").gameObject.GetComponent<Image>();
                barImg.fillAmount = _extractProgress;
                infoText.text = "解压中：" + _extractFileName + " " + Math.Floor(_extractProgress * 100) + "%";
            }
            
            
        }


        /// <summary>
        /// 资源初始化结束
        /// </summary>
        public void OnResourceInited()
        {
            ResManager.Initialize();
            this.OnInitialize();
        }

        void OnInitialize()
        {
            LuaManager.InitStart();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        void OnDestroy()
        {
            if (NetManager != null)
            {
                NetManager.Unload();
            }

            if (LuaManager != null)
            {
                LuaManager.Close();
            }

            Debug.Log("~GameManager was destroyed");
        }

        /// <summary>
        /// 游戏重启 </summary>
        public void ReLaunch()
        {
            StartCoroutine(DoRelaunch());
        }

        IEnumerator DoRelaunch()
        {
            yield return new WaitForEndOfFrame();
            DOTween.KillAll();
            DestroyImmediate(gameObject);
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
            for (int n = 0; n < sceneCount; ++n)
            {
                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(n);
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
            }

            LuaHelper.GetLuaManager().Close();
            LuaHelper.GetResManager().UnloadAllBundles();
            AppFacade.Destroy();
            TouchEvent.Clear();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Enter");
        }
    }
}