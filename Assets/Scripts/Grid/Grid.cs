using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject topCorner;
    [SerializeField] private GameObject bottomCorner;
    public Vector2 gridSize;

    private Vector3 returnPosition;
    private float distanceToPlayer;

    [SerializeField] private float width;
    [SerializeField] private float height;

    public float cellWidth;
    public float cellHeight;

    private void Update()
    {
        if (gridSize.x > 0 && gridSize.y > 0)
        {
            width = topCorner.transform.position.x - bottomCorner.transform.position.x;
            height = topCorner.transform.position.y - bottomCorner.transform.position.y;

            cellWidth = width / gridSize.x;
            cellHeight = height / gridSize.y;
        }
    }

    private void OnDrawGizmos()
    {

        width = topCorner.transform.position.x - bottomCorner.transform.position.x;
        height = topCorner.transform.position.y - bottomCorner.transform.position.y;

        cellWidth = width / gridSize.x;
        cellHeight = height / gridSize.y;

        if (gridSize.x > 0 && gridSize.y > 0)
        {
            Gizmos.color = Color.red;

            for (float x = bottomCorner.transform.position.x; x < topCorner.transform.position.x; x += cellWidth)
            {
                for (float y = bottomCorner.transform.position.y; y < topCorner.transform.position.y; y += cellHeight)
                {
                    Gizmos.DrawWireCube(new Vector2(x + cellWidth / 2, y + cellHeight / 2), new Vector2(cellWidth, cellHeight));
                }
            }
        }
    }

    public Vector3 GetClosestPosition(Vector3 position)
    {
        float closestPosition = 1000f;

        for (float x = bottomCorner.transform.position.x; x < topCorner.transform.position.x; x += cellWidth)
        {
            for (float y = bottomCorner.transform.position.y; y < topCorner.transform.position.y; y += cellHeight)
            {
                distanceToPlayer = Vector2.Distance(new Vector2(x + cellWidth / 2, y + cellHeight / 2), position);
                if (distanceToPlayer < closestPosition)
                {
                    closestPosition = distanceToPlayer;
                    returnPosition = new Vector2(x + cellWidth / 2, y + cellHeight / 2);
                }
            }
        }
        return returnPosition;
    }
}
