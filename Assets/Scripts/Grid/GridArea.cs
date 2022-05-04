using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG
{
    public class GridArea : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private GameObject topLeft, topRight, botLeft, botRight;

        public Vector2 area;
        private Vector2 distance;

        private void Start()
        {
            if (FindObjectOfType<SceneConfigSidebar>().data != null)
            {
                area = (Vector2)FindObjectOfType<SceneConfigSidebar>().data[5];

                topLeft.transform.localPosition = new Vector3(area.x / -2f, area.y / 2f, -1);
                topRight.transform.localPosition = new Vector3(area.x / 2f, area.y / 2f, -1);
                botLeft.transform.localPosition = new Vector3(area.x / -2f, area.y / -2f, -1);
                botRight.transform.localPosition = new Vector3(area.x / 2f, area.y / -2f, -1);
            }
            

            if (GameObject.Find("Scene").GetComponent<SpriteRenderer>().sprite != null)
            {
                Sprite scene = GameObject.Find("Scene").GetComponent<SpriteRenderer>().sprite;
            }
        }

        private void Update()
        {
            if (gameObject.name == "Center" && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetButton("Horizontal")) FindObjectOfType<GridManager>().transform.position = new Vector3(FindObjectOfType<GridManager>().transform.position.x + (Input.GetAxis("Horizontal") * 0.01f), FindObjectOfType<GridManager>().transform.position.y);
                if (Input.GetButton("Vertical")) FindObjectOfType<GridManager>().transform.position = new Vector3(FindObjectOfType<GridManager>().transform.position.x, FindObjectOfType<GridManager>().transform.position.y + (Input.GetAxis("Vertical") * 0.01f));
            }
            
            HandleLines();
            
            area = new Vector2(Math.Abs(topLeft.transform.position.x - topRight.transform.position.x), Math.Abs(topLeft.transform.position.y - botLeft.transform.position.y));
            FindObjectOfType<GridManager>().area = area;

            GameObject canvas = GameObject.Find("Grid Canvas");

            canvas.GetComponent<RectTransform>().sizeDelta = area;
            canvas.GetComponent<RectTransform>().transform.position = new Vector3((topLeft.transform.position.x + topRight.transform.position.x) / 2f, (topLeft.transform.position.y + botRight.transform.position.y) / 2f);

            canvas.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns, Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns);
            canvas.GetComponentInChildren<GridLayoutGroup>().constraintCount = FindObjectOfType<GridManager>().rows;
        }

        #region Drag
        private void HandleLines()
        {
            if (gameObject.name == "Top Left")
            {
                Vector3[] positions = new Vector3[2];
                positions[0] = transform.position;
                positions[1] = topRight.transform.position;

                GetComponent<LineRenderer>().SetPositions(positions);
                return;
            }
            else if (gameObject.name == "Top Right")
            {
                Vector3[] positions = new Vector3[2];
                positions[0] = transform.position;
                positions[1] = botRight.transform.position;

                GetComponent<LineRenderer>().SetPositions(positions);
                return;
            }
            else if (gameObject.name == "Bot Left")
            {
                Vector3[] positions = new Vector3[2];
                positions[0] = transform.position;
                positions[1] = topLeft.transform.position;

                GetComponent<LineRenderer>().SetPositions(positions);
                return;
            }
            else if (gameObject.name == "Bot Right")
            {
                Vector3[] positions = new Vector3[2];
                positions[0] = transform.position;
                positions[1] = botLeft.transform.position;

                GetComponent<LineRenderer>().SetPositions(positions);
                return;
            }
        }

        public void SetCorners(Sprite sprite)
        {
            topLeft.transform.localPosition = new Vector3(sprite.texture.width / -200f, sprite.texture.height / 200f, -1);
            topRight.transform.localPosition = new Vector3(sprite.texture.width / 200f, sprite.texture.height / 200f, -1);
            botLeft.transform.localPosition = new Vector3(sprite.texture.width / -200f, sprite.texture.height / -200f, -1);
            botRight.transform.localPosition = new Vector3(sprite.texture.width / 200f, sprite.texture.height / -200f, -1);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponentInChildren<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition))) distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;            
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragCorner();
        }

        private void DragCorner()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, -1);

            if (gameObject.name == "Top Left")
            {
                if (mousePos.x != transform.position.x)
                {
                    transform.position = new Vector3(mousePos.x - distance.x, transform.position.y - distance.y, -1);

                    float cellSize = Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns;

                    transform.position = new Vector3(transform.position.x, botLeft.transform.position.y + (cellSize * FindObjectOfType<GridManager>().rows), -1);

                    botLeft.transform.position = new Vector3(transform.position.x, botLeft.transform.position.y, -1);
                    topRight.transform.position = new Vector3(topRight.transform.position.x, transform.position.y, -1);
                }
            }
            else if (gameObject.name == "Top Right")
            {
                if (mousePos.x != transform.position.x)
                {
                    transform.position = new Vector3(mousePos.x - distance.x, transform.position.y - distance.y, -1);

                    float cellSize = Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns;

                    transform.position = new Vector3(transform.position.x, botRight.transform.position.y + (cellSize * FindObjectOfType<GridManager>().rows), -1);

                    botRight.transform.position = new Vector3(transform.position.x, botRight.transform.position.y, -1);
                    topLeft.transform.position = new Vector3(topLeft.transform.position.x, transform.position.y, -1);
                }
            }
            else if (gameObject.name == "Bot Left")
            {
                if (mousePos.x != transform.position.x)
                {
                    transform.position = new Vector3(mousePos.x - distance.x, transform.position.y - distance.y, -1);

                    float cellSize = Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns;

                    transform.position = new Vector3(transform.position.x, topLeft.transform.position.y - (cellSize * FindObjectOfType<GridManager>().rows), -1);

                    botRight.transform.position = new Vector3(botRight.transform.position.x, transform.position.y, -1);
                    topLeft.transform.position = new Vector3(transform.position.x, topLeft.transform.position.y, -1);
                }
            }
            else if (gameObject.name == "Bot Right")
            {
                if (mousePos.x != transform.position.x)
                {
                    transform.position = new Vector3(mousePos.x - distance.x, transform.position.y - distance.y, -1);

                    float cellSize = Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns;

                    transform.position = new Vector3(transform.position.x, topRight.transform.position.y - (cellSize * FindObjectOfType<GridManager>().rows), -1);

                    botLeft.transform.position = new Vector3(botLeft.transform.position.x, transform.position.y, -1);
                    topRight.transform.position = new Vector3(transform.position.x, topRight.transform.position.y, -1);
                }
            }

            float size = Math.Abs(topLeft.transform.position.x - topRight.transform.position.x) / FindObjectOfType<GridManager>().columns;
        }
        #endregion
    }
}
