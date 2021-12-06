using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequest
{
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

    /// <summary>
    /// Getting image over the internet
    /// </summary>
    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        webRequestMonoBehaviour.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
    }

    /// <summary>
    /// Getting string over the internet
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
            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
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
            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
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
}