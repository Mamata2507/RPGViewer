using UnityEngine;
using Photon.Pun;

public class LightManager : MonoBehaviourPunCallbacks
{
    // All lights in scene
    private GameObject[] lights;

    // This token's light
    public FunkyCode.Light2D myLight;

    private void Start()
    {
        photonView.RPC("HideLights", RpcTarget.All);
    }

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

    /// <summary>
    /// Enabling and disaling my light
    /// </summary>
    public void UpdateMyLight(bool toggle)
    {
       myLight.enabled = toggle;
    }
}
