using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMaps : MonoBehaviour
{
    [SerializeField] private GameObject mapTemplate;

    private List<GameObject> maps;
    private bool loadingDone = false;

    private void Start()
    {
        maps = AssetBundles.GetItems("maps");
    }

    private void Update()
    {
        if (maps.Count > 0 && loadingDone == false)
        {
            ScaleRect();
            DisplayMaps();
        }
    }

    private void ScaleRect()
    {
        RectTransform rt = GetComponent<RectTransform>();

        for (int i = 0; i < maps.Count; i++) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 100);

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);
    }

    private void DisplayMaps()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            GameObject instantiatedMap = Instantiate(mapTemplate, this.transform);
            instantiatedMap.transform.localPosition = (new Vector3(0, transform.position.y - 230 * i - 120, -15));
            instantiatedMap.GetComponent<Image>().sprite = maps[i].GetComponent<SpriteRenderer>().sprite;
            instantiatedMap.GetComponent<MapHandler>().mapPrefab = maps[i];

        }
        loadingDone = true;
    }
}
