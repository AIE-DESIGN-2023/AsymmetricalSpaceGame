using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn = false;

    public TextMeshProUGUI timerText;

    AIMechanicController aiMechanicController;

    // Start is called before the first frame update
    void Start()
    {
        aiMechanicController = GetComponentInParent<AIMechanicController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerOn)
        {
            if (timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                aiMechanicController.AIWinGame();
                Debug.Log("Time is UP");
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
