using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace RPG
{
    public static class WebRequest
    {
        private class WebRequestMonoBehaviour : MonoBehaviour { }
        private static WebRequestMonoBehaviour webRequestMonoBehaviour;

        /// <summary>
        /// Initiating script
        /// </summary>
        private static void Initiate()
        {
            if (webRequestMonoBehaviour == null)
            {
                GameObject gameObject = new GameObject("WebRequest");
                gameObject.transform.parent = GameObject.FindGameObjectWithTag("Network").transform;
                webRequestMonoBehaviour = gameObject.AddComponent<WebRequestMonoBehaviour>();
            }
        }

        /// <summary>
        /// Get asset bundle
        /// </summary>
        public static void GetBundle(string url, Action<string> onError, Action<UnityEngine.Object[]> onSuccess)
        {
            Initiate();
            webRequestMonoBehaviour.StartCoroutine(GetBundleCoroutine(url, onError, onSuccess));
        }

        /// <summary>
        /// Returning asset bundle on success
        /// </summary>
        private static IEnumerator GetBundleCoroutine(string url, Action<string> onError, Action<UnityEngine.Object[]> onSuccess)
        {
            Initiate();
            using (UnityWebRequest unityWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                yield return unityWebRequest.SendWebRequest();

                Debug.Log(unityWebRequest.downloadProgress);

                if (unityWebRequest.result != UnityWebRequest.Result.Success) onError(unityWebRequest.error);
                else
                {
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(unityWebRequest);
                    UnityEngine.Object[] assets = bundle.LoadAllAssets();
                    yield return new WaitUntil(() => assets.Length > 0);

                    onSuccess(assets);
                }
            }
        }

        /// <summary>
        /// Get texture2D
        /// </summary>
        public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
        {
            Initiate();
            webRequestMonoBehaviour.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
        }

        /// <summary>
        /// Returning texture2D on success
        /// </summary>
        private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
        {
            Initiate();
            using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError) onError(unityWebRequest.error);
                else
                {
                    DownloadHandlerTexture texture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                    onSuccess(texture.texture);
                }
            }
        }

        /// <summary>
        /// Get string
        /// </summary>
        public static void GetString(string url, Action<string> onError, Action<string> onSuccess)
        {
            Initiate();
            webRequestMonoBehaviour.StartCoroutine(GetStringCoroutine(url, onError, onSuccess));
        }

        /// <summary>
        /// Returning string on success
        /// </summary>
        private static IEnumerator GetStringCoroutine(string url, Action<string> onError, Action<string> onSuccess)
        {
            Initiate();
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError) onError(unityWebRequest.error);
                else onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }
}