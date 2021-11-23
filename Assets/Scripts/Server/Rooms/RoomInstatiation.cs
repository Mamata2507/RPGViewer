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
    }
}
