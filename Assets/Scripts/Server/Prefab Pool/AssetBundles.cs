using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

public static class AssetBundles
{
    // Creating game object to be able to use Coroutines
    private class AssetBundlesMonoBehaviour : MonoBehaviour { }
    private static AssetBundlesMonoBehaviour assetBundlesMonoBehaviour;

    // List of each asset in AssetBundle
    private static Object[] maps;

    // AssetBundle of the maps
    public static AssetBundle mapBundle;

    /// <summary>
    /// Creating Game Object and attaching MonoBehaviour script to it
    /// </summary>
    private static void Init()
    {
        if (assetBundlesMonoBehaviour == null)
        {
            GameObject gameObject = new GameObject("AssetBundles");
            assetBundlesMonoBehaviour = gameObject.AddComponent<AssetBundlesMonoBehaviour>();
        }
    }

    /// <summary>
    /// Getting the AssetBundle from Google Cloud Storage
    /// </summary>
    public static void GetMapAssets()
    {
        Init();

        assetBundlesMonoBehaviour.StartCoroutine(GetMaps("https://storage.googleapis.com/rpgviewer/AssetBundles/maps"));
    }

    /// <summary>
    /// Handling networking of getting AssetBundle
    /// </summary>
    private static IEnumerator GetMaps(string path)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return www.SendWebRequest();

        // Informing if error occured while downloading assets
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Downloading AssetBundle
            mapBundle = DownloadHandlerAssetBundle.GetContent(www);

            maps = mapBundle.LoadAllAssets();
            yield return new WaitUntil(() => maps.Length > 0);
            
            foreach (var map in maps)
            {
                // Adding each asset to list of maps
                Assets.AddMap((GameObject)map);
            }

            ReloadPrefabs();
        }
    }

    /// <summary>
    /// Adding all maps to prefab pool
    /// </summary>
    public static void ReloadPrefabs()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (pool != null && Assets.maps != null)
        {
            foreach (GameObject map in Assets.maps)
            {
                pool.ResourceCache.Add(map.name, map);
            }
        }
    }
}