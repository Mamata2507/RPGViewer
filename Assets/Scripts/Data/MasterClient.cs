using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class MasterClient
    {
        public static List<GameObject> tokens = new List<GameObject>();

        public static object[] data;
        public static GameObject reference;

        public static bool isMaster;

        public static string RoomName()
        {
            string name = "";
            for (int i = 1; i <= 5; ++i)
            {
                bool upperCase = Random.Range(0, 2) == 1;
                int rand = 0;
                if (upperCase) rand = Random.Range(65, 91);
                else rand = Random.Range(97, 123);

                name += (char)rand;
            }

            return name;
        }
    }
}