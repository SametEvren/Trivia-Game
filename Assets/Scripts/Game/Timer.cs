using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 20.0f;
    public TextMeshProUGUI timerText;
    public bool canDecrease;
    
    void Update()
    {
        if(time > 0 && canDecrease)
            time -= Time.deltaTime;
        if (time < 0)
            time = 0;
        
        timerText.text = "Time: " + Mathf.Round(time);

        if (time <= 0.0f)
        {
            GameManager.Instance.AnswerTheQ("");
            canDecrease = false;
            RefreshTimer();
        }
    }

    public void RefreshTimer()
    {
        time = 20.0f;
    }
}