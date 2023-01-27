using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movable : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public bool isRandomSpeed = true;

    public float speed;
    private float realSpeed;
    private PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        realSpeed = speed;
        if(isRandomSpeed)
            realSpeed = Random.Range(maxSpeed, minSpeed);   
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * realSpeed);
        if(Input.GetKeyDown(KeyCode.M) && PhotonNetwork.IsMasterClient)
            view.RPC("aaa", RpcTarget.All);
    }

    public void setRealSpeed(float sp) {
        realSpeed = sp;
    }

    public float getRealSpeed() {
        return realSpeed;
    }

    [PunRPC]
    public void aaa() {
        setRealSpeed(0);
    }
}