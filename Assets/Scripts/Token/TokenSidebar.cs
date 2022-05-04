using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG
{
    public class TokenSidebar : MonoBehaviourPun, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private GameObject configPanel;

        public object[] data;
        private GameObject config = null;
        private bool isDragging = false;

        private const byte HIDE_TOKEN = 1;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (image == null) return;
            
            if (RectTransformUtility.RectangleContainsScreenPoint(image.GetComponent<RectTransform>(), Input.mousePosition) && eventData.pointerId == -1) StartCoroutine(HandleInput(Input.mousePosition));          
        }

        public void OnDrag(PointerEventData eventData) {  }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (image == null) return;

            StopAllCoroutines();
            isDragging = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (image == null) return;
            if (RectTransformUtility.RectangleContainsScreenPoint(image.GetComponent<RectTransform>(), Input.mousePosition))
            {
                if (eventData.button == PointerEventData.InputButton.Right && !isDragging) ConfigToken();
            }
        }

        private IEnumerator HandleInput(Vector3 startPos)
        {
            while (true)
            {
                Vector2 endPos = Input.mousePosition;
                if (Vector3.Distance(startPos, endPos) >= 0.1f && FindObjectOfType<CurrentScene>() != null)
                {
                    isDragging = true;
                    SpawnToken();
                    StopAllCoroutines();
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        public void SpawnToken()
        {
            GameObject token = PhotonNetwork.Instantiate(@"Prefabs/Tokens/Other/Token", Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1)), Quaternion.identity);
            
            token.GetComponent<TokenConfig>().LoadData(data);
            token.GetComponent<TokenConfig>().data[14] = "";

            object[] datas = { token.GetComponent<PhotonView>().ViewID, false };
            PhotonNetwork.RaiseEvent(HIDE_TOKEN, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);

            token.GetComponent<Drag>().canDrag = true;
            token.GetComponent<Drag>().instantiated = false;

            token.GetComponent<PhotonTransformViewClassic>().m_PositionModel.SynchronizeEnabled = false;
        }

        private void ConfigToken()
        {
            if (config != null) return;

            config = Instantiate(configPanel, GameObject.FindGameObjectWithTag("Main Canvas").transform, false);
            config.transform.localPosition = new Vector2(0, 0);
            config.GetComponent<TokenConfigPanel>().LoadData(data, gameObject);
        }

        public void RefreshToken(object[] data)
        {
            this.data = data;
            gameObject.name = (string)data[1];

            if (image == null) return;
            
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0]));
            image.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);

            GetComponentInChildren<TMP_Text>().text = (string)data[1];
        }
    }
}