using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float counter = 0f;
    private static bool startCounter = false;
    private Movable mov;

    void Start()
    {
        mov = GameObject.Find("Reference").GetComponent<Movable>();
    }

    void Update()
    {
        if(startCounter) {
            if(counter < 0) {
                mov.setRealSpeed(mov.speed);
            } else {
                counter -= Time.deltaTime;
            }
        }
    }

    public static void ResetTimer() {
        counter = 0f;
        startCounter = false;
    }

    public static void StartCounter() {
        ResetTimer();
        startCounter = true;
    }
}
