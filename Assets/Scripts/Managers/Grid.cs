using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject topCorner;
    public GameObject bottomCorner;
    public Vector2 gridSize;
    public Vector3 returnPosition;

    private float width;
    private float height;

    private float rows;
    private float columns;

    private void OnDrawGizmos()
    {
        if (gridSize.x > 0 && gridSize.y > 0)
        {
            Gizmos.color = Color.red;

            width = topCorner.transform.position.x - bottomCorner.transform.position.x;
            height = topCorner.transform.position.y - bottomCorner.transform.position.y;

            rows = width / gridSize.x;
            columns = height / gridSize.y;

            for (float x = bottomCorner.transform.position.x; x < topCorner.transform.position.x; x += rows)
            {
                for (float y = bottomCorner.transform.position.y; y < topCorner.transform.position.y; y += columns)
                {
                    Vector2 position = new Vector2(x, y);
                    Gizmos.DrawWireCube(new Vector2(position.x + rows / 2, position.y + columns / 2), new Vector2(rows, columns));
                }
            }
        }
    }

    public Vector3 GetClosestPosition(Vector3 position)
    {
        float closestPosition = 10000f;

        for (float x = bottomCorner.transform.position.x; x < topCorner.transform.position.x; x += rows)
        {
            for (float y = bottomCorner.transform.position.y; y < topCorner.transform.position.y; y += columns)
            {
                float distanceToPlayer = Vector2.Distance(new Vector2(x, y), position);
                if (distanceToPlayer < closestPosition)
                {
                    closestPosition = distanceToPlayer;
                    returnPosition = new Vector3(x, y);
                }
            }
        }

        return returnPosition;
    }
}
