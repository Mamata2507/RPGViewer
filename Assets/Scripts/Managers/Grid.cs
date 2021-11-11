using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float gridSize;
    public float width;
    public float height;

    private void OnDrawGizmos()
    {
        if (width > 0 && height > 0)
        {
            Gizmos.color = Color.red;
            
            for (float x = 0; x < width; x ++)
		    {
                for (float y = 0; y < height; y ++)
		        {
                    Gizmos.DrawWireCube(new Vector3(x + transform.position.x, y + transform.position.y), new Vector2(gridSize, gridSize));
		        }
		    }
        }
    }
}
