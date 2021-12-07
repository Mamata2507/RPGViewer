using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class InfoManager : MonoBehaviour
{
    // View distance of the token
    public TMP_InputField viewInput;
    private GridManager grid;

    // Lighting of the token
    [SerializeField] private LightManager LightManager;

    // Networking variables
    private PhotonView photonView;

    // Mouse over GUI
    public bool isInteracting;

    private void OnMouseOver()
    {
        isInteracting = true;
    }

    private void OnMouseExit()
    {
        isInteracting = false;
    }

    private void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
    }

    private void Update()
    {
        // Getting reference of grid
        if (FindObjectOfType<GridManager>() != null && photonView.IsMine) grid = FindObjectOfType<GridManager>();

        UpdateView();
    }

    /// <summary>
    /// Uploading view distance
    /// </summary>
    private void UpdateView()
    {
        if (viewInput.text != "")
        {
            if (int.Parse(viewInput.text) >= 0)
            {
                LightManager.myLight.size = grid.cellWidth * (int.Parse(viewInput.text) / 5) + grid.cellHeight / 2;
            }
        }
        
    }
}
