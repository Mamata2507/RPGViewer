using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapGenerator : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject mapPrefab;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        PhotonNetwork.Instantiate("Prefabs/Icons/" + playerPrefab.name, new Vector2(-4, 2), Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("GenerateMap", RpcTarget.All);
        }
    }

    [PunRPC]
    private void GenerateMap()
    {
        PhotonNetwork.Instantiate("Prefabs/Maps/" + mapPrefab.name, transform.position, Quaternion.identity);
    }
}