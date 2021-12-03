using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomInstatiation : MonoBehaviour
{
    private void Start()
    {
        AssetBundles.GetAssetBundles("icons");
        AssetBundles.GetAssetBundles("maps");
    }

    private void Update()
    {
        if (File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt") == "true")
        {
            Debug.Log("Update Bundles");

            //AssetBundle.UnloadAllAssetBundles(true);

            AssetBundles.GetAssetBundles("icons");
            AssetBundles.GetAssetBundles("maps");

            File.WriteAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt", "false");
        }
    }
}
