using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Position of top and bottom corners
    [Header("Grid Area")]
    [SerializeField] private Transform topCorner;
    [SerializeField] private Transform bottomCorner;

    // Grid size (how many cells on both axis)
    [Header("Grid Size")]
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private GameObject gridCell;

    // Width and height of grid area
    private float width;
    private float height;

    // Size of each cell
    // These need to be public, because they're needed when calculating the view distance
    public float cellWidth;
    public float cellHeight;

    private void Start()
    {
        // Dividing grid area to as many parts as grid size (x - axis)
        for (float x = bottomCorner.position.x; x < topCorner.position.x; x += cellWidth)
        {
            // Dividing grid area to as many parts as grid size (y - axis)
            for (float y = bottomCorner.position.y; y < topCorner.position.y; y += cellHeight)
            {
                GameObject cell = Instantiate(gridCell, new Vector2(x + cellWidth / 2, y + cellHeight / 2), Quaternion.identity, this.transform);
                cell.transform.localScale = new Vector2(cellWidth, cellHeight);
            }
        }
    }

    // Grid calculations on runtime
    private void Update()
    {
        // Checking if map has a grid to be shown (checking if grid size is over zero)
        if (gridSize.x > 0 && gridSize.y > 0)
        {
            // Calculating grid area size
            width = topCorner.position.x - bottomCorner.position.x;
            height = topCorner.position.y - bottomCorner.position.y;

            // Determing cell width and height
            cellWidth = width / gridSize.x;
            cellHeight = height / gridSize.y;
        }
    }

    // Grid calculations on editor
    private void OnDrawGizmos()
    {
        // Calculating grid area size (on editor)
        width = topCorner.position.x - bottomCorner.position.x;
        height = topCorner.position.y - bottomCorner.position.y;

        // Determing cell width and height (on editor)
        cellWidth = width / gridSize.x;
        cellHeight = height / gridSize.y;

        // Checking grid size is over zero (avoiding memory overload)
        if (gridSize.x > 0 && gridSize.y > 0)
        {
            Gizmos.color = Color.red;

            // Dividing grid area to as many parts as grid size (x - axis)
            for (float x = bottomCorner.position.x; x < topCorner.position.x; x += cellWidth)
            {
                // Dividing grid area to as many parts as grid size (y - axis)
                for (float y = bottomCorner.position.y; y < topCorner.position.y; y += cellHeight)
                {
                    Gizmos.DrawWireCube(new Vector2(x + cellWidth / 2, y + cellHeight / 2), new Vector2(cellWidth, cellHeight));
                }
            }
        }
    }

    /// <summary>
    /// Returns the position of the nearest cell to the given position
    /// </summary>
    public Vector3 GetClosestPosition(Vector3 position)
    {
        float closestPosition = 0f;
        float distanceToPlayer;
        Vector3 returnPosition = Vector3.zero;

        // Dividing grid area to as many parts as grid size (x - axis)
        for (float x = bottomCorner.position.x; x < topCorner.position.x; x += cellWidth)
        {
            // Dividing grid area to as many parts as grid size (y - axis)
            for (float y = bottomCorner.position.y; y < topCorner.position.y; y += cellHeight)
            {
                // Calculating the position of the nearest cell to the given position
                distanceToPlayer = Vector2.Distance(new Vector2(x + cellWidth / 2, y + cellHeight / 2), position);

                // Setting a starting value to the closestPosition
                if (closestPosition == 0f) closestPosition = distanceToPlayer;

                // Checking if current cell is closer to the given position than the current closest cell
                if (distanceToPlayer < closestPosition)
                {
                    // Setting current distance to be the new closest distance
                    closestPosition = distanceToPlayer;

                    // Setting the position of the cell to return value
                    returnPosition = new Vector2(x + cellWidth / 2, y + cellHeight / 2);
                }
            }
        }
        return returnPosition;
    }
}