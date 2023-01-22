using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class GameManager : Instancable<GameManager>
{
    public TextMeshProUGUI questionBlock;
    public List<TextMeshProUGUI> answerBlocks;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        QuestionController.Instance.UpdateQuestionView();
    }

    public void AnswerTheQ(string answerBlock)
    {
        if (QuestionController.Instance.questionData.questions[QuestionController.Instance.currentQuestionIndex]
                .answer == answerBlock)
        {
            QuestionController.Instance.currentQuestionIndex++;
            QuestionController.Instance.UpdateQuestionView();
        }
    }
}
