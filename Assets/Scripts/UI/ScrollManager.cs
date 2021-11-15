using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollManager : ScrollRect
{
    private void OnMouseOver()
    {
        Camera.main.GetComponent<CameraController>().canZoom = false;
    }

    private void OnMouseExit()
    {
        Camera.main.GetComponent<CameraController>().canZoom = true;
    }

    public override void OnBeginDrag(PointerEventData eventData) { }
    public override void OnDrag(PointerEventData eventData) { }
    public override void OnEndDrag(PointerEventData eventData) { }
}
