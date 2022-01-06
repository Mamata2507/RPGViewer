using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace RPG
{
    public class LoadTokens : MonoBehaviour
    {
        #region Variables
        // Template to instantiate
        [SerializeField] private GameObject tokenTemplate;

        // List of instantiated tokens as strings
        private List<string> tokens = new List<string>();

        // Reload tokens only once
        private bool reloaded = false;

        // Reference of grid
        private GridManager grid;
        #endregion

        #region Start & Update
        private void Update()
        {
            // Getting reference of grid
            if (FindObjectOfType<GridManager>() != null) grid = FindObjectOfType<GridManager>();

            // Reload tokens when list changes
            if (Assets.tokenNames.Count > 0 && !reloaded && grid != null)
            {
                ReloadTokens();
            }
        }
        #endregion

        #region Start Process
        /// <summary>
        /// Getting reference of tokens
        /// </summary>
        public void ReloadTokens()
        {
            tokens.Clear();

            foreach (DictionaryEntry entry in Assets.tokenNames)
            {
                tokens.Add((string)entry.Value);
            }

            ScaleRect();
            DisplayTokens();

            reloaded = true;
        }
        #endregion

        #region Rect Scaling
        /// <summary>
        /// Scaling rect transform to fit all tokens
        /// </summary>
        public void ScaleRect()
        {
            // Getting reference of rect transform
            RectTransform rt = GetComponent<RectTransform>();

            // Scaling rect for each token instantiated
            for (int i = 0; i < tokens.Count; i++) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 90);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);
        }
        #endregion

        #region Display Tokens
        /// <summary>
        /// Displaying all tokens
        /// </summary>
        private void DisplayTokens()
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                // Displaying token
                GameObject instantiatedIcon = Instantiate(tokenTemplate, this.transform);

                // Moving token to right position
                instantiatedIcon.transform.localPosition = (new Vector3(0, transform.position.y - 75 * i - 50, -15));

                // Setting custom name for each token
                instantiatedIcon.name = tokens[i];

                // Downloading token image from Google Cloud Storage
                string url = "https://storage.googleapis.com/rpgviewer/Tokens/" + tokens[i] + ".png";
                WebRequest.GetTexture(url, (string error) =>
                {
                // Informing if error occured
                Debug.Log("Error: " + error);
                }, (Texture2D texture) =>
                {
                // Creating new sprite from texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                // Setting token's sprite to downloaded image
                instantiatedIcon.GetComponent<Image>().sprite = sprite;
                });
            }
        }
        #endregion
    }
}
