using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadIcons : MonoBehaviour
{
    [SerializeField] private GameObject iconTemplate;

    private List<GameObject> icons;
    private bool loadingDone = false;

    private void Start()
    {
        icons = AssetBundles.GetItems("icons");
    }

    private void Update()
    {
        if (icons.Count > 0 && loadingDone == false)
        {
            ScaleRect();
            DisplayIcons();
        }
    }

    private void ScaleRect()
    {
        RectTransform rt = GetComponent<RectTransform>();

        for (int i = 0; i < icons.Count; i++) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 90);

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);
    }

    private void DisplayIcons()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            GameObject instantiatedIcon = Instantiate(iconTemplate, this.transform);
            instantiatedIcon.transform.localPosition = (new Vector3(0, transform.position.y - 75 * i - 50, -15));
            instantiatedIcon.GetComponent<Image>().sprite = icons[i].GetComponent<SpriteRenderer>().sprite;
            instantiatedIcon.GetComponent<DragAndInstantiate>().iconPrefab = icons[i];
        }
        loadingDone = true;
    }
}
