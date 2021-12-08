using UnityEngine;
using Photon.Pun;

public class SidebarButtons : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject maps, tokens;
    [SerializeField] private GameObject mapButton;
    #endregion

    #region Start & Update
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
    }
    #endregion
}
