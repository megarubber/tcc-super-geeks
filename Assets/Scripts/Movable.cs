using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public bool isRandomSpeed = true;

    public float speed;
    private float realSpeed;

    void Start()
    {
        realSpeed = speed;
        if(isRandomSpeed)
            realSpeed = Random.Range(maxSpeed, minSpeed);   
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * realSpeed);
    }

    public void setRealSpeed(float sp) {
        realSpeed = sp;
    }

    public float getRealSpeed() {
        return realSpeed;
    }
}