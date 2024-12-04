using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
/// 模拟光 对同一个物体暂时只支持 每种光源各一个
/// </summary>
public class SimulateLight : MonoBehaviour
{
    public enum LIGHT_TYPE
    {
        DIRECTION = 0,
        POINT = 1,
        SPOT = 2
    }

    /// <summary>
    /// 颜色
    /// </summary>
    public Color LightColor = Color.black;

    /// <summary>
    /// 光强
    /// </summary>
    public float Intensity = 1;

    public LIGHT_TYPE lightType;

    public int SpotLightAngle;
    public int SpotLightRange;

    public int PointLightRadius;

    [SerializeField]
    [Range(0f, 2f)]
    public float AttenFactor1 = 0;
    
    [SerializeField]
    [Range(-1f, 1f)]
    public float AttenFactor2 = 0;
    
    [SerializeField]
    [Range(1f, 1000f)]
    public float AttenFactor3 = 0;
    
    public List<Renderer> list = new List<Renderer>();


    
    public void Refresh()
    {
        if (enabled == false || gameObject.activeSelf == false)
        {
            Reset();
            return;
        }
//        print("Refresh " + list.Count + " - " + lightType);
        float setIntensity = Intensity;
        if (Intensity > 1)
        {
            setIntensity = (Intensity - 1) / 10 + 1;
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                continue;
            }
            Material m = list[i].sharedMaterial;

            Vector4 lightForward = new Vector4(transform.forward.x,
                transform.forward.y, transform.forward.z, 0);
            
           
            Vector4 lightWorldPos = new Vector4(transform.position.x, 
                transform.position.y, transform.position.z, 0);

            if (lightType == LIGHT_TYPE.DIRECTION)
            {
                m.SetVector("_DirectionForward", lightForward);
                m.SetFloat("_DirectionIntensity", setIntensity); 
                m.SetColor("_DirectionCol", LightColor);
            }
            else if(lightType == LIGHT_TYPE.POINT)
            {
                m.SetVector("_PointWorldPos", lightWorldPos);
                m.SetColor("_PointCol", LightColor);
                m.SetFloat("_PointIntensity", setIntensity); 
                
                m.SetInt("_PointLightRadius", PointLightRadius);
            }
            else if (lightType == LIGHT_TYPE.SPOT)
            {
                m.SetVector("_SpotWorldPos", lightWorldPos);
                m.SetVector("_SpotForward", lightForward);
                m.SetColor("_SpotCol", LightColor);
                m.SetFloat("_SpotIntensity", setIntensity); 
                
                m.SetInt("_SpotLightAngle", SpotLightAngle);
                m.SetInt("_SpotLightRange", SpotLightRange);
                
                m.SetFloat("_AttenFactor1", AttenFactor1);
                m.SetFloat("_AttenFactor2", AttenFactor2);
                m.SetFloat("_AttenFactor3", AttenFactor3);
            }
        }
    }

    public void Reset()
    {
//        print("Reset " + lightType);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                continue;
            }
            Material m = list[i].sharedMaterial;

            if (lightType == LIGHT_TYPE.DIRECTION)
            {
                m.SetFloat("_DirectionIntensity", 1); 
                m.SetColor("_DirectionCol", Color.black);
            }
            else if(lightType == LIGHT_TYPE.POINT)
            {
                m.SetColor("_PointCol", Color.black);
                m.SetFloat("_PointIntensity", 1); 
                
                m.SetInt("_PointLightRadius", PointLightRadius);
            }
            else if (lightType == LIGHT_TYPE.SPOT)
            {
                m.SetColor("_SpotCol", Color.black);
                m.SetFloat("_SpotIntensity", 1); 
                
                m.SetInt("_SpotLightAngle", SpotLightAngle);
                m.SetInt("_SpotLightRange", SpotLightRange);
            }
        }
    }

    private void OnEnable()
    {
        print("OnEnable");
        Refresh();
    }

    private void OnDisable()
    {
        print("OnDisable");
        Reset();
    }

    // Update is called once per frame
    void Start()
    {
        Refresh();
    }
    
    
