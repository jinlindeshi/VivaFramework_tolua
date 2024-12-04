using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ObjectFollower : MonoBehaviour
{
    public Dictionary<int, Transform> targets = new Dictionary<int, Transform>();
    private Vector3 _centerPoint;
    private Vector3 _originEulerAngle;
    private RectTransform _uiCanvasRT;
    private Canvas _uiCanvas;
    public Vector3 centerOffset;

    private Vector3 _targetForward;
    private Vector3 _targetUpward;

    public enum SyncRotationType
    {
        Disable = 0,
        Self = 1,
        Target = 2
    }

    public Transform defaultObject;

    public bool isGUI = false;
    // private Camera _uiCamera;
    // UIStage camera or main camera

    public Camera stageCamera;
    // sub view port for TDImage
    public RectTransform subViewport;
    // Start is called before the first frame update
    
    // Rotation Setting
    public SyncRotationType syncRotateType = SyncRotationType.Disable;
    public Vector3 targetEulersOffset;
    public Vector3 selfEulersOffset;
    void Start()
    {
        if (stageCamera == null)
        {
            // default use main cam
            stageCamera = Camera.main;
            if(Camera.main == null)
              Debug.LogWarning("Camera.main not found!"); 
            
        }
        if (isGUI)
        {
            _uiCanvasRT = GetComponentInParent<CanvasScaler>().gameObject.GetComponent<RectTransform>();
            _uiCanvas = _uiCanvasRT.gameObject.GetComponent<Canvas>();
        }
        if (defaultObject != null)
        {
            AddTarget(defaultObject, isGUI,syncRotateType);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCenterPoint();
    }
    // This will clear targets and set new target
    public void SetTarget(Transform target)
    {
        targets.Clear();
        AddTarget(target, isGUI);
    }
    
    public void AddTarget(Transform target, bool isGUI = false,SyncRotationType syncRotateType = SyncRotationType.Disable)
    {
        this.isGUI = isGUI;
        this.syncRotateType = syncRotateType;
        if (targets.ContainsKey(target.gameObject.GetInstanceID())) return;
        targets.Add(target.gameObject.GetInstanceID(), target);
    }
    
    public void ClearTargets()
    {
        targets.Clear();
    }

    public void RemoveTarget(Transform target)
    {
        if (!targets.ContainsKey(target.gameObject.GetInstanceID())) return;
        targets.Remove(target.gameObject.GetInstanceID());
    }
    private void UpdateCenterPoint()
    {
        if (targets.Count > 0)
        {
            var list = targets.Keys.ToList();
            _originEulerAngle = Vector3.zero;
            _centerPoint = Vector3.zero;
            foreach (int key in list)
            {
                // Debug.Log("Key:" + key.ToString() + "Value: " + targets[key]);
                if (targets[key] != null)
                {
                    _centerPoint += targets[key].position;

                    if (list.IndexOf(key) == 0)
                    {
                        _originEulerAngle = targets[key].localEulerAngles;
                        _targetForward = targets[key].forward;
                        _targetUpward = targets[key].up;
                    }

                    // Debug.DrawLine(Vector3.zero,targets[key].position);
                }
                else
                {
                    targets.Remove(key);
                }

            }
            if (targets.Count > 0)
            {
                _centerPoint = _centerPoint / targets.Count;
                if (targets.Count > 1)
                {
                    transform.position = _centerPoint + centerOffset;
                }
                else
                {
                    if (isGUI)
                    {
                        if (stageCamera)
                            transform.localPosition = WorldToCanvasPoint(_centerPoint) + centerOffset;
                    }
                    else
                    {
                        transform.position = _centerPoint + centerOffset;
                        if (syncRotateType == SyncRotationType.Target)
                        {
                            var newOffset = Quaternion.Euler(_originEulerAngle + targetEulersOffset) * centerOffset;
                            transform.position = _centerPoint + newOffset;
                            var forward = - newOffset;
                            var upward = Quaternion.Euler(_originEulerAngle + targetEulersOffset) * Vector3.up;
                            
                            var quaternion = Quaternion.LookRotation(forward,upward);
                            transform.rotation = quaternion;
                            
                            forward = Quaternion.Euler(selfEulersOffset) * transform.forward;
                            upward = Quaternion.Euler(selfEulersOffset) * transform.up;
                            quaternion = Quaternion.LookRotation(forward,upward);
                            transform.rotation = quaternion;

                        }
                        else if (syncRotateType == SyncRotationType.Self)
                        {
                            var forward = Quaternion.Euler(selfEulersOffset) * _targetForward;
                            var upward = Quaternion.Euler(selfEulersOffset) * _targetUpward;
                            var quaternion = Quaternion.LookRotation(forward,upward);
                            transform.rotation = quaternion;
                        }
                    }
                }
                // Debug.DrawLine(Vector3.zero,_centerPoint);
            }
        }

    }

    private Vector3 WorldToCanvasPoint(Vector3 pos)
    {
        pos = stageCamera.WorldToViewportPoint(pos);
        if (subViewport != null)
        {
            pos = ViewportConvertToParentSpace(_uiCanvasRT, subViewport, pos);
        }

        if (_uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            pos = new Vector3(pos.x - 0.5f, pos.y - 0.5f, 0);
            pos = stageCamera.ViewportToScreenPoint(pos);
        }
        else
        {
            pos = _uiCanvas.worldCamera.ViewportToWorldPoint(pos);
        }
        Vector3 s = _uiCanvasRT.localScale;
        return new Vector3(pos.x / s.x, pos.y / s.y, 0);
    }

    private Vector3 ViewportConvertToParentSpace(RectTransform parent, RectTransform child, Vector3 point)
    {
        return new Vector3((point.x * child.rect.width + child.anchoredPosition.x) / parent.rect.width, (point.y * child.rect.height + child.anchoredPosition.y) / parent.rect.height, 0);
    }

#if UNITY_EDITOR  
    [ContextMenu("Preview")]
    private void PreviewInEditor()
    {
      Start();
    }
#endif
}
