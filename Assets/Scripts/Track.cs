using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Track : MonoBehaviour
{
    public GameObject[] buildings;
    private float zPoint;
    private Transform reference;
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
        reference = GameObject.Find("Reference").GetComponent<Transform>();
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
        if (PhotonNetwork.IsMasterClient) {
            int idBuild = Random.Range(0, buildings.Length);

            float xSort = Random.Range(minOffset.x, maxOffset.x);
            float ySort = Random.Range(minOffset.y, maxOffset.y);

            sBuilding = idBuild;
            sPosition = new Vector3(xSort, ySort, offset);
            sRotationY = Random.Range(0f, 180f);

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
        }
        //Debug.Log(sBuilding);
        yield return new WaitForSeconds(time);
    }

    [PunRPC]
    public void SetRandomValuesBuildings(int i, Vector3 v, float f, PhotonMessageInfo info) {
        sBuilding = i;
        sPosition = v;
        sRotationY = f;
        var obj = Instantiate(
            buildings[sBuilding], 
            sPosition, 
            Quaternion.Euler(0f, sRotationY, 0f)
        );
        obj.transform.parent = gameObject.transform;
    }
    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            stream.SendNext(sBuilding);
            stream.SendNext(sPosition);
            stream.SendNext(sRotationY);
        } else if(stream.IsReading) {
            sBuilding = (int)stream.ReceiveNext();
            sPosition = (Vector3)stream.ReceiveNext();
            sRotationY = (float)stream.ReceiveNext();
        }
    }
    */
}