#if UNITY_EDITOR
    
    private const float gizmos_radius = 0.3f;//半径
    private const float gizmos_distance = 1.6f;//投影距离
    void OnDrawGizmos()
    {
        Gizmos.color = LightColor;
        Gizmos.DrawIcon(transform.position, "PointLight Gizmo", true);

        switch (lightType)
        {
            case LIGHT_TYPE.DIRECTION:
                DrawDirectionGizmos();
                break;
            case LIGHT_TYPE.POINT:
                DrawPointGizmos(); 
                break;
            case LIGHT_TYPE.SPOT:
                DrawProjGizmos(); 
                break;
            default:
                DrawDirectionGizmos(); 
                break;
        }
    }

    void DrawDirectionGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 startPos1 = transform.position + transform.up * gizmos_radius;
        Gizmos.DrawLine(startPos1, startPos1 + transform.forward * gizmos_distance);

        Vector3 startPos2 = transform.position + -transform.up * gizmos_radius;
        Gizmos.DrawLine(startPos2, startPos2 + transform.forward * gizmos_distance);

        Vector3 startPos3 = transform.position + transform.right * gizmos_radius;
        Gizmos.DrawLine(startPos3, startPos3 + transform.forward * gizmos_distance);

        Vector3 startPos4 = transform.position + -transform.right * gizmos_radius;
        Gizmos.DrawLine(startPos4, startPos4 + transform.forward * gizmos_distance);

        Gizmos.DrawLine(startPos1, startPos3);
        Gizmos.DrawLine(startPos3, startPos2);
        Gizmos.DrawLine(startPos2, startPos4);
        Gizmos.DrawLine(startPos4, startPos1);
    }
    
    void DrawPointGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PointLightRadius);
    }
    
    void DrawProjGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 startPos = transform.position;
        
        float radius = Mathf.Sin(SpotLightAngle * (Mathf.PI / 180f)) * SpotLightRange;
        //float distance = Mathf.Cos(projLightAngle * (Mathf.PI / 180f)) / (radius + projLightDistance);
        float maxRadius = Mathf.Sin(SpotLightAngle * (Mathf.PI / 180f)) * (radius + SpotLightRange);
        Vector3 centerPos = startPos + transform.forward * (radius + SpotLightRange);

        Vector3 endPos1 = centerPos + transform.up * maxRadius;
        Gizmos.DrawLine(startPos, endPos1);

        Vector3 endPos2 = centerPos + -transform.up * maxRadius;
        Gizmos.DrawLine(startPos, endPos2);

        Vector3 endPos3 = centerPos + transform.right * maxRadius;
        Gizmos.DrawLine(startPos, endPos3);

        Vector3 endPos4 = centerPos + -transform.right * maxRadius;
        Gizmos.DrawLine(startPos, endPos4);

        //Gizmos.DrawWireSphere(centerPos, projLightRadius);

        DrawCyicle(6, maxRadius, centerPos);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPos, centerPos);
        Gizmos.DrawCube(centerPos,new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawLine(centerPos, endPos1);
        Gizmos.DrawLine(centerPos, endPos2);
        Gizmos.DrawLine(centerPos, endPos3);
        Gizmos.DrawLine(centerPos, endPos4);
    }

    void DrawCyicle(float delta,float radius, Vector3 centerPos)
    {
        Quaternion temp = transform.rotation;
        int num = Mathf.FloorToInt(360f / delta);
        Vector3 lastPoint = centerPos + transform.up * radius;
        for (int i = 0; i < num; i++)
        {
            transform.Rotate(Vector3.forward, delta);
            Vector3 point = centerPos + transform.up * radius;
            Gizmos.DrawLine(lastPoint, point);
            lastPoint = point;
        }
        transform.rotation = temp;
    }

#endif
}
