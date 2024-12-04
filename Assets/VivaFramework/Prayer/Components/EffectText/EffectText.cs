
using System;
using UnityEngine;

public class EffectText : EffectTextBase
{
    public int index = 0;

    public float scale = 1;

    public Vector3 addPos = Vector3.zero;
    
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    
    
    private int _showSize;

    //设定要显示的字体大小
    public void SetFontSize(int showSize, float maxScale)
    {
        _showSize = showSize;

        Label.fontSize = Convert.ToInt32(Mathf.Ceil(showSize * maxScale));
        Label.transform.localScale = Vector3.one / maxScale;
    }
    private void Start()
    {
        if (Label != null)
        {
            return;
        }
        
//        _labelRt = Label.gameObject.GetComponent<RectTransform>();
        SetFontSize(40, 2);
        color1 = Label.color;
        color2 = Label.color;
        color3 = Label.color;
        color4 = Label.color;
        
        SetScaleChange(scale, index);
    }

    private void Update()
    {
        print("EffectText1 - " + Label.preferredWidth + " - " + Label.preferredHeight);
        print("EffectText2 - " + Label.alignment);
        if (_scaleChanges.ContainsKey(index) != true || _scaleChanges[index] != scale)
        {
            SetScaleChange(scale, index);
            print("缩放改变啦~~~");
        }
        
        if (_posChanges.ContainsKey(index) != true || _posChanges[index] == addPos != true)
        {
            SetPosChange(addPos, index);
            print("位置改变啦~~~");
        }

        Color[] cs = {color1, color2, color3, color4};
        SetColorChange(index, cs);
    }
}