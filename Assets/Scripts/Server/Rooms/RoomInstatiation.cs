using UnityEngine;
using System.Collections.Generic;

public class RoomInstatiation : MonoBehaviour
{
    public List<string> tokens = new List<string>();

    private void Start()
    {
        GetTokens();
        AssetBundles.GetMapAssets();
    }

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
            string split = GetBetween(text, "Tokens/", "</ListBucketResult>");
            string[] names = split.Split('/');

            foreach (var name in names)
            {
                if (name.Contains(".png"))
                {
                    Assets.AddToken(GetBetween(name, "", ".png"));
                }
            }
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
}
