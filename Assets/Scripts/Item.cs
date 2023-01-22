using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;

    [SerializeField] private Movable mov;
    [HideInInspector] public float time = 10f;

    void Start()
    {
        mov = GameObject.Find("Reference").GetComponent<Movable>();
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            switch(id) {
                case 0:
                    GameController.counter = time;
                    if(mov.getRealSpeed() > 2f)
                        mov.setRealSpeed(Random.Range(2f, 4f));
                    else
                        mov.setRealSpeed(Random.Range(4f, 5f));
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}