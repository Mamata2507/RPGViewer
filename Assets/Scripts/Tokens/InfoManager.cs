using UnityEngine;
using TMPro;
using Photon.Pun;

namespace RPG
{
    public class InfoManager : MonoBehaviour
    {
        #region Variables
        // View distance of the token
        public TMP_InputField viewInput;
        private GridManager grid;

        // Label of the token
        public TMP_InputField labelInput;

        // Health of the token
        public TMP_InputField healthInput;

        // Size of the token
        public TMP_InputField sizeInput;


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
            if (FindObjectOfType<GridManager>() != null) grid = FindObjectOfType<GridManager>();

            LightManager.myLight.size = grid.cellWidth * (int.Parse(viewInput.text) / 5) + grid.cellHeight / 2;
        }
        #endregion

        #region Buttons
        public void UpdateLabel()
        {
            photonView.RPC("UpdateLabelRPC", RpcTarget.AllBuffered, labelInput.text, photonView.ViewID);
        }

        public void UpdateView()
        {
            if (viewInput.text != "")
            {
                if (int.Parse(viewInput.text) >= 0) photonView.RPC("UpdateViewRPC", RpcTarget.AllBuffered, viewInput.text, photonView.ViewID);
            }
        }

        public void UpdateHealth()
        {
            if (healthInput.text != "")
            {
                if (int.Parse(healthInput.text) >= 0) photonView.RPC("UpdateHealthRPC", RpcTarget.AllBuffered, healthInput.text, photonView.ViewID);
            }
        }

        public void UpdateSize()
        {
            if (sizeInput.text != "")
            {
                if (int.Parse(sizeInput.text) >= 0)
                {
                    float sizeY = 60f;
                    float healthY = 75;
                    float viewY = 60f;
                    float labelY = -60f;

                    for (int i = 1; i < int.Parse(sizeInput.text) / 5; i++)
                    {
                        sizeY *= 1.5f;
                        healthY *= 1.5f;
                        viewY *= 1.5f;
                        labelY *= 1.5f;
                    }

                    sizeInput.GetComponent<RectTransform>().localPosition = new Vector3(sizeInput.GetComponent<RectTransform>().localPosition.x, sizeY);
                    healthInput.GetComponent<RectTransform>().localPosition = new Vector3(healthInput.GetComponent<RectTransform>().localPosition.x, healthY);
                    viewInput.GetComponent<RectTransform>().localPosition = new Vector3(viewInput.GetComponent<RectTransform>().localPosition.x, viewY);
                    labelInput.GetComponent<RectTransform>().localPosition = new Vector3(labelInput.GetComponent<RectTransform>().localPosition.x, labelY);

                    GetComponentInParent<DragAndDrop>().SetScale((GetComponentInParent<DragAndDrop>().GetComponentInChildren<SpriteRenderer>().sprite.texture.width + GetComponentInParent<DragAndDrop>().GetComponentInChildren<SpriteRenderer>().sprite.texture.height) / 200f);
                    photonView.RPC("UpdateSizeRPC", RpcTarget.AllBuffered, sizeInput.text, photonView.ViewID);
                }
            }
        }
        #endregion

        #region RPC

        [PunRPC]
        private void UpdateLabelRPC(string name, int viewID)
        {
            if (photonView.ViewID == viewID) labelInput.text = name;
        }

        [PunRPC]
        private void UpdateViewRPC(string value, int viewID)
        {
            if (photonView.ViewID == viewID) viewInput.text = value;
        }

        [PunRPC]
        private void UpdateHealthRPC(string value, int viewID)
        {
            if (photonView.ViewID == viewID) healthInput.text = value;
        }

        [PunRPC]
        private void UpdateSizeRPC(string value, int viewID)
        {
            if (photonView.ViewID == viewID)
            {
                sizeInput.text = value;
                GetComponentInParent<DragAndDrop>().size = int.Parse(value);
                GetComponentInParent<DragAndDrop>().SetScale((GetComponentInParent<DragAndDrop>().GetComponentInChildren<SpriteRenderer>().sprite.texture.width + GetComponentInParent<DragAndDrop>().GetComponentInChildren<SpriteRenderer>().sprite.texture.height) / 200f);
            }
        }
        #endregion
    }
}
