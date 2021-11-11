using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCorner : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (gameObject.tag == "Top Corner")
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, GameObject.FindGameObjectWithTag("Bottom Corner").transform.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(GameObject.FindGameObjectWithTag("Bottom Corner").transform.position.x, transform.position.y));
        }
        else if (gameObject.tag == "Bottom Corner")
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, GameObject.FindGameObjectWithTag("Top Corner").transform.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(GameObject.FindGameObjectWithTag("Top Corner").transform.position.x, transform.position.y));
        }

    }
}
