using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class PreparePool : MonoBehaviour
{
    #region Singleton
    public static PreparePool instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public List<GameObject> Prefabs;

    public void ReloadPrefabs()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (pool != null && this.Prefabs != null)
        {
            foreach (GameObject prefab in this.Prefabs)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
        }
    }
}