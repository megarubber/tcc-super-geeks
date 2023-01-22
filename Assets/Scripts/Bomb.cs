using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 3f;
    public float explosionForce = 10f;
    public float radius = 20f;

    void Explosion()
    {
        // 6: Player Layer Mask
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider near in colliders) {
            GameObject p = near.gameObject;
            Rigidbody rb = near.GetComponent<Rigidbody>();

            if(rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Acceleration);
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        Explosion();
        Destroy(gameObject);
    }
}
