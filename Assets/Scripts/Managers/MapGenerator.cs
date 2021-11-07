using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject map;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(0, 2), Random.Range(0, 2));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        Instantiate(map, transform.position, Quaternion.identity);
    }
}
