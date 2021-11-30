using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstatiation : MonoBehaviour
{
    private void Start()
    {
        AssetBundles.GetAssetBundles("icons");
        AssetBundles.GetAssetBundles("maps");
    }
}
