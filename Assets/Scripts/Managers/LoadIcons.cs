using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadIcons : MonoBehaviour
{
    [SerializeField] private GameObject iconTemplate;

    private GameObject[] icons;

    private void Start()
    {
        icons = Resources.LoadAll<GameObject>("Prefabs/Icons");
        DisplayIcons();
    }

    private void DisplayIcons()
    {
        float offset = 0f;
        
        RectTransform rt = GetComponent<RectTransform>();
        foreach (var icon in icons) rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 90);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - 25);

        foreach (var icon in icons)
        {
            GameObject instantiatedIcon = Instantiate(iconTemplate, new Vector3(transform.position.x, transform.position.y + offset), Quaternion.identity, this.transform);
            instantiatedIcon.GetComponent<Image>().sprite = icon.GetComponent<SpriteRenderer>().sprite;
            instantiatedIcon.GetComponent<DragAndInstantiate>().iconPrefab = icon;
            offset -= 50f;
        }
    }
}
