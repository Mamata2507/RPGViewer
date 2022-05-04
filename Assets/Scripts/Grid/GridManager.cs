using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject cell;
        [SerializeField] private Transform startPos;

        [HideInInspector] public int columns;
        [HideInInspector] public int rows;

        public List<GameObject> cells = new List<GameObject>();
        [HideInInspector] public Vector2 area;

        private void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex == 3) 
            {
                if (FindObjectOfType<SceneConfigSidebar>().data == null) return;

                columns = int.Parse((string)FindObjectOfType<SceneConfigSidebar>().data[3]);
                rows = int.Parse((string)FindObjectOfType<SceneConfigSidebar>().data[4]);

                transform.position = (Vector2)FindObjectOfType<SceneConfigSidebar>().data[6];
                InstantiateCells();
            }
        }

        #region Cells
        public void InstantiateCells()
        {
            foreach (var item in cells) Destroy(item);
            cells.Clear();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y ++)
                {
                    GameObject cell = Instantiate(this.cell, GameObject.Find("Grid Panel").transform);
                    cells.Add(cell);

                    if (FindObjectOfType<SceneConfigSidebar>().data != null) cell.GetComponent<Image>().color = new Color((float)FindObjectOfType<SceneConfigSidebar>().data[8], (float)FindObjectOfType<SceneConfigSidebar>().data[9], (float)FindObjectOfType<SceneConfigSidebar>().data[10], float.Parse((string)FindObjectOfType<SceneConfigSidebar>().data[11]) / 100);
                }
            }

            GridArea[] corners = FindObjectsOfType<GridArea>();

            float cellSize = area.x / columns;

            foreach (var corner in corners)
            {
                if (corner.gameObject.name == "Bot Left")
                {
                    corner.transform.position = new Vector3(startPos.position.x, startPos.position.y - (cellSize * rows), -1);
                }
                else if (corner.gameObject.name == "Bot Right")
                {
                    corner.transform.position = new Vector3(startPos.position.x + (cellSize * columns), startPos.position.y - (cellSize * rows), -1);
                }
            }
        }

        public void ChangeColor(Color color, float alpha)
        {
            foreach (var cell in cells)
            {
                cell.GetComponent<Image>().color = new Color(color.r, color.g, color.b, alpha);
            }
        }
        #endregion
    }
}