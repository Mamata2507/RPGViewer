using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Assets
{
    #region Lists
    [Tooltip("Returns tokens as list of strings")]
    public static Hashtable tokens = new Hashtable();

    [Tooltip("Returns tokens as list of sprites")]
    public static Hashtable textures = new Hashtable();

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
        tokens.Add(key, token);
    }

    /// <summary>
    /// Adds a token as sprite
    /// </summary>
    public static void AddTexture(string key, Sprite sprite)
    {
        textures.Add(key, sprite);
    }
    #endregion
}
