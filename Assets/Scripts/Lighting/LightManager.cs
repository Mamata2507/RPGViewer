using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightManager : MonoBehaviourPunCallbacks
{
    public GameObject lightPrefab;

    private PhotonView photonView;
    private GameObject[] lights;

    private UnityEngine.Experimental.Rendering.Universal.Light2D myLight;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        myLight = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        HideLights();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && photonView.IsMine) HideMyLight();
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

    public void HideMyLight()
    {
        myLight.enabled = !myLight.enabled;
    }
}
