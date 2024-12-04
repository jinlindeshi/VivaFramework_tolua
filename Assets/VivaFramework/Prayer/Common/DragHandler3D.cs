using System;
using UnityEngine;

namespace Prayer
{
    public class DragHandler3D : OperateHandler
    {
        
        public const string BEGIN_DRAG = "BeginDrag";
        public const string DRAG = "Drag";
        public const string END_DRAG = "EndDrag";
        public const string MOUSE_ENTER = "MouseEnter";
        public const string MOUSE_EXIT = "MouseExit";
        public const string DRAG_ENABLE = "DragEnable";
        public const string DRAG_DISABLE = "DragDisable";

        private GameObject _movedObj;
        private Vector3 _moveOffset = Vector3.zero;
        /** 
         *  拖动时跟随的目标
         */
        public void SetMovedGameObj(GameObject go, Vector3 offset)
        {
            _movedObj = go;
            _moveOffset = offset;
        }
        public void SetMovedGameObj(GameObject go)
        {
            _movedObj = go;
            _moveOffset = Vector3.zero;
        }
        
        private void OnMouseDrag()
        {
            if (enabled == false) return;
            if (enabled == true && _movedObj != null)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(_movedObj.transform.position);
                Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
                mouseScreenPos += _moveOffset;
                _movedObj.transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            }
            TakeCall(DRAG);
        }

        private void OnDisable()
        {
            TakeCall(DRAG_DISABLE);
        }

        private void OnEnable()
        {
            TakeCall(DRAG_ENABLE);
        }

        private void OnMouseEnter()
        {
            if (enabled == false) return;
            TakeCall(MOUSE_ENTER);
        }
        private void OnMouseExit()
        {
            if (enabled == false) return;
            TakeCall(MOUSE_EXIT);
        }

        private void OnMouseUp()
        {
            if (enabled == false) return;
            // Debug.Log("OnMouseUp");
            TakeCall(END_DRAG);
        }
        
        private void OnMouseDown()
        {
            if (enabled == false) return;
            TakeCall(BEGIN_DRAG);
        }
    }
}