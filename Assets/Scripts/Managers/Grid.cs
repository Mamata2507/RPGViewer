using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int gridSize = 1;

    private void OnDrawGizmos()
    {
        if (gridSize > 0)
        {
            Gizmos.color = Color.yellow;
            for (int x = -20; x < 20; x += gridSize)
		    {
                for (int y = -20; y < 20; y += gridSize)
		        {
                    Gizmos.DrawWireSphere(new Vector3(x, y), 0.1f);
		        }
		    }
        }
    }
}
