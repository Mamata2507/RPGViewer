using Photon.Pun;
using UnityEngine;
using System.IO;

namespace RPG
{
    public class CloudDownloader: MonoBehaviour
    {
        private void Start()
        {
            GetTokenNames();
            GetMapNames();
        }

        /// <summary>
        /// Adds each image's name from GCS to token names list
        /// </summary>
        private void GetTokenNames()
        {
            string url = "https://storage.googleapis.com/rpgviewer/";
            WebRequest.GetString(url, (string error) => { Debug.Log("Error: " + error); }, (string text) =>
            {
                string images = GetBetween(text, "Tokens/", "</ListBucketResult>");
                string[] names = images.Split('/');

                foreach (var name in names)
                {
                    if (name.Contains(".png"))
                    {
                        string result = GetBetween(name, "", ".png");
                        Assets.AddToken(result, result);
                        GetTokenTextures(result);
                    }
                }
            });
        }

        /// <summary>
        /// Adds each image's texture from GCS to token textures list
        /// </summary>
        private void GetTokenTextures(string name)
        {
            string url = "https://storage.googleapis.com/rpgviewer/Tokens/" + name + ".png";
            WebRequest.GetTexture(url, (string error) => { Debug.Log("Error: " + error); }, (Texture2D texture) =>
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                Assets.AddTexture(name, sprite);
            });
        }

        /// <summary>
        /// Adds each image name from Google Cloud storage to map names list
        /// </summary>
        private void GetMapNames()
        {
            string url = "";
            if (Application.platform == RuntimePlatform.Android)
            {
                url = "https://storage.googleapis.com/rpgviewer/AssetBundles/Android/maps";
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                url = "https://storage.googleapis.com/rpgviewer/AssetBundles/Windows/maps";
            }

            WebRequest.GetBundle(url, (string error) => { Debug.Log("Error: " + error); }, (Object[] maps) =>
            {
                DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
                if (pool != null && maps != null)
                {
                    foreach (GameObject map in maps)
                    {
                        Assets.AddMap(map.name, map);
                        pool.ResourceCache.Add(map.name, map);
                    }
                }
            });
        }

        /// <summary>
        /// Returns string between given values
        /// </summary>
        private string GetBetween(string source, string startValue, string endValue)
        {
            if (source.Contains(startValue) && source.Contains(endValue))
            {
                int Start, End;
                Start = source.IndexOf(startValue, 0) + startValue.Length;
                End = source.IndexOf(endValue, Start);
                return source.Substring(Start, End - Start);
            }

            return "";
        }
    }
}
