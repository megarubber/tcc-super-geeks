using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Track : MonoBehaviour
{
    public GameObject[] buildings;
    private float zPoint;
    public Transform reference;
    public Vector3 maxOffset;
    public Vector3 minOffset;
    public int numberBuildings;
    public float timeBetweenSpawn;
    private float timer;
    private PhotonView view;

    // Photon
    private int sBuilding;
    private Vector3 sPosition;
    private float sRotationY;

    void Start() {
        view = GetComponent<PhotonView>();
        ResetZPoint();
        InstantiateBuildings();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBetweenSpawn) {
            timer = 0;
            InstantiateBuildings();
        }
    }

    void ResetZPoint() {
        zPoint = minOffset.z;
    }

    void InstantiateBuildings() {
        for(int i = 0; i < numberBuildings; i++) {
            StartCoroutine(BuildingsPerTime(10f, zPoint));
            zPoint += Random.Range(maxOffset.z, minOffset.z);
        }
        //ResetZPoint();
    }

    IEnumerator BuildingsPerTime(float time, float offset) {
        int idBuild = Random.Range(0, buildings.Length);
        //Debug.Log(idBuild);

        float xSort = Random.Range(minOffset.x, maxOffset.x);
        float ySort = Random.Range(minOffset.y, maxOffset.y);

        view.RPC("SetRandomValuesBuildings",
            RpcTarget.OthersBuffered,
            idBuild,
            new Vector3(xSort, ySort, offset),
            Random.Range(0f, 180f)
        );

        var obj = Instantiate(
            buildings[sBuilding], 
            sPosition, 
            Quaternion.Euler(0f, sRotationY, 0f)
        );
        obj.transform.parent = gameObject.transform;

        yield return new WaitForSeconds(time);
    }

    [PunRPC]
    void SetRandomValuesBuildings(int i, Vector3 v, float f) {
        Debug.Log(sBuilding);
        sBuilding = i;
        sPosition = v;
        sRotationY = f;
    }
}