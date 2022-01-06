using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class LoadMaps : MonoBehaviour
    {
        #region Variables
        // Template image to show on sidebar
        [SerializeField] private GameObject mapTemplate;

        // List of maps as GameObjects
        private List<GameObject> maps = new List<GameObject>();

        // Maps loaded
        private bool mapsLoaded = false;
        #endregion

        #region Start & Update
        private void Update()
        {
            // Getting reference of available maps
            if (Assets.maps.Count > 0 && !mapsLoaded)
            {
                foreach (DictionaryEntry entry in Assets.maps) maps.Add((GameObject)entry.Value);

                ScaleRect();
                DisplayMaps();

                mapsLoaded = true;
            }
        }
        #endregion

        #region Rect Scaling
        /// <summary>
        /// Scaling rect transform to fit all maps
        /// </summary>
        private void ScaleRect()
        {
            // Getting reference of rect transform
            RectTransform rt = GetComponent<RectTransform>();

            // Scaling rect for each token instantiated
            for (int i = 0; i < maps.Count / 2; i++) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 100);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);
        }
        #endregion

        #region Display Maps
        /// <summary>
        /// Displaying all maps
        /// </summary>
        private void DisplayMaps()
        {
            for (int i = 0; i < maps.Count; i++)
            {
                // Displaying map
                GameObject instantiatedMap = Instantiate(mapTemplate, this.transform);

                // Moving map to right position
                instantiatedMap.transform.localPosition = (new Vector3(0, transform.position.y - 230 * i - 120, -15));

                // Setting correct image for each map
                instantiatedMap.GetComponent<Image>().sprite = maps[i].GetComponent<SpriteRenderer>().sprite;

                // Setting map prefab for each map to instantiate
                instantiatedMap.GetComponent<MapHandler>().mapPrefab = maps[i];

            }
        }
        #endregion
    }
}
