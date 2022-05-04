using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

namespace RPG
{
    public class SceneSidebar : MonoBehaviourPun, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private GameObject selectionPanel;
        
        public object[] data;

        #region Start & Update
        private void Start()
        {
            if (MasterClient.reference == gameObject)
            {
                RefreshScene(MasterClient.data);
                MasterClient.reference = null;
                MasterClient.data = null;
            }
        }
        #endregion

        #region Selection
        public void OnPointerClick(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                if (eventData.button == PointerEventData.InputButton.Left && !selectionPanel.activeInHierarchy) OpenSelection();
            }
        }

        private void OpenSelection()
        {
            if (selectionPanel.activeInHierarchy) return;

            selectionPanel.SetActive(true);

            selectionPanel.GetComponent<SceneSelection>().data = data;
            selectionPanel.GetComponent<SceneSelection>().reference = gameObject;
        }

        public void RefreshScene(object[] data)
        {
            this.data = data;
            gameObject.name = (string)data[1];
            if (image == null) return;

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0]));
            image.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            GetComponentInChildren<TMP_Text>().text = (string)data[1];
        }
        #endregion
    }
}