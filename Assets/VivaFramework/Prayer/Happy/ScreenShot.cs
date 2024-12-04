using UnityEngine;

using UnityEditor;

using System.Collections;

//截屏
public class ScreenShot : MonoBehaviour {

    public Camera targetCamera;

    // Update is called once per frame

    void Update () {

        //鼠标右键按下，进行三种截图方式的调用

        if (Input.GetMouseButtonDown(0)) {

            ScreenShot_ScreenCapture ();

            StartCoroutine (ScreenShot_ReadPixels());

            targetCamera.gameObject.SetActive (true);

            ScreenShot_ReadPixelsWithCamera (targetCamera);

            targetCamera.gameObject.SetActive (false);

            //刷新工程目录，显示保存的图片，这里可以不要，改为手动即可

            //AssetDatabase.Refresh ();

        }

    }

            //Unity自带的截图功能

    private void ScreenShot_ScreenCapture(){

        //截屏并保存

        ScreenCapture.CaptureScreenshot (Application.dataPath + "/ScreenShot_ScreenCapture.png");

    }

    //读取屏幕像素进行截图

    private IEnumerator ScreenShot_ReadPixels(){

        yield return new WaitForEndOfFrame ();

        //读取像素

        Texture2D tex = new Texture2D(Screen.width, Screen.height);

        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height),0,0);

        tex.Apply();

        //保存读取的结果

        string path = Application.dataPath + "/ScreenShot_ReadPixels.png";

        System.IO.File.WriteAllBytes(path, tex.EncodeToPNG());

    }

    //读取指定相机渲染的像素

    private void ScreenShot_ReadPixelsWithCamera(Camera _camera){

        //对指定相机进行 RenderTexture

        RenderTexture renTex = new RenderTexture(Screen.width, Screen.height, 16);

        _camera.targetTexture = renTex;

        _camera.Render();

        RenderTexture.active = renTex;

        //读取像素

        Texture2D tex = new Texture2D(Screen.width, Screen.height);

        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height),0,0);

        tex.Apply();

        //读取目标相机像素结束，渲染恢复原先的方式

        _camera.targetTexture = null;

        RenderTexture.active = null;

        Destroy (renTex);

        //保存读取的结果

        string path = Application.dataPath + "/ScreenShot_ReadPixelsWithCamera.png";

        System.IO.File.WriteAllBytes(path, tex.EncodeToPNG());

    }

}