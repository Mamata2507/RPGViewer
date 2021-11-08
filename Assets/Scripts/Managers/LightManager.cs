using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LightManager : MonoBehaviourPunCallbacks
{
    public GameObject LightPrefab;
    private GameObject instantiatedLight;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        instantiatedLight = PhotonNetwork.Instantiate(LightPrefab.name, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            FollowTarget();

            if (Input.GetKeyDown(KeyCode.L))
            {
                HandleLighting();
            }
        }
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        instantiatedLight.transform.position = desiredPosition;
    }

    public void HandleLighting()
    {
        instantiatedLight.SetActive(!instantiatedLight.activeInHierarchy);
    }
}
