using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera zoom
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    private float cameraZoom;

    // Camera movement
    private float distance;
    private Vector3 startPosition;
    public bool canDrag = true;

    void Start()
    {
        distance = transform.position.z;
        cameraZoom = Camera.main.orthographicSize;
    }

    void Update()
    {
        HandleZoom();
        HandleMovement();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            cameraZoom -= scroll * zoomSpeed;
            cameraZoom = Mathf.Clamp(cameraZoom, minZoom, maxZoom);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, cameraZoom, smoothSpeed * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0) && canDrag)
        {
            startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            startPosition = Camera.main.ScreenToWorldPoint(startPosition);
            startPosition.z = transform.position.z;
        }
        else if (Input.GetMouseButton(0) && canDrag)
        {
            var MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.z = transform.position.z;
            transform.position = transform.position - (MouseMove - startPosition);
        }
    }
}
