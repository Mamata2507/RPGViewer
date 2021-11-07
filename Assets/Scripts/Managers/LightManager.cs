using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LightManager : MonoBehaviour
{
    public GameObject LightPrefab;
    private GameObject playerLight;

    private void Start()
    {
        playerLight = PhotonNetwork.Instantiate(LightPrefab.name, transform.position, Quaternion.identity);
        playerLight.transform.parent = this.transform;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        playerLight.transform.position = desiredPosition;
    }
}
