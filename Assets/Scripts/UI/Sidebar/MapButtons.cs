using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtons : MonoBehaviour
{
    public GameObject map;

    public GameObject acceptButton;
    public GameObject cancelButton;

    public void AcceptMap()
    {
        MapHandler[] maps = GetComponentsInChildren<MapHandler>();

        foreach (var item in maps)
        {
            if (map == item.mapPrefab)
            {
                item.AcceptMap();
                map = null;
            }

            if (item.acceptButton != null) item.acceptButton.SetActive(false);
            if (item.acceptButton != null) item.cancelButton.SetActive(false);
        }
    }

    public void CancelMap()
    {
        MapHandler[] maps = GetComponentsInChildren<MapHandler>();

        foreach (var item in maps)
        {
            if (item.acceptButton != null) item.acceptButton.SetActive(false);
            if (item.acceptButton != null) item.cancelButton.SetActive(false);
        }
        map = null;
    }
}
