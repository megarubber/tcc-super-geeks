using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Generic Item")]
public class Item : ScriptableObject
{
    public string name;
    public GameObject pickupModel;
    public GameObject model;
    public float force;
}