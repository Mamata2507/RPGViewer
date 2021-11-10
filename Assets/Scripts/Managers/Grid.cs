using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float gridSize;

    private void OnDrawGizmos()
    {
        if (gridSize > 0)
        {
            Gizmos.color = Color.yellow;
            
            for (float x = -20; x < 20; x += gridSize)
		    {
                for (float y = -20; y < 20; y += gridSize)
		        {
                    Gizmos.DrawWireSphere(new Vector3(x + transform.position.x, y + transform.position.y), 0.1f);
		        }
		    }
        }
    }
}
