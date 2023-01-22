using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 20.0f;
    public TextMeshProUGUI timerText;
    private bool _canDecrease;
    
    void Update()
    {
        if(time > 0 && _canDecrease)
            time -= Time.deltaTime;
        if (time < 0)
            time = 0;
        
        timerText.text = "Time: " + Mathf.Round(time);

        if (time <= 0.0f)
        {
            GameManager.Instance.HandleQuestionAnswered(String.Empty);
            _canDecrease = false;
            ResetCountdown();
        }
    }

    public void ResetCountdown()
    {
        time = 20.0f;
    }

    public void RestartTimer()
    {
        _canDecrease = true;
        ResetCountdown();
    }
}