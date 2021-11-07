using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject globalLight;
    private bool lights;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(0, 2), Random.Range(0, 2));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            switch(lights)
            {
                case true:
                {
                    globalLight.SetActive(false);
                    lights = false;
                    break;
                }
                case false:
                {
                    globalLight.SetActive(true);
                    lights = true;
                    break;
                }
            }
        }
    }
}
