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
    public bool canZoom = true;

    // Camera movement
    private Vector3 startPosition;
    public bool canDrag = true;

    void Update()
    {
        if (canZoom) HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
        if (canDrag) HandleMovement();
    }

    private void HandleZoom(float increment)
    {
        if (increment != 0.0f)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment * zoomSpeed, minZoom, maxZoom);
        }
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (canZoom && Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            HandleZoom(difference * 0.002f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = startPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
    }
}
