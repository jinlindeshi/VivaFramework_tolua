using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 封装Text 位移、缩放、变色的基类
/// </summary>
public class EffectTextBase : BaseMeshEffect
{
    public Text Label;
    protected RectTransform _labelRt;

    private const int EACH_VERTEX_NUM = 4;
    
    protected Dictionary<int, Vector3> _posChanges = new Dictionary<int, Vector3>();
    protected Dictionary<int, float> _scaleChanges = new Dictionary<int, float>();
    protected Dictionary<int, bool> _colorChanges = new Dictionary<int, bool>();
    
    private Dictionary<int, Color> _colorDic = new Dictionary<int, Color>();


    private bool _isClear;
    
    private UIVertex _uiVertex = new UIVertex();

    private Vector3 _centerP = new Vector3();

    public void SetPosChange(Vector3 pos, int i)
    {
        _posChanges[i] = pos;
        _isClear = false;
    }

    public void RemovePosChange(int i)
    {
        _posChanges.Remove(i);
        _isClear = CheckClear();
    }

    public void SetScaleChange(float scale, int i)
    {
        _scaleChanges[i] = scale;
        _isClear = false;
    }
    
    public void RemoveScaleChange(int i)
    {
        _scaleChanges.Remove(i);
        _isClear = CheckClear();
    }
    
    public void SetColorChange(int i, Color[] cs)
    {
        for (int j = 0; j < 4; j++)
        {
            int writeIndex = i * EACH_VERTEX_NUM + j;
            _colorDic[writeIndex] = cs[j];
        }
        _colorChanges[i] = true;
        _isClear = false;
    }
    
    public void RemoveColorChange(int i)
    {
        for (int j = 0; j < 4; j++)
        {
            int writeIndex = i * EACH_VERTEX_NUM + j;
            _colorDic.Remove(writeIndex);
        }
        _colorChanges.Remove(i);
        _isClear = CheckClear();
    }

    private bool CheckClear()
    {
        foreach (var VARIABLE in _posChanges)
        {
            return false;
        }
        foreach (var VARIABLE in _scaleChanges)
        {
            return false;
        }

        return true;
    }

    private void Update()
    {
        if (_isClear == true)
        {
            return;
        }
        graphic.SetVerticesDirty();
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        if (_labelRt == null)
        {
            _labelRt = Label.gameObject.GetComponent<RectTransform>();
        }
        if (Label.alignment == TextAnchor.LowerLeft || Label.alignment == TextAnchor.UpperLeft
                                                    || Label.alignment == TextAnchor.MiddleLeft)
        {
            _centerP.x = -_labelRt.sizeDelta.x / 2;
        }
        else if(Label.alignment == TextAnchor.LowerCenter || Label.alignment == TextAnchor.MiddleCenter
                                                        || Label.alignment == TextAnchor.UpperCenter)
        {
            _centerP.x = 0;
        }
        else
        {
            _centerP.x = _labelRt.sizeDelta.x / 2;
        }
        
        if (Label.alignment == TextAnchor.UpperCenter || Label.alignment == TextAnchor.UpperLeft
                                                    || Label.alignment == TextAnchor.UpperRight)
        {
            _centerP.y = _labelRt.sizeDelta.y / 2;
        }
        else if(Label.alignment == TextAnchor.MiddleCenter || Label.alignment == TextAnchor.MiddleLeft
                                                          || Label.alignment == TextAnchor.MiddleRight)
        {
            _centerP.y = 0;
        }
        else
        {
            _centerP.y = -_labelRt.sizeDelta.y / 2;
        }
        
        for (int i = 0; i < Label.text.Length; i++)
        {
            FixOneWordPos(i, vh);
        }
        
    }

    //查看指定index下需不需要改
    bool CheckChange(int index)
    {
        if (_scaleChanges.ContainsKey(index))
        {
            return true;
        }

        if (_posChanges.ContainsKey(index))
        {
            return true;
        }

        return false;
    }
    
    //修改一个字符的顶点
    void FixOneWordPos(int index, VertexHelper vh)
    {
        if (CheckChange(index) == false)
        {
            return;
        }
        

        List<UIVertex> vList = new List<UIVertex>();
        for (int i = 0; i < EACH_VERTEX_NUM; i++)
        {
            int writeIndex = index * EACH_VERTEX_NUM + i;
            vh.PopulateUIVertex(ref _uiVertex, writeIndex);
            vList.Add(_uiVertex);
        }

        for (int i = 0; i < vList.Count; i++)
        {
            int writeIndex = index * EACH_VERTEX_NUM + i;

            //vh.PopulateUIVertex(ref v, i);
            UIVertex heheV = vList[i];

            if (_scaleChanges.ContainsKey(index))
            {
                
                float scale = _scaleChanges[index];
                Vector3 dir = heheV.position - _centerP;
                //放大
                if (scale > 1)
                {
//                    print("FixOneWordPos1 " + writeIndex + " - " + scale);
                    heheV.position = heheV.position + dir * (scale - 1);
                }
                //缩小
                else
                {
                    heheV.position = heheV.position - dir * (1-scale);
                }

            }

            if (_posChanges.ContainsKey(index))
            {
                heheV.position += _posChanges[index];
            }

            if (_colorChanges.ContainsKey(index))
            {
                heheV.color = _colorDic[writeIndex];
            }

//            print("FixOneWordPos3 " + heheV.position);

//            print("FixOneWordPos " + writeIndex);
            vh.SetUIVertex(heheV, writeIndex);
        }
    }
}