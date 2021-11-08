using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapGenerator : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject mapPrefab;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(0, 2), Random.Range(0, 2));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        PhotonNetwork.Instantiate(mapPrefab.name, transform.position, Quaternion.identity);
    }
}