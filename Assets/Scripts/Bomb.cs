using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 3f;
    public float explosionForce = 10f;
    public float radius = 20f;

    void Start()
    {
        
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
    }
}
