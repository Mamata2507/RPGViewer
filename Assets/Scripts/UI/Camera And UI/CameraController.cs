using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera zoom
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [HideInInspector] public bool canZoom = true;

    // Camera movement
    private Vector3 startPosition;
    private bool zoomingWithMobile;
    [HideInInspector] public bool canDrag = true;

    // GameObjects preventing camera zooming or moving
    private CanvasManager[] canvasHandlers;

    void Update()
    {
        CheckHandlers();
        if (canZoom) HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
        if (canDrag) HandleMovement();
    }

    /// <summary>
    /// Check if camera can be moved or zoomed
    /// </summary>
    private void CheckHandlers()
    {
        canvasHandlers = FindObjectsOfType<CanvasManager>();

        canDrag = true;
        canZoom = true;

        foreach (var handler in canvasHandlers)
        {
            if (handler.preventingDrag == true)
            {
                canDrag = false;
            }
            if (handler.preventingZoom == true)
            {
                canZoom = false;
            }
        }
    }

    /// <summary>
    /// Zooming camera with mouse wheel
    /// </summary>
    private void HandleZoom(float increment)
    {
        if (increment != 0.0f)
        {
            // Changin camera ortographic size to simulate zooming effect
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment * zoomSpeed, minZoom, maxZoom);
        }
    }

    /// <summary>
    /// Moving camera with dragging
    /// </summary>
    private void HandleMovement()
    {
        // Handle movement if on not zooming with mobile device
        if (Input.GetMouseButtonDown(0) && !zoomingWithMobile)
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition = new Vector3(startPosition.x, startPosition.y, -10f);
        }

        // Handle zooming with mobile devices
        if (canZoom && Input.touchCount == 2)
        {
            zoomingWithMobile = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            HandleZoom(difference * 0.002f);
        }

        // Handle camera movement
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = startPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10f);
        }

        // Enabling moving and zooming camera with mobile devices
        if (Input.GetMouseButtonUp(0))
        {
            zoomingWithMobile = false;
        }
    }
}
