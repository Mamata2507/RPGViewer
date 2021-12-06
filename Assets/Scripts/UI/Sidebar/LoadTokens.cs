using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTokens : MonoBehaviour
{
    [SerializeField] private GameObject tokenTemplate;
    private List<string> tokens = new List<string>();

    private bool reloaded = false;

    private void Update()
    {
        if (Assets.tokens.Count > 0 && !reloaded)
        {
            ReloadTokens();
        }
    }

    /// <summary>
    /// Getting reference of tokens
    /// </summary>
    public void ReloadTokens()
    {
        tokens.Clear();
        tokens = Assets.tokens;
        ScaleRect();
        DisplayTokens();

        reloaded = true;
    }

    /// <summary>
    /// Scaling rect transform to fit all tokens
    /// </summary>
    public void ScaleRect()
    {
        RectTransform rt = GetComponent<RectTransform>();

        for (int i = 0; i < tokens.Count; i++) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 90);

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);
    }

    /// <summary>
    /// Displaying all tokens
    /// </summary>
    private void DisplayTokens()
    {
        Debug.Log(tokens.Count);
        for (int i = 0; i < tokens.Count; i++)
        {
            GameObject instantiatedIcon = Instantiate(tokenTemplate, this.transform);
            instantiatedIcon.transform.position = (new Vector3(transform.position.x, transform.position.y - 60 * i - 40, -15));

            string url = "https://storage.googleapis.com/rpgviewer/Tokens/" + tokens[i] + ".png";
            WebRequest.GetTexture(url, (string error) =>
            {
                Debug.Log("Error: " + error);
            }, (Texture2D texture) =>
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f));
                instantiatedIcon.GetComponent<Image>().sprite = sprite;
            });
        }
    }
}
