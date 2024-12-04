using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FPS : MonoBehaviour
{
    private float m_LastUpdateShowTime = 0f;  //上一次更新帧率的时间;  
    
    private float m_UpdateShowDeltaTime = 1f;//更新帧率的时间间隔;  

    private int m_FrameUpdate = 0;//帧数;  

    private float m_FPS = 0;

	private GUIStyle _style;
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization  
    void Start()
    {
        m_LastUpdateShowTime = Time.realtimeSinceStartup;
		_style = new GUIStyle();
		_style.fontSize = 40;
        _style.normal.textColor = Color.white;
    }

    // Update is called once per frame  
    void Update()
    {
        m_FrameUpdate++;
        if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
        {
            m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
            m_FrameUpdate = 0;
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
        }
    }

    void OnGUI()
    {
		GUI.Label(new Rect(Screen.width / 2, 0, 100, 100), "FPS: " + m_FPS, _style);
    }
}
