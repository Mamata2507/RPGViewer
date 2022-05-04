using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace RPG
{
    public class Drag : MonoBehaviourPun, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private bool isDragging = false;
        public bool canDrag = false;
        
        public bool instantiated = true;
        private const byte HIDE_TOKEN = 1;

        [SerializeField] private Image image;
        [SerializeField] private GameObject dragObject, dragPrefab;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (image == null) return;

            if (MasterClient.isMaster || (int)GetComponent<TokenConfig>().data[4] == 0)
            {
                if (eventData.pointerId != -1) return;
                photonView.RequestOwnership();
                GetComponent<PhotonTransformViewClassic>().m_PositionModel.SynchronizeEnabled = false;
                StartCoroutine(HandleInput(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1)));
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (image == null) return;

            if (isDragging && eventData.pointerId == -1) canDrag = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (image == null) return;

            if (MasterClient.isMaster || (int)GetComponent<TokenConfig>().data[4] == 0)
            {
                StopAllCoroutines();

                isDragging = false;
                SnapToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (image == null) return;

            if (MasterClient.isMaster || (int)GetComponent<TokenConfig>().data[4] == 0)
            {
                if (eventData.button == PointerEventData.InputButton.Right && !isDragging) GetComponent<TokenConfig>().OpenSelection();
            }
        }

        private void Update()
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(image.GetComponent<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition))) FindObjectOfType<Camera2D>().overToken = true;
            if (dragObject != null)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(dragObject.GetComponentInChildren<Image>().GetComponent<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition))) FindObjectOfType<Camera2D>().overToken = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!instantiated)
                {
                    instantiated = true;

                    GetComponent<PhotonTransformViewClassic>().m_PositionModel.SynchronizeEnabled = true;

                    if ((int)GetComponent<TokenConfig>().data[4] == 0)
                    {
                        object[] datas = { photonView.ViewID, true };
                        PhotonNetwork.RaiseEvent(HIDE_TOKEN, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
                    }
                }

                canDrag = false;
                if (MasterClient.isMaster && (FindObjectOfType<CurrentScene>() != null || SceneManager.GetActiveScene().buildIndex == 3)) GetComponent<TokenConfig>().SaveConfiguration();
            }

            if ((MasterClient.isMaster || (int)GetComponent<TokenConfig>().data[4] == 0) && canDrag) DragToken();
        }

        private IEnumerator HandleInput(Vector3 startPos)
        {
            while (true)
            {
                Vector2 endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1));
                if (Vector3.Distance(startPos, endPos) >= 0.01f)
                {
                    dragObject = Instantiate(dragPrefab);
                    dragObject.GetComponentInChildren<Image>().sprite = image.sprite;
                    dragObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.80f);

                    dragObject.transform.localScale = transform.localScale;

                    isDragging = true;
                    StopAllCoroutines();
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        private void DragToken()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, -1);
           if (dragObject != null) dragObject.transform.position = mousePos;
           else transform.position = mousePos;
        }

        private void SnapToGrid(Vector2 position)
        {
            if (dragObject != null)
            {
                RaycastHit2D raycast = Physics2D.Raycast(transform.position, dragObject.transform.position);
                if (raycast.collider == null)
                {
                    transform.position = GridData.GetClosestCell(position, float.Parse((string)GetComponent<TokenConfig>().data[2]));
                    transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                    Destroy(dragObject);
                }
            }
            GetComponent<PhotonTransformViewClassic>().m_PositionModel.SynchronizeEnabled = true;
        }
    }
}