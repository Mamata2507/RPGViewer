using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LightManager : MonoBehaviourPunCallbacks
{
    private GameObject[] lights;

    public UnityEngine.Experimental.Rendering.Universal.Light2D myLight;

    private void Start()
    {
        myLight = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        HideLights();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        HideLights();
    }

    private void HideLights()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");

        foreach (var light in lights)
        {
            if (!light.GetComponent<PhotonView>().IsMine) light.SetActive(false);
        }
    }

    public void UpdateMyLight(bool toggle)
    {
        myLight.enabled = toggle;
    }
}
