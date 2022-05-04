using UnityEngine;

namespace RPG
{
    public class SceneData
    {
        public string path;
        public string name;

        public bool snapToGrid;
        
        public string columns;
        public string rows;

        public Vector2 area;
        public Vector2 position;
        public Vector2 startPos;

        public float gridR, gridG, gridB;
        public string opacity;

        public bool vision;

        public string fileName;

        public SceneData(object[] data)
        {
            path = (string)data[0];
            name = (string)data[1];

            snapToGrid = (bool)data[2];
            
            columns = (string)data[3];
            rows = (string)data[4];

            area = (Vector2)data[5];
            position = (Vector2)data[6];
            startPos = (Vector2)data[7];
            
            gridR = (float)data[8];
            gridB = (float)data[9];
            gridG = (float)data[10];

            opacity = (string)data[11];
            vision = (bool)data[12];

            fileName = (string)data[13];
        }
    }
}