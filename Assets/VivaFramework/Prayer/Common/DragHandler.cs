using UnityEngine;
using UnityEngine.EventSystems;

namespace Prayer
{
    public class DragHandler : OperateHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        
        public const string BEGIN_DRAG = "BeginDrag";
        public const string DRAG = "Drag";
        public const string END_DRAG = "EndDrag";
        public const string DROP = "Drop";
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            TakeCall(BEGIN_DRAG, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TakeCall(DRAG, eventData);
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            TakeCall(END_DRAG, eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            TakeCall(DROP, eventData);
        }
    }
    
}