using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private bool preventZoom;
    [SerializeField] private bool preventDrag;

    public bool preventingZoom;
    public bool preventingDrag;

    private void OnMouseOver()
    {
        if (GetComponent<DragAndDrop>() != null)
        {
            if (GetComponent<DragAndDrop>().isDragging)
            {
                if (preventZoom) preventingZoom = true;
                if (preventDrag) preventingDrag = true;
            }
        }
        else if (GetComponent<DragAndInstantiate>() != null)
        {
            if (GetComponent<DragAndInstantiate>().isDragging)
            {
                if (preventZoom) preventingZoom = true;
                if (preventDrag) preventingDrag = true;
            }
        }
        else
        {
            if (preventZoom) preventingZoom = true;
            if (preventDrag) preventingDrag = true;
        }
    }

    private void OnMouseExit()
    {
        if (GetComponent<DragAndDrop>() != null)
        {
            if (!GetComponent<DragAndDrop>().isDragging)
            {
                if (preventZoom) preventingZoom = false;
                if (preventDrag) preventingDrag = false;
            }
        }
        else if (GetComponent<DragAndInstantiate>() != null)
        {
            if (!GetComponent<DragAndInstantiate>().isDragging)
            {
                if (preventZoom) preventingZoom = false;
                if (preventDrag) preventingDrag = false;
            }   
        }
        else
        {
            if (preventZoom) preventingZoom = false;
            if (preventDrag) preventingDrag = false;
        }
        
        
    }
}
