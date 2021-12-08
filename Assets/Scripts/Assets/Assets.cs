using System.Collections.Generic;
using UnityEngine;

public static class Assets
{
    #region Lists
    [Tooltip("Returns tokens as list of strings")]
    public static List<string> tokens = new List<string>();

    [Tooltip("Returns tokens as list of sprites")]
    public static List<Sprite> textures = new List<Sprite>();

    [Tooltip("Returns maps as list of GameObjects")]
    public static List<GameObject> maps = new List<GameObject>();
    #endregion

    #region List Handlers
    /// <summary>
    /// Adds a map as GameObject
    /// </summary>
    public static void AddMap(GameObject map)
    {
        maps.Add(map);
    }

    /// <summary>
    /// Adds a token as string
    /// </summary>
    public static void AddToken(string token)
    {
        tokens.Add(token);
    }

    /// <summary>
    /// Adds a token as sprite
    /// </summary>
    public static void AddTexture(Sprite sprite)
    {
        textures.Add(sprite);
    }
    #endregion
}
