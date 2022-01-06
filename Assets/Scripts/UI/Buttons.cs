using UnityEngine;
using Photon.Pun;

namespace RPG
{
    public class Buttons: MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject maps, tokens, menu;
        [SerializeField] private GameObject mapButton;

        private bool mouseOver = false;
        #endregion

        #region Mouse Input
        private void OnMouseOver()
        {
            mouseOver = true;
        }

        private void OnMouseExit()
        {
            mouseOver = false;
        }
        #endregion

        #region Start & Update
        private void Start()
        {
            mapButton.SetActive(false);
        }

        private void Update()
        {
            if (PhotonNetwork.IsMasterClient && Assets.maps.Count > 0 && mapButton.activeInHierarchy == false) mapButton.SetActive(true);

            Canvas2D[] handlers = maps.GetComponentsInChildren<Canvas2D>();
            bool canClose = true;
            foreach (var handler in handlers)
            {
                if (handler.preventingDrag || handler.preventingZoom) canClose = false;
            }

            if (Input.GetMouseButtonDown(0) && canClose && mouseOver)
            {
                maps.SetActive(false);
            }
        }
        #endregion

        #region Buttons
        public void OpenTokens()
        {
            tokens.SetActive(!tokens.activeInHierarchy);
            maps.SetActive(false);
        }

        public void OpenMaps()
        {
            maps.SetActive(!maps.activeInHierarchy);
            tokens.SetActive(false);
            menu.SetActive(false);
        }

        public void OpenMenu()
        {
            maps.SetActive(false);
            menu.SetActive(!menu.activeInHierarchy);
        }
        #endregion
    }
}
