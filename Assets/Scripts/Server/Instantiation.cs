using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace RPG
{
    public static class Instantiation
    {
        public static void Instantiate()
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                PhotonView[] networkObjects = GameObject.FindObjectsOfType<PhotonView>();

                foreach (var networkObject in networkObjects)
                {
                    if ((string)networkObject.ViewID.ToString().ToCharArray().GetValue(0) == PlayerPrefs.GetInt("PlayerID").ToString()) networkObject.RequestOwnership();
                }
            }
            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                GameObject.FindObjectOfType<CreateAndJoin>().nickname.text = PlayerPrefs.GetString("Nickname");
            }
        }
    } 
}