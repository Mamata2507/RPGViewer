using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MasterClient : MonoBehaviourPun
{
    private AssetBundle icons;
    private AssetBundle maps;

    private AssetBundle localIcons;
    private AssetBundle localMaps;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (icons == null) icons = AssetBundles.iconBundle;
            if (maps == null) maps = AssetBundles.mapBundle;

            if (icons != null)
            {
                try
                {
                    localIcons = AssetBundle.LoadFromFile(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\icon");
                }
                catch (System.Exception)
                {
                    Debug.Log("Changes detected in icons");
                }
            }

            if (maps != null)
            {
                try
                {
                    localMaps = AssetBundle.LoadFromFile(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\map");
                }
                catch (System.Exception)
                {
                    Debug.Log("Changes detected in maps");
                }
            }
        }
    }
    
}
