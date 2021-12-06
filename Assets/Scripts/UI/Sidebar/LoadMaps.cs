using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMaps : MonoBehaviour
{
    // Template image to show on sidebar
    [SerializeField] private GameObject mapTemplate;

    // List of maps as GameObjects
    private List<GameObject> maps = new List<GameObject>();

    // Maps loaded
    private bool loadingDone = false;
    private bool mapsLoaded = false;

    private void Update()
    {
        if (Assets.maps.Count > 0 && !mapsLoaded)
        {
            Debug.Log(Assets.maps.Count);
            maps = Assets.maps;
            mapsLoaded = true;
        }

        if (maps.Count > 0 && loadingDone == false)
        {
            ScaleRect();
            DisplayMaps();
        }
    }

    /// <summary>
    /// Scaling rect transform to fit all maps
    /// </summary>
    private void ScaleRect()
    {
        RectTransform rt = GetComponent<RectTransform>();

        for (int i = 0; i < maps.Count; i++) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 100);

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);
    }

    /// <summary>
    /// Displaying all maps
    /// </summary>
    private void DisplayMaps()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            GameObject instantiatedMap = Instantiate(mapTemplate, this.transform);
            instantiatedMap.transform.position = (new Vector3(transform.position.x, transform.position.y - 230 * i - 120, -15));
            instantiatedMap.GetComponent<Image>().sprite = maps[i].GetComponent<SpriteRenderer>().sprite;
            instantiatedMap.GetComponent<MapHandler>().mapPrefab = maps[i];

        }
        loadingDone = true;
    }
}
