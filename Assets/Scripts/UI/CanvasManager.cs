using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private bool preventZoom;
    [SerializeField] private bool preventDrag;

    private void OnMouseOver()
    {
        if (preventZoom) camera.GetComponent<CameraController>().canZoom = false;
        if (preventDrag) camera.GetComponent<CameraController>().canDrag = false;
    }

    private void OnMouseExit()
    {
        if (preventZoom) camera.GetComponent<CameraController>().canZoom = true;
        if (preventDrag) camera.GetComponent<CameraController>().canDrag = true;
    }

    private void Start()
    {
        camera = Camera.main;
    }
}
