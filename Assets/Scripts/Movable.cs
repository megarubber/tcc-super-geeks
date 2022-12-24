using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public bool isRandomSpeed = true;

    [SerializeField]
    private float speed;

    void Start()
    {
        if(isRandomSpeed)
            speed = Random.Range(maxSpeed, minSpeed);   
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * speed);
    }
}
