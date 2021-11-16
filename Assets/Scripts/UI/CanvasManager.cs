using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private bool preventZoom;
    [SerializeField] private bool preventDrag;

    [HideInInspector] public bool preventingZoom;
    [HideInInspector] public bool preventingDrag;

    private void OnMouseOver()
    {
        if (preventZoom) preventingZoom = false;
        if (preventDrag) preventingDrag = false;
    }

    private void OnMouseExit()
    {
        if (preventZoom) preventingZoom = true;
        if (preventDrag) preventingDrag = true;
    }
}
