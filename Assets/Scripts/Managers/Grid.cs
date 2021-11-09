using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float gridSize = 0.5f;

    private void OnDrawGizmos()
    {
        if (gridSize > 0)
        {
            Gizmos.color = Color.black;
            
            for (float x = -20; x < 20; x += gridSize)
		    {
                for (float y = -20; y < 20; y += gridSize)
		        {
                    Gizmos.DrawWireCube(new Vector3(x, y), new Vector3(gridSize, gridSize));
		        }
		    }
        }
    }
}
