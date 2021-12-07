using UnityEngine;
using Photon.Pun;

public class SidebarButtons : MonoBehaviour
{
    [SerializeField] private GameObject maps, tokens;
    [SerializeField] private GameObject mapButton;

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient) mapButton.SetActive(false);
    }

    private void Update()
    {
        Canvas2D[] handlers = maps.GetComponentsInChildren<Canvas2D>();
        bool canClose = true;
        foreach (var handler in handlers)
        {
            if (handler.preventingDrag || handler.preventingZoom)
            {
                canClose = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && canClose)
        {
            maps.SetActive(false);
        }
    }

    public void OpenTokens()
    {
        tokens.SetActive(!tokens.activeInHierarchy);
        maps.SetActive(false);
    }

    public void OpenMaps()
    {
        maps.SetActive(!maps.activeInHierarchy);
        tokens.SetActive(false);
    }
}
