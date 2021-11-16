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
    private bool zoomingWithMobile;

    private CanvasManager[] abilities;

    void Update()
    {
        CheckAbilities();
        if (canZoom) HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
        if (canDrag) HandleMovement();
    }

    private void CheckAbilities()
    {
        abilities = FindObjectsOfType<CanvasManager>();

        canDrag = true;
        canZoom = true;

        foreach (var ability in abilities)
        {
            if (ability.preventingDrag == true)
            {
                canDrag = false;
            }
            if (ability.preventingZoom == true)
            {
                canZoom = false;
            }
        }
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
        if (Input.GetMouseButtonDown(0) && !zoomingWithMobile)
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition = new Vector3(startPosition.x, startPosition.y, -10f);
        }
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
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = startPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            zoomingWithMobile = false;
        }
    }
}
