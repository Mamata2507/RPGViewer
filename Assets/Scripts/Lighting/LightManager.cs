using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LightManager : MonoBehaviourPunCallbacks
{
    private GameObject[] lights;

    public FunkyCode.Light2D myLight;

    private void Start()
    {
        photonView.RPC("HideLights", RpcTarget.All);
    }

    [PunRPC]
    private void HideLights()
    {
        lights = GameObject.FindGameObjectsWithTag("FOWLight");

        foreach (var light in lights)
        {
            if (!light.GetComponentInParent<PhotonView>().IsMine) light.SetActive(false);
        }
    }

    public void UpdateMyLight(bool toggle)
    {
       myLight.enabled = toggle;
    }
}
