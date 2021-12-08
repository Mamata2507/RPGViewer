using UnityEngine;

public class GridCorner : MonoBehaviour
{
    #region Variables
    // Position of top and bottom corners
    [SerializeField] private Transform topCorner;
    [SerializeField] private Transform bottomCorner;
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Checking if this is the top corner
        if (gameObject.tag == "Top Corner")
        {
            // Drawing lines to left and down
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, bottomCorner.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(bottomCorner.position.x, transform.position.y));
        }

        // Checking if this is the bottom corner
        else if (gameObject.tag == "Bottom Corner")
        {
            // Drawing lines to right and up
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, topCorner.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(topCorner.position.x, transform.position.y));
        }
    }
    #endregion
}