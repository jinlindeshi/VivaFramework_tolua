using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

/// <summary>
/// TMP文字效果
/// </summary>
public class TMPEffect : MonoBehaviour
{
    private TMP_Text tmpComponent; 
    private int VertexCount = 4; //字符定点数量
    private Dictionary<int, Vector3[]> orgVectorDic; //记录初始顶点信息
    public Sequence fadingSequence; //渐变效果Sequence  效果完成后值为null
    private void Start()
    {
        tmpComponent = GetComponent<TMP_Text>();
        if (tmpComponent is null)
        {
            Debug.LogError("TMPEffect 没有找到TMP_Text组件");
        }

        //PlayScaleEffect(2, 1, 1.5f);
        //PlayWaveEffect(2, 20);
    }
    
    //设置字符的顶点数据
    private void SetCharacterVertices(TMP_CharacterInfo info,  Func<int, Vector3> fun)
    {
        TMP_TextInfo textInfo = tmpComponent.textInfo;
        Vector3[] verts = textInfo.meshInfo[info.materialReferenceIndex].vertices;
        for (int j = 0; j < VertexCount; j++)
        {
            verts[info.vertexIndex + j] = fun(j);
        }
    }

    //设置字符的alpha值
    private void SetCharacterAlpha(TMP_CharacterInfo info, byte alpha)
    {
        TMP_TextInfo textInfo = tmpComponent.textInfo;
        Color32[] colors = textInfo.meshInfo[info.materialReferenceIndex].colors32;
        for (int j = 0; j < VertexCount; j++)
        {
            colors[info.vertexIndex + j].a = alpha;
        }
    }

    //重新记录初始顶点信息
    public void ResetOrgVector()
    {
        tmpComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpComponent.textInfo;
        orgVectorDic = new Dictionary<int, Vector3[]>(); 
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo info = textInfo.characterInfo[i];
            Vector3[] verts = textInfo.meshInfo[info.materialReferenceIndex].vertices;
            Vector3[] orgList = new Vector3[VertexCount];
            for (int j = 0; j < VertexCount; j++)
            {
                Vector3 org = verts[info.vertexIndex + j];
                orgList[j] = org;
            }
            orgVectorDic.Add(i, orgList);
        }
    }
    
    //文字起伏效果
    public void PlayWaveEffect(float time, float moveY, float waveY = 0.5f)
    {
        if (tmpComponent is null) return;
        if (orgVectorDic is null) ResetOrgVector();
        TMP_TextInfo textInfo = tmpComponent.textInfo;
        TMP_CharacterInfo[] chaInfo = textInfo.characterInfo;
        int count = textInfo.characterCount; //字符长度
        float singleTime = time / count / 2; //单个字符的动画时间
        float[] wave = {1,waveY,waveY,1}; //起伏程度
        
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < count; i++)
        {
            TMP_CharacterInfo info = chaInfo[i];
            if (!info.isVisible) continue;//不显示字符 跳过
            Vector3[] orgVerts; //原顶点数据
            orgVectorDic.TryGetValue(i, out orgVerts);
            DOSetter<float> setValue = (value) =>
            {
                SetCharacterVertices(info, (index) =>
                {
                    Vector3 org = orgVerts[index];
                    float y = moveY * wave[index] * value;
                    return org + new Vector3(0, y, 0);
                });
                tmpComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
            };
            seq.Append(DOTween.To(setValue, 0, 1, singleTime));
            seq.Append(DOTween.To(setValue, 1, 0, singleTime));
            seq.AppendCallback(() => //回到初始状态
            {
                SetCharacterVertices(info, (index) => { return orgVerts[index]; });
                tmpComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
            });
        }
    }

    //文字逐个放大或缩小效果
    // public void PlayScaleEffect(float singleTime, float gapTime, float scale)
    // {
    //     if (tmpComponent is null) return;
    //     if (orgVectorDic is null) ResetOrgVector();
    //     TMP_TextInfo textInfo = tmpComponent.textInfo;
    //     TMP_CharacterInfo[] chaInfo = textInfo.characterInfo;
    //
    //     Sequence seq = DOTween.Sequence();
    //     for (int i = 0; i < textInfo.characterCount; i++)
    //     {
    //         TMP_CharacterInfo info = chaInfo[i];
    //         if (!info.isVisible) continue;//不显示字符 跳过
    //         Vector3[] verts = textInfo.meshInfo[info.materialReferenceIndex].vertices;
    //         float canterX = (info.topRight.x - info.topLeft.x) / 2;
    //         float canterY = (info.bottomLeft.y - info.topLeft.y) / 2;
    //         Vector3 center = new Vector3(canterX, canterY, info.topRight.z); //字符中心点坐标
    //        
    //         DOSetter<float> setValue = (value) =>
    //         {
    //             verts[info.vertexIndex + 0] = info.bottomLeft + (info.bottomLeft - center) * (scale - 1) * value;
    //             verts[info.vertexIndex + 1] = info.topLeft + (info.topLeft - center) * (scale - 1) * value;
    //             verts[info.vertexIndex + 2] = info.topRight + (info.topRight - center) * (scale - 1) * value;
    //             verts[info.vertexIndex + 3] = info.bottomRight + (info.bottomRight - center) * (scale - 1) * value;
    //             tmpComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    //         };
    //         seq.Append(DOTween.To(setValue, 0, 1, singleTime).SetEase(Ease.Linear));
    //         seq.Append(DOTween.To(setValue, 1, 0, singleTime).SetEase(Ease.Linear));
    //         seq.AppendInterval(gapTime);
    //     }
    // }

    //渐显打字效果
    public void PlayFadingType(string text, float singleTime)
    {
        tmpComponent = GetComponent<TMP_Text>();
        if (tmpComponent is null) return;

        if (fadingSequence != null)
        {
            fadingSequence.Kill();
        }
        fadingSequence = DOTween.Sequence();
        if (text.Length > 0)
        {
            tmpComponent.SetText(text);
            tmpComponent.ForceMeshUpdate();
        }
        TMP_TextInfo textInfo = tmpComponent.textInfo;
        TMP_CharacterInfo[] chaInfo = textInfo.characterInfo;
        if (chaInfo.Length == 0)
        {
            Debug.LogError("PlayFadingType 字符数为0");
            return;
        }
        // 先把alpha设为0
        for (int i = 0; i < chaInfo.Length; i++)
        {
            SetCharacterAlpha(chaInfo[i], 0);
        }
        tmpComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        
        //逐个字符显示
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo info = chaInfo[i];
            if (!info.isVisible) continue;
            fadingSequence.Append(DOTween.To((value) =>
            {
                SetCharacterAlpha(info, (byte)value);
                tmpComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }, 0, 255, singleTime).SetEase(Ease.Linear));
        }
        fadingSequence.AppendCallback(() => 
        {
            fadingSequence = null;
        });
    }

    //直接完成渐显效果
    public void CompleteFadingType() 
    {
        if (fadingSequence != null)
        {
            fadingSequence.Kill();
        }
        TMP_CharacterInfo[] chaInfo = tmpComponent.textInfo.characterInfo;
        for (int i = 0; i < chaInfo.Length; i++)
        {
            SetCharacterAlpha(chaInfo[i], 255);
        }
        tmpComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        fadingSequence = null;
    }


}