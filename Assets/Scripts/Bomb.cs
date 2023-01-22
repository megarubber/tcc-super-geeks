using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay;
    public float explosionForce = 10f;
    public float radius = 20f;
    private float rotateSpeed = 5f;

    void Update()
    {
        transform.Rotate(rotateSpeed, 0, 0, Space.World);
    }

    void Explosion()
    {
        // 6: Player Layer Mask
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider near in colliders) {
            GameObject p = near.gameObject;
            Rigidbody rb = near.GetComponent<Rigidbody>();

            if(rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other) {
        Invoke("Explosion", delay);
    }
}