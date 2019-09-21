using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollRectDerived : ScrollRect {

    public override void OnDrag(PointerEventData eventData)
    {
        //Do nothing to disable the dragging
    }
}
