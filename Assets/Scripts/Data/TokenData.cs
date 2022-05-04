namespace RPG
{
    public class TokenData
    {
        public string path;
        public string name;

        public string width;
        public string height;

        public int disposition;

        public bool vision;
        public string visionRadius;
        public int visionType;

        public bool light;
        public string lightRadius;

        public float lightR, lightG, lightB;
        public int lightType;

        public string fileName;

        public string elevation;
        public string health;

        public float xPos, yPos;

        public TokenData(object[] data)
        {
            path = (string)data[0];
            name = (string)data[1];

            if (data[2] != null) width = (string)data[2];
            if (data[3] != null) height = (string)data[3];

            disposition = (int)data[4];

            vision = (bool)data[5];
            
            if (data[6] != null) visionRadius = (string)data[6];
            visionType = (int)data[7];

            light = (bool)data[8];
            if (data[9] != null) lightRadius = (string)data[9];

            lightR = (float)data[10];
            lightB = (float)data[11];
            lightG = (float)data[12];

            lightType = (int)data[13];
            fileName = (string)data[14];
            
            elevation = (string)data[15];
            health = (string)data[16];

            if (data.Length > 17)
            {
                xPos = (float)data[17];
                yPos = (float)data[18];
            }
        }
    }
}