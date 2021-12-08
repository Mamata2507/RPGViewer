using UnityEngine;
using TMPro;
using Photon.Pun;

public class InfoManager : MonoBehaviour
{
    #region Variables
    // View distance of the token
    public TMP_InputField viewInput;
    private GridManager grid;

    // Lighting of the token
    [SerializeField] private LightManager LightManager;

    // Networking variables
    private PhotonView photonView;

    // Mouse over GUI
    public bool isInteracting;
    #endregion

    #region Mouse Input
    private void OnMouseOver()
    {
        isInteracting = true;
    }

    private void OnMouseExit()
    {
        isInteracting = false;
    }
    #endregion

    #region Start & Update
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
    #endregion

    #region View Distance
    /// <summary>
    /// Updating view distance
    /// </summary>
    private void UpdateView()
    {
        if (viewInput.text != "")
        {
            if (int.Parse(viewInput.text) >= 0)
            {
                // Calculating view distance (ft.) to match grid size
                LightManager.myLight.size = grid.cellWidth * (int.Parse(viewInput.text) / 5) + grid.cellHeight / 2;
            }
        } 
    }
    #endregion
}
