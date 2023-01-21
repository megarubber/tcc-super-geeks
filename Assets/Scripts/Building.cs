using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] items;

    void Start()
    {
        float inst = Random.Range(0f, 1f);
        if(inst > 0.1f) {
            int selSpawnPoint = Random.Range(0, spawnPoints.Length);
            int selItem = Random.Range(0, items.Length);
            Instantiate(items[selItem], spawnPoints[selSpawnPoint]);
        }
    }
}