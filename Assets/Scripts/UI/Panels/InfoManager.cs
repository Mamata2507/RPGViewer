using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class InfoManager : MonoBehaviour
{
    // Label of the token
    [SerializeField] private TMP_InputField labelInput;
    [SerializeField] private TMP_Text labelText;

    // View distance of the token
    [SerializeField] private TMP_InputField viewInput;
    [SerializeField] private Toggle lightsToggle;
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
        if (GameObject.FindGameObjectWithTag("Map").GetComponent<GridManager>() != null && photonView.IsMine) grid = GameObject.FindGameObjectWithTag("Map").GetComponent<GridManager>();

        UpdateLabel();
        UpdateView();
        UpdateLights();
    }

    /// <summary>
    /// Uploading label text
    /// </summary>
    private void UpdateLabel()
    {
        labelText.text = labelInput.text;
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
                LightManager.myLight.size = (grid.cellWidth + grid.cellHeight) / 2 * (int.Parse(viewInput.text) / 5) + (grid.cellWidth + grid.cellHeight) / 4;
            }
        }
        
    }

    /// <summary>
    /// Uploading light visibility
    /// </summary>
    private void UpdateLights()
    {
        LightManager.UpdateMyLight(lightsToggle.isOn);
    }
}
