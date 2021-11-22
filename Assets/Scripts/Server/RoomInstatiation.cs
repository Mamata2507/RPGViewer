using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomInstatiation : MonoBehaviour
{
    private void Start()
    {
        AssetBundles.GetAssetBundles("icons");
        AssetBundles.GetAssetBundles("maps");

        PhotonNetwork.Instantiate(AssetBundles.GetItems("icons")[0].name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
