using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class AssetBundles
{
    private class AssetBundlesMonoBehaviour : MonoBehaviour { }

    private static AssetBundlesMonoBehaviour assetBundlesMonoBehaviour;

    private static Object[] icons;
    private static Object[] maps;

    private static void Init()
    {
        if (assetBundlesMonoBehaviour == null)
        {
            GameObject gameObject = new GameObject("AssetBundles");
            assetBundlesMonoBehaviour = gameObject.AddComponent<AssetBundlesMonoBehaviour>();
        }
    }

    public static List<GameObject> GetItems(string type)
    {
        if (type == "icons")
        {
            return PreparePool.icons;
        }
        else if (type == "maps")
        {
            return PreparePool.maps;
        }
        else return null;
    }

    public static void GetAssetBundles(string type)
    {
        Init();

        if (type == "icons")
        {
            assetBundlesMonoBehaviour.StartCoroutine(GetIcons("https://github.com/FrostCrystals/RPGViewer/blob/networking/AssetBundles/icons?raw=true"));
        }
        else if (type == "maps")
        {
            assetBundlesMonoBehaviour.StartCoroutine(GetMaps("https://github.com/FrostCrystals/RPGViewer/blob/networking/AssetBundles/maps?raw=true"));
        }
    }

    private static IEnumerator GetIcons(string path)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            assetBundlesMonoBehaviour.StartCoroutine(GetIcons(path));
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(www);
            icons = assetBundle.LoadAllAssets();
            yield return new WaitUntil(() => icons.Length > 0);

            foreach (var item in icons)
            {
                PreparePool.icons.Add((GameObject)item);
                PreparePool.AddToPrefabs((GameObject)item);
            }
        }
    }

    private static IEnumerator GetMaps(string path)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            assetBundlesMonoBehaviour.StartCoroutine(GetMaps(path));
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(www);
            maps = assetBundle.LoadAllAssets();
            yield return new WaitUntil(() => maps.Length > 0);
            
            foreach (var item in maps)
            {
                PreparePool.maps.Add((GameObject)item);
                PreparePool.AddToPrefabs((GameObject)item);
            }
        }
    }
}