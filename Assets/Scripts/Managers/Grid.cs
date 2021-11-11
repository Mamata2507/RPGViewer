using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject topCorner;
    public GameObject bottomCorner;
    public Vector2 gridSize;

    public Vector3 returnPosition;
    private float distanceToPlayer;

    private float width;
    private float height;

    private float rows;
    private float columns;

    private void Update()
    {
        if (gridSize.x > 0 && gridSize.y > 0)
        {
            width = topCorner.transform.position.x - bottomCorner.transform.position.x;
            height = topCorner.transform.position.y - bottomCorner.transform.position.y;

            rows = width / gridSize.x;
            columns = height / gridSize.y;
        }
    }

    private void OnDrawGizmos()
    {
        if (gridSize.x > 0 && gridSize.y > 0)
        {
            Gizmos.color = Color.red;

            for (float x = bottomCorner.transform.position.x; x < topCorner.transform.position.x; x += rows)
            {
                for (float y = bottomCorner.transform.position.y; y < topCorner.transform.position.y; y += columns)
                {
                    Gizmos.DrawWireCube(new Vector2(x + rows / 2, y + columns / 2), new Vector2(rows, columns));
                }
            }
        }
    }

    public Vector3 GetClosestPosition(Vector3 position)
    {
        float closestPosition = 1000f;

        for (float x = bottomCorner.transform.position.x; x < topCorner.transform.position.x; x += rows)
        {
            for (float y = bottomCorner.transform.position.y; y < topCorner.transform.position.y; y += columns)
            {
                distanceToPlayer = Vector2.Distance(new Vector2(x + rows / 2, y + columns / 2), position);
                if (distanceToPlayer < closestPosition)
                {
                    closestPosition = distanceToPlayer;
                    returnPosition = new Vector2(x + rows / 2, y + columns / 2);
                }
            }
        }
        return returnPosition;
    }
}
