using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadAssetBundles : MonoBehaviour
{
    AssetBundle assetBundle;

    public string path;

    private Object[] assetBundleItems;

    private void Start()
    {
        LoadAssetBundle(path);
        InstantiateObjectFromBundle();
    }

    private void LoadAssetBundle(string bundleUrl)
    {
        assetBundle = AssetBundle.LoadFromFile(bundleUrl);

        Debug.Log(assetBundle == null ? "Failed to load AssetBundle" : "AssetBundle loaded succesfully");
    }

    private void InstantiateObjectFromBundle()
    {
        assetBundleItems = assetBundle.LoadAllAssets();
        foreach (var item in assetBundleItems)
        {
            PreparePool.instance.Prefabs.Add((GameObject)item);
        }

        PreparePool.instance.ReloadPrefabs();
        PhotonNetwork.Instantiate("Ithar", this.transform.position, Quaternion.identity);
    }
}
