using System.Collections.Generic;
using UnityEngine;

public static class Assets
{
    [Tooltip("Returns tokens as list of strings")]
    public static List<string> tokens = new List<string>();

    [Tooltip("Returns maps as list of GameObjects")]
    public static List<GameObject> maps = new List<GameObject>();

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
}
