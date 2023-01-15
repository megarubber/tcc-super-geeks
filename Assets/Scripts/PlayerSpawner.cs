using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject myPlayer;
    public List<Transform> spawnPoints;

    void Start()
    {
        int i = Random.Range(0, spawnPoints.Count);
        PhotonNetwork.Instantiate(myPlayer.name, spawnPoints[i].position, spawnPoints[i].rotation, 0);
    }
}