
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    public uint HNum = 3;

    public uint VNum = 3;

    public GameObject itemPrefab;

    private ScrollRect _sRect;

    private RectTransform _viewPort;

    private RectTransform _content;

    private Vector2 _itemSize;

    //用于Lua生成itemRenderer
    private LuaFunction _itemInitCall;
    #region 运行时支持

    private uint _childCount = 0;
    public void SetShow(uint num)
    {
        if (itemPrefab == null)
        {
            return;
        }
        Clear();
        _childCount = num;
        _content.sizeDelta = new Vector2(_itemSize.x * HNum, 
            _itemSize.y * Mathf.Ceil((float)_childCount/(float)HNum));

        for (int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(itemPrefab);
            item.transform.parent = _content;
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((i % HNum) * _itemSize.x,
                -Mathf.Floor(i/HNum) * _itemSize.y);
            if (_itemInitCall != null)
            {
                _itemInitCall.Call();
            }
        }
    }

    public void GetItemByIndex(int index)
    {
        
    }

    public void Clear()
    {
        while (_content.childCount > 0)
        {
            DestroyImmediate(_content.GetChild(0).gameObject);
        }
    }
    
    public void SetScrollRate(float value)
    {
        float maxUp = _viewPort.sizeDelta.y / 2 - _content.sizeDelta.y / 2;
        float maxBottom = _content.sizeDelta.y / 2 - _viewPort.sizeDelta.y / 2;
//        print("SetScrollRate " + maxUp + " " + maxBottom);
        _content.anchoredPosition = new Vector2(_content.anchoredPosition.x,
            maxUp + (maxBottom - maxUp) * value);
    }

    private void Start()
    {
        FixItemSize();
    }

    public void FixItemSize()
    {
        _itemSize = itemPrefab.GetComponent<RectTransform>().sizeDelta;
    }

    #endregion

    #region 编辑器支持

    public void Ctor()
    {
        Vector2 v = new Vector2(0,1);
        //滚动插件
        _sRect = gameObject.GetComponent<ScrollRect>();
        if (_sRect == null)
        {
            _sRect = gameObject.AddComponent<ScrollRect>();
        }

        if (transform.Find("Viewport") != null)
        {
            _viewPort = transform.Find("Viewport").gameObject.GetComponent<RectTransform>(); 
        }
        if (_viewPort == null)
        {
            GameObject obj = new GameObject();
            obj.name = "Viewport";
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            _viewPort = obj.AddComponent<RectTransform>();
            obj.AddComponent<Image>();
            obj.AddComponent<Mask>().showMaskGraphic = false;
        }

        _sRect.viewport = _viewPort;
        
        if (_viewPort.Find("Content") != null)
        {
            _content = _viewPort.Find("Content").gameObject.GetComponent<RectTransform>(); 
        }
        if (_content == null)
        {
            GameObject obj = new GameObject();
            obj.name = "Content";
            obj.transform.parent = _viewPort;
            _content = obj.AddComponent<RectTransform>();
//            _content.anchorMax = v;
//            _content.anchorMin = v;
            _content.anchoredPosition = Vector2.zero;
        }

        _sRect.content = _content;

        if (itemPrefab != null)
        {
            _itemSize = itemPrefab.GetComponent<RectTransform>().sizeDelta;
        }
    }

    #endregion
    
}