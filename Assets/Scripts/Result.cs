using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Result : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;

    void Start()
    {
        PhotonNetwork.Disconnect();
        if(Server.lose) {
            loseScreen.SetActive(true);
            winScreen.SetActive(false);
        } else {
            loseScreen.SetActive(false);
            winScreen.SetActive(true);            
        }
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }
}