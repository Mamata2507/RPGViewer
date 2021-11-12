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
        float yPosition = 820;
        foreach (var icon in icons)
        {
            GameObject instantiatedIcon = Instantiate(iconTemplate, new Vector2(transform.position.x, yPosition), Quaternion.identity, this.transform);
            instantiatedIcon.GetComponent<Image>().sprite = icon.GetComponent<SpriteRenderer>().sprite;
            yPosition -= 100;
        }
    }
}
