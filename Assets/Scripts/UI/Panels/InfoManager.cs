using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class InfoManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField labelInput;
    [SerializeField] private TMP_Text labelText;

    [SerializeField] private TMP_InputField viewInput;
    [SerializeField] private Toggle lightsToggle;

    [SerializeField] private LightManager LightManager;

    private GridManager grid;
    private PhotonView photonView;

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
        if (GameObject.FindGameObjectWithTag("Map").GetComponent<GridManager>() != null && photonView.IsMine) grid = GameObject.FindGameObjectWithTag("Map").GetComponent<GridManager>();

        UpdateLabel();
        UpdateView();
        UpdateLights();
    }

    private void UpdateLabel()
    {
        labelText.text = labelInput.text;
    }

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

    private void UpdateLights()
    {
        LightManager.UpdateMyLight(lightsToggle.isOn);
    }
}
