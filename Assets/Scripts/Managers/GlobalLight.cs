using UnityEngine;
using Photon.Pun;

public class GlobalLight : MonoBehaviour
{
    [SerializeField] private GameObject lightPrefab;
    
    private GameObject globalLight;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient) InstantiateLight();
    }

    private void InstantiateLight()
    {
        globalLight = Instantiate(lightPrefab, transform.position, Quaternion.identity);
        globalLight.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && PhotonNetwork.IsMasterClient) ShowLights();
    }

    private void ShowLights()
    {
        globalLight.SetActive(!globalLight.activeInHierarchy);
    }
}
