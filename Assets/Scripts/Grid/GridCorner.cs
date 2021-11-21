using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCorner : MonoBehaviour
{
    [SerializeField] private GameObject topCorner;
    [SerializeField] private GameObject bottomCorner;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (gameObject.tag == "Top Corner")
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, bottomCorner.transform.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(bottomCorner.transform.position.x, transform.position.y));
        }
        else if (gameObject.tag == "Bottom Corner")
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, topCorner.transform.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(topCorner.transform.position.x, transform.position.y));
        }
    }
}
