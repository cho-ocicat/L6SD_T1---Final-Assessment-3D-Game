using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Timer1 : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float startTime;
    
    GameResult gameResult;

    bool finished = true;

    void Start()
    {
    }

    void Update()
    {
        if(finished){
            return;
        }

        Counting();
    }

    public void StartTimer()
    {
        finished = false;
        startTime = Time.time;
    }

    public void Counting()
    {
        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString();
        //f(number) is how much decimals you want to show
        string seconds = (t % 60).ToString("f2");

        timerText.text = "Time: " + minutes + ":" + seconds;

        if(minutes.Equals("60"))
        {
            timerText.color = Color.red;
            finished = true;
        }
    }

    public void Finish()
    {
        finished = true;
        timerText.color = Color.green;
    }

    public void Result()
    {
        gameResult = FindObjectOfType<GameResult>();

        finished = true;

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(timerText.text.Contains("0:") || timerText.text.Contains("1:"))
            {
                gameResult.Winning();
            }
            else
            {
                gameResult.Losing();
            }
        }
    }
}
