using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class GridData
    {
        public static List<Vector2> positions = new List<Vector2>();

        public static float cellSize = 1f;
        public static bool snapToGrid = true;
        
        public static Vector2 GetClosestCell(Vector2 position, float size)
        {
            float closestPosition = 0f;
            float distanceToPlayer = 0f;
            Vector3 returnPosition = Vector3.zero;


            foreach (var p in positions)
            {
                if (size % 10 == 0) distanceToPlayer = Vector2.Distance(new Vector2(p.x + (cellSize / 2f), p.y - (cellSize / 2f)), position);
                else distanceToPlayer = Vector2.Distance(p, position);

                if (closestPosition == 0f)
                {
                    closestPosition = distanceToPlayer;
                    returnPosition = p;
                }
                if (distanceToPlayer < closestPosition)
                {
                    closestPosition = distanceToPlayer;
                    if (size % 10 == 0) returnPosition = new Vector2(p.x + (cellSize / 2f), p.y - (cellSize / 2f));
                    else returnPosition = p;
                }
            }
            if (!snapToGrid || Input.GetKey(KeyCode.LeftControl)) return position;
            else return returnPosition;
        }

        public static float ScaleToGrid(float size)
        {
            float s = cellSize * (size / 5f);
            return s;
        }
    }
}