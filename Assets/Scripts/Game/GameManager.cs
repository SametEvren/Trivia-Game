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
    private const int CorrectPoint = 10;
    private const int WrongPoint = -5;
    private const int DoesntReplyPoint = -3;
    
    public int score;
    public List<TextMeshProUGUI> answerBlocks;
    public TextMeshProUGUI questionBlock;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI categoryText;
    public Timer timer;
    public int currentQuestionIndex;
    
    private IEnumerator Start()
    {
        QuestionAPI.ReadDataFromJSON();
        yield return new WaitUntil(() => QuestionAPI.questionData != null);
        StartCoroutine(QuestionController.Instance.UpdateQuestionView());
    }

    public void AnswerTheQ(string answerBlock)
    {
        string correctAnswer = QuestionAPI.questionData
            .questions[currentQuestionIndex]
            .answer;
        if (correctAnswer == answerBlock)
        {
            ChangeColor(correctAnswer, answerBlock, Color.green);
            UpdateScoreAndText(CorrectPoint);
        }
        else
        {
            ChangeColor(correctAnswer, answerBlock, Color.red);
            UpdateScoreAndText(WrongPoint);
        }
    }

    private void ChangeColor(string correctAnswer, string answerBlock, Color color)
    {
        for (int i = 0; i < answerBlocks.Count; i++)
        {
            if(answerBlocks[i].transform.parent.GetComponent<AnswerView>().answerChoice == correctAnswer)
            {
                var sequence = DOTween.Sequence()
                    .Append(answerBlocks[i].transform.parent.GetComponent<Image>().DOColor(Color.green, 1f));
            }
            else if (answerBlocks[i].transform.parent.GetComponent<AnswerView>().answerChoice == answerBlock)
            {
                var sequence = DOTween.Sequence()
                    .Append(answerBlocks[i].transform.parent.GetComponent<Image>().DOColor(color, 1f));
            }
            else
            {
                var sequence = DOTween.Sequence()
                    .Append(answerBlocks[i].transform.parent.GetComponent<Image>().DOColor(Color.gray, 1.1f)).OnComplete(
                        () =>
                        {
                            for (int i = 0; i < answerBlocks.Count; i++)
                            {
                                answerBlocks[i].transform.parent.GetComponent<Image>().color = Color.white;
                            }
                            ChangeQuestion();
                        });
            }
        }
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
    
    public void ChangeQuestion()
    {
        currentQuestionIndex++;
        currentQuestionIndex %= QuestionAPI.questionData.questions.Count;
        
        StartCoroutine(QuestionController.Instance.UpdateQuestionView());
        timer.canDecrease = true;
        timer.RefreshTimer();
    }
}
