using UnityEngine;
using Photon.Pun;

namespace RPG
{
    public class LightManager : MonoBehaviourPunCallbacks
    {
        #region Variables
        // All lights in scene
        private GameObject[] lights;

        // This token's light
        public FunkyCode.Light2D myLight;
        #endregion

        #region Start & Update
        private void Start()
        {
            photonView.RPC("HideLights", RpcTarget.All);
        }
        #endregion

        #region RPC
        /// <summary>
        /// Hiding other clients lights
        /// </summary>
        [PunRPC]
        private void HideLights()
        {
            lights = GameObject.FindGameObjectsWithTag("FOW Light");

            foreach (var light in lights)
            {
                // Hiding the light if it isn't mine
                if (!light.GetComponentInParent<PhotonView>().IsMine) light.SetActive(false);
            }
        }
        #endregion
    }
}
