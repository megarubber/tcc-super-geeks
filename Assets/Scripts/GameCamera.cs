using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Transform reference;
    public Vector3 offset;

    void Update()
    {
        transform.position = reference.position - offset;
        transform.LookAt(reference);
    }
}