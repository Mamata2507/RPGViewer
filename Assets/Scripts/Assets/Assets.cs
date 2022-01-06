using System.Collections;
using UnityEngine;

namespace RPG
{
    public static class Assets
    {
        #region Lists
        [Tooltip("Returns tokens as list of strings")]
        public static Hashtable tokenNames = new Hashtable();

        [Tooltip("Returns tokens as list of sprites")]
        public static Hashtable tokenTextures = new Hashtable();

        [Tooltip("Returns maps as list of GameObjects")]
        public static Hashtable maps = new Hashtable();
        #endregion

        #region List Handlers
        /// <summary>
        /// Adds a map as GameObject
        /// </summary>
        public static void AddMap(string key, GameObject map)
        {
            maps.Add(key, map);
        }

        /// <summary>
        /// Adds a token as string
        /// </summary>
        public static void AddToken(string key, string token)
        {
            tokenNames.Add(key, token);
        }

        /// <summary>
        /// Adds a token as sprite
        /// </summary>
        public static void AddTexture(string key, Sprite sprite)
        {
            tokenTextures.Add(key, sprite);
        }
        #endregion
    }
}