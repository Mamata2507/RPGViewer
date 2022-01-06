using UnityEngine;

namespace RPG
{
    public class Canvas2D : MonoBehaviour
    {
        #region Variables
        // Prevent zooming and moving of camera
        [SerializeField] private bool preventZoom;
        [SerializeField] private bool preventDrag;

        // Currently ppreventing zoom and drag (hiding in inspector)
        [HideInInspector] public bool preventingZoom;
        [HideInInspector] public bool preventingDrag;
        #endregion

        #region Mouse Input
        private void OnMouseOver()
        {
            // Preventing camera controlling when dragging tokens
            if (GetComponent<DragAndDrop>() != null)
            {
                if (GetComponent<DragAndDrop>().isPressing && GetComponent<DragAndDrop>().photonView.IsMine)
                {
                    if (preventZoom) preventingZoom = true;
                    if (preventDrag) preventingDrag = true;
                }
            }

            // Preventing camera controlling when instantiating tokens
            else if (GetComponent<DragAndInstantiate>() != null)
            {
                if (GetComponent<DragAndInstantiate>().isDragging)
                {
                    if (preventZoom) preventingZoom = true;
                    if (preventDrag) preventingDrag = true;
                }
            }

            // Preventing zoom outside of tokens
            else
            {
                if (preventZoom) preventingZoom = true;
                if (preventDrag) preventingDrag = true;
            }
        }

        private void OnMouseExit()
        {
            // Enabling camera controlling after dragging tokens
            if (GetComponent<DragAndDrop>() != null)
            {
                if (!GetComponent<DragAndDrop>().isPressing)
                {
                    if (preventZoom) preventingZoom = false;
                    if (preventDrag) preventingDrag = false;
                }
            }
            // Enabling camera controlling after instantiated tokens
            else if (GetComponent<DragAndInstantiate>() != null)
            {
                if (!GetComponent<DragAndInstantiate>().isDragging)
                {
                    if (preventZoom) preventingZoom = false;
                    if (preventDrag) preventingDrag = false;
                }
            }

            // Enabling zoom outside of tokens
            else
            {
                if (preventZoom) preventingZoom = false;
                if (preventDrag) preventingDrag = false;
            }
        }
        #endregion
    }
}
