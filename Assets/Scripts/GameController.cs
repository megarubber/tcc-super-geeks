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
    public GameObject jetpackRef;
    private Text bombText;
    private Text timerText;
    public RectTransform jetpackBar;

    void Start()
    {
        timerRef.SetActive(false);
        jetpackRef.SetActive(false);
        timerText = timerRef.transform.GetChild(1).gameObject.GetComponent<Text>();
        bombText = bombRef.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        timerText.text = counter.ToString();
        bombText.text = Player.nBombs.ToString();
        if(calJetpackBar() >= 0)
            jetpackBar.localScale = new Vector3(calJetpackBar(), 1f, 1f);
        else
            jetpackBar.localScale = new Vector3(0f, 1f, 1f);

        jetpackRef.SetActive(Player.jetpackMode);

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

    float calJetpackBar() {
        return (float)Player.jetpackForce / Player.maxJetpackForce;
    }
}