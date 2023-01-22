using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GameManager : Instancable<GameManager>
{
    public static event Action<string,string> OnQuestionAnswered; 
    public static event Action<int> OnQuestionChanged ; 

    public int score;
    public TextMeshProUGUI questionBlock;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI categoryText;
    public Timer timer;
    public int currentQuestionIndex;
    
    private const int CorrectPoint = 10;
    private const int WrongPoint = -5;
    private const int DoesntReplyPoint = -3;
    private IEnumerator Start()
    {
        QuestionAPI.ReadDataFromJSON();
        yield return new WaitUntil(() => QuestionAPI.questionData != null);
        LoadQuestion();
    }

    public void HandleQuestionAnswered(string chosenAnswer)
    {
        string correctAnswer = QuestionAPI.questionData
            .questions[currentQuestionIndex]
            .answer;
        
        OnQuestionAnswered?.Invoke(chosenAnswer,correctAnswer);
        
        if (correctAnswer == chosenAnswer)
        {
            UpdateScoreAndText(CorrectPoint);
        }
        else
        {
            UpdateScoreAndText(WrongPoint);
        }
        
        DOVirtual.DelayedCall(1f, ChangeQuestion);
    }
    
    private void UpdateScoreAndText(int amount)
    {
        float oldScore = score;
        float newScore = oldScore + amount;
        DOTween.To(() => oldScore, x => oldScore = x, newScore, 1f)
            .OnUpdate(() =>
            {
                score = Convert.ToInt16(oldScore);
                scoreText.text = score.ToString();
            });
    }
    
    private void ChangeQuestion()
    {
        currentQuestionIndex++;
        currentQuestionIndex %= QuestionAPI.questionData.questions.Count;
        
        OnQuestionChanged?.Invoke(currentQuestionIndex);
        LoadQuestion();
    }
    
    private void LoadQuestion()
    {
        StartCoroutine(QuestionController.Instance.UpdateQuestionView());
        timer.RestartTimer();
    }
}
