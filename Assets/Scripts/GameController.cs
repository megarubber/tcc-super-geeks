using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static float counter = 0f;
    private static bool startCounter = false;
    public Movable mov;

    [Header("Canvas Variables")]
    public GameObject timerRef;
    public GameObject bombRef;
    private Text bombText;
    private Text timerText;
    public Text jetpackForce;

    void Start()
    {
        timerRef.SetActive(false);
        timerText = timerRef.transform.GetChild(1).gameObject.GetComponent<Text>();
        bombText = bombRef.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        timerText.text = counter.ToString();
        bombText.text = Player.nBombs.ToString();
        jetpackForce.text = Player.jetpackForce.ToString();
        if(startCounter) {
            if(counter < 0) {
                timerRef.SetActive(false);
                mov.setRealSpeed(mov.speed);
            } else {
                timerRef.SetActive(true);
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
