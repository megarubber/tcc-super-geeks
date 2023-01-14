using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackRemover : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Building") {
            Destroy(other.gameObject);
            Debug.Log("eae");
        }
    }
}