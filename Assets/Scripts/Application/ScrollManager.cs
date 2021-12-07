using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollManager : ScrollRect
{
    // Preventing dragging of scroll rect
    public override void OnBeginDrag(PointerEventData eventData) { }
    public override void OnDrag(PointerEventData eventData) { }
    public override void OnEndDrag(PointerEventData eventData) { }
}
