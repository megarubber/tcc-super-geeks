using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] buildings;
    private int idBuild;
    private float zPoint;
    public Transform reference;
    public float minZOffset;
    public float maxZOffset;
    public int numberBuildings;

    void Start() {
        ResetZPoint();
        InstantiateBuildings();
    }

    void Update()
    {

    }

    void ResetZPoint() {
        zPoint = minZOffset;
    }

    void InstantiateBuildings() {
        for(int i = 0; i < numberBuildings; i++) {
            StartCoroutine(BuildingsPerTime(10f, zPoint));
            zPoint += Random.Range(minZOffset, maxZOffset);
        }
        ResetZPoint();
    }

    IEnumerator BuildingsPerTime(float time, float offset) {
        idBuild = (int)Random.Range(0, buildings.Length - 1);
        Instantiate(buildings[idBuild], new Vector3(0, -10, offset), Quaternion.identity);
        yield return new WaitForSeconds(time);
    }
}