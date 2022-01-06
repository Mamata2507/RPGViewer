using UnityEngine;
using Photon.Pun;

namespace RPG
{
    public class MapButtons : MonoBehaviourPun
    {
        #region Variables
        // Currently selected map
        public GameObject map;

        // Accept and Cancel buttons
        public GameObject acceptButton;
        public GameObject exitButton;
        #endregion

        #region Start & Update
        private void Update()
        {
            if (map != null && acceptButton.activeInHierarchy == false) acceptButton.SetActive(true);
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Accepting currently selected map (called when pressing Accept-button)
        /// </summary>
        /// 
        public void AcceptMap()
        {
            // Getting reference of each map
            MapHandler[] maps = GetComponentsInChildren<MapHandler>();

            foreach (var map in maps)
            {
                if (this.map == map.mapPrefab)
                {
                    // Destroying old map
                    if (GameObject.FindGameObjectWithTag("Map") != null) GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<GridManager>().GetComponent<PhotonView>().RPC("DestroyMap", RpcTarget.All);

                    // Accepting new map
                    map.AcceptMap();

                    // Clearing reference of selected map
                    this.map = null;
                    
                    gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Cancelling currently selected map (called when pressing Cancel-button)
        /// </summary>
        public void CloseWindow()
        {
            // Clearing reference of selected map
            map = null;
            gameObject.SetActive(false);
        }
        #endregion
    }
}
