using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace RPG
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private GameObject options, selections;

        public void OpenOptions()
        {
            options.SetActive(true);
            selections.SetActive(false);
        }

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }

        public void ExitGame()
        {
            gameObject.SetActive(false);
            
            PhotonNetwork.LeaveRoom(false);
            SceneManager.LoadScene("Lobby");
        }
    }
}