using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public static class PreparePool
{
    private static List<GameObject> prefabs = new List<GameObject>();
    public static List<GameObject> icons = new List<GameObject>();
    public static List<GameObject> maps = new List<GameObject>();

    public static void AddToPrefabs(GameObject prefab)
    {
        prefabs.Add(prefab);
    }

    public static void ReloadPrefabs()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (pool != null && prefabs != null)
        {
            foreach (GameObject prefab in prefabs)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
                Debug.Log(prefab);
            }
        }
    }
}