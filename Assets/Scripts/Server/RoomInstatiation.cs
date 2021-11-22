using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomInstatiation : MonoBehaviour
{
    private bool mapInstantiated = false;

    private void Start()
    {
        AssetBundles.GetAssetBundles("icons");
        AssetBundles.GetAssetBundles("maps");
    }

    private void Update()
    {
        if (AssetBundles.GetItems("maps").Count > 0 && !mapInstantiated)
        {
            PreparePool.ReloadPrefabs();
            mapInstantiated = true;
            PhotonNetwork.Instantiate(AssetBundles.GetItems("maps")[0].name, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
