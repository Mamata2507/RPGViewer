using Photon.Pun;
using UnityEngine;

public class AssetHandler : MonoBehaviour
{
    private void Start()
    {
        GetTokens();
        GetMaps();
    }

    #region tokens
    /// <summary>
    /// Adds each image name from Google Cloud storage to token list
    /// </summary>
    private void GetTokens()
    {
        string url = "https://storage.googleapis.com/rpgviewer/";
        WebRequest.GetString(url, (string error) =>
        {
            Debug.Log("Error: " + error);
        }, (string text) =>
        {
            string names = GetBetween(text, "Tokens/", "</ListBucketResult>");
            string[] images = names.Split('/');

            foreach (var image in images)
            {
                if (image.Contains(".png"))
                {
                    Assets.AddToken(GetBetween(image, "", ".png"));
                    GetSprites(GetBetween(image, "", ".png"));
                }
            }
        });
    }

    private void GetSprites(string name)
    {
        string url = "https://storage.googleapis.com/rpgviewer/Tokens/" + name + ".png";
        WebRequest.GetTexture(url, (string error) =>
        {
            Debug.Log("Error: " + error);
        }, (Texture2D texture) =>
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            Assets.AddTexture(sprite);
        });
    }

    /// <summary>
    /// Returning string between two values
    /// </summary>
    private string GetBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }

        return "";
    }
    #endregion

    #region maps
    /// <summary>
    /// Adds each image name from Google Cloud storage to token list
    /// </summary>
    private void GetMaps()
    {
        string url = "https://storage.googleapis.com/rpgviewer/AssetBundles/maps";
        WebRequest.GetBundle(url, (string error) =>
        {
            Debug.Log("Error: " + error);
        }, (Object[] maps) =>
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

            if (pool != null && maps != null)
            {
                foreach (GameObject map in maps)
                {
                    // Adding each asset to list of maps
                    Assets.AddMap(map);
                    pool.ResourceCache.Add(map.name, map);
                }
            }
        });
    }
    #endregion
}
