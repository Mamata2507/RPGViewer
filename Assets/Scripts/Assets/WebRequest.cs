using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

public static class WebRequest
{
    #region init
    // Creating game object to be able to use Coroutines
    private class WebRequestMonoBehaviour : MonoBehaviour { }
    private static WebRequestMonoBehaviour webRequestMonoBehaviour;

    /// <summary>
    /// Creating Game Object and attaching MonoBehaviour script to it
    /// </summary>
    private static void Init()
    {
        if (webRequestMonoBehaviour == null)
        {
            GameObject gameObject = new GameObject("WebRequest");
            webRequestMonoBehaviour = gameObject.AddComponent<WebRequestMonoBehaviour>();
        }
    }
    #endregion

    #region maps
    /// <summary>
    /// Getting AssetBundle from internet
    /// </summary>
    public static void GetBundle(string url, Action<string> onError, Action<UnityEngine.Object[]> onSuccess)
    {
        Init();
        webRequestMonoBehaviour.StartCoroutine(GetBundleCoroutine(url, onError, onSuccess));
    }

    /// <summary>
    /// Returning AssetBundle on success
    /// </summary>
    private static IEnumerator GetBundleCoroutine(string url, Action<string> onError, Action<UnityEngine.Object[]> onSuccess)
    {
        Init();
        using (UnityWebRequest unityWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return unityWebRequest.SendWebRequest();

            // Informing if error occured while downloading assets
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                onError(unityWebRequest.error);
            }
            else
            {
                // Returning list of assets
                AssetBundle mapBundle = DownloadHandlerAssetBundle.GetContent(unityWebRequest);
                UnityEngine.Object[] maps = mapBundle.LoadAllAssets();

                yield return new WaitUntil(() => maps.Length > 0);

                onSuccess(maps);
            }
        }
    }
    #endregion

    #region tokens
    /// <summary>
    /// Getting image from internet
    /// </summary>
    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        webRequestMonoBehaviour.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
    }

    /// <summary>
    /// Getting string from internet
    /// </summary>
    public static void GetString(string url, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestMonoBehaviour.StartCoroutine(GetStringCoroutine(url, onError, onSuccess));
    }

    /// <summary>
    /// Returning the texture on success
    /// </summary>
    private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return unityWebRequest.SendWebRequest();

            // Informing if error occured
            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError(unityWebRequest.error);
            }
            else
            {
                // Returning texture
                DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                onSuccess(downloadHandlerTexture.texture);
            }
        }
    }

    /// <summary>
    /// Returning the string on success
    /// </summary>
    private static IEnumerator GetStringCoroutine(string url, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            yield return unityWebRequest.SendWebRequest();

            // Informing if error occured
            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError(unityWebRequest.error);
            }
            else
            {
                // Returnig text
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }
    #endregion
}