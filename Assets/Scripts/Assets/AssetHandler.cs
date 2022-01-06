using Photon.Pun;
using UnityEngine;

namespace RPG
{
    public class AssetHandler : MonoBehaviour
    {
        #region Start & Update
        private void Start()
        {
            GetTokens();
            GetMaps();
        }
        #endregion

        #region Tokens
        /// <summary>
        /// Adds each image name from Google Cloud storage to token list
        /// </summary>
        private void GetTokens()
        {
            // Downloading image names from Google Cloud Storage
            string url = "https://storage.googleapis.com/rpgviewer/";
            WebRequest.GetString(url, (string error) =>
            {
            // informing if error occured
            Debug.Log("Error: " + error);
            }, (string text) =>
            {
            // Finding name of each image
            string names = GetBetween(text, "Tokens/", "</ListBucketResult>");
                string[] images = names.Split('/');

                foreach (var image in images)
                {
                    if (image.Contains(".png"))
                    {
                    // Adding the final name to name lists (ex. Ithar, Kvothe, Geleen...)
                    string result = GetBetween(image, "", ".png");
                        Assets.AddToken(result, result);

                    // Getting the image itself
                    GetSprites(result);
                    }
                }
            });
        }

        private void GetSprites(string name)
        {
            // Downloading images from Google Cloud Storage
            string url = "https://storage.googleapis.com/rpgviewer/Tokens/" + name + ".png";
            WebRequest.GetTexture(url, (string error) =>
            {
            // Informing if error occured
            Debug.Log("Error: " + error);
            }, (Texture2D texture) =>
            {
            // Creating new sprite from texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                Assets.AddTexture(name, sprite);
            });
        }

        /// <summary>
        /// Returning string between two values
        /// </summary>
        private string GetBetween(string strSource, string strStart, string strEnd)
        {
            // Returns value between two given strings
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                // Declaring start and end positions of returned string
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);

                // Returning final value
                return strSource.Substring(Start, End - Start);
            }

            // Return empty if nothing was found
            return "";
        }
        #endregion

        #region Maps
        /// <summary>
        /// Adds each image name from Google Cloud storage to token list
        /// </summary>
        private void GetMaps()
        {
            string url = "";

            // Downloading maps from Google Cloud Storage
            if (UnityEngine.Application.platform == RuntimePlatform.Android) url = "https://storage.googleapis.com/rpgviewer/AssetBundles/Android/maps";
            else if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor || UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer) url = "https://storage.googleapis.com/rpgviewer/AssetBundles/Windows/maps";

            WebRequest.GetBundle(url, (string error) =>
            {
            // Informing if error occured
            Debug.Log("Error: " + error);
            }, (Object[] maps) =>
            {
            // Getting reference of prefab pool
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

                if (pool != null && maps != null)
                {
                    foreach (GameObject map in maps)
                    {
                    // Adding each asset to list of maps
                    Assets.AddMap(map.name, map);
                        pool.ResourceCache.Add(map.name, map);
                    }
                }
            });
        }
        #endregion
    }
}
