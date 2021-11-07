using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LightManager : MonoBehaviour
{
    public GameObject LightPrefab;
    private GameObject playerLight;
    private PhotonView photonView;
    private List<GameObject> lights = new List<GameObject>();

    private void Start()
    {
        foreach (var light in GameObject.FindGameObjectsWithTag("Light"))
        {
            lights.Add(light);
        }
        photonView = GetComponent<PhotonView>();
        playerLight = PhotonNetwork.Instantiate(LightPrefab.name, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        foreach (var item in lights)
        {
            if (photonView.IsMine == false)
            {
                item.SetActive(false);
            }
        }

        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        playerLight.transform.position = desiredPosition;
    }
}
