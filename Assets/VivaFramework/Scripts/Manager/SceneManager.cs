using System;
using System.Collections;
using LuaInterface;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace VivaFramework
{
	
	public class SceneManager:Manager
    {

	    public void LoadSceneAsync(string sceneName, LuaFunction callBack, LoadSceneMode mode)
		{
			print("SceneManager - LoadSceneAsync " + sceneName);
			StartCoroutine(LoadingScene(sceneName, callBack, mode));
		}
	    
	    public void UnLoadSceneAsync(Scene scene, LuaFunction callBack)
	    {
		    StartCoroutine(UnLoadingScene(scene, callBack));
	    }


		IEnumerator LoadingScene(string sceneName, LuaFunction callBack, LoadSceneMode mode)
		{
			AsyncOperation sceneLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, mode);
			yield return sceneLoad;

			Scene s = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
			UnityEngine.SceneManagement.SceneManager.SetActiveScene(s);
			if (callBack != null)
			{
				// Debug.Log("你妹啊~" + s);
				callBack.Call((object)s);
			}
		}
	    
	    IEnumerator UnLoadingScene(Scene scene, LuaFunction callBack)
	    {
		    AsyncOperation sceneUnload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
		    yield return sceneUnload;

		    if (callBack != null)
		    {
			    callBack.Call();
		    }
	    }
    }
}
