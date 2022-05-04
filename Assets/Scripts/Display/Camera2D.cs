using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG
{
    public class Camera2D : MonoBehaviour
    {
        [Header("Zoom")]
        [SerializeField]private float zoomSpeed;
        [SerializeField]private float minZoom;
        [SerializeField]private float maxZoom;
        
        private Vector3 startPosition;

        private bool canDrag, canZoom, dragging, overObject;

        public bool overToken = false;

        private void Start()
        {
            Application.targetFrameRate = 30;
        }

        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                overToken = false;
            }

            if (overToken || !EventSystem.current.IsPointerOverGameObject()) canZoom = true;
            else canZoom = false;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                canDrag = true;
                dragging = true;
            }
            else if (!dragging && EventSystem.current.IsPointerOverGameObject())
            {
                overObject = true;
                canDrag = false;
            }

            if (overObject)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    overObject = false;
                    canDrag = true;
                }
            }
            else if (!overObject && !EventSystem.current.IsPointerOverGameObject()) canDrag = true;

            if (Input.GetMouseButtonUp(0)) dragging = false;

            MouseInput();
        }

        private void HandleZoom(float increment)
        {
            if (increment != 0.0f ) Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment * zoomSpeed, minZoom, maxZoom);
        }

        private void MouseInput()
        {
            if (canZoom) HandleZoom(Input.GetAxis("Mouse ScrollWheel"));

            if (Input.GetMouseButtonDown(0) && canDrag)
            {
                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPosition = new Vector3(startPosition.x, startPosition.y, -10f);
            }
            if (Input.GetMouseButton(0) && canDrag)
            {
                Vector3 direction = startPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10f);
            }
        }
    }
}
