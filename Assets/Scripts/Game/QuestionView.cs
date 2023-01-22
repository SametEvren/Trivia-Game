using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class QuestionView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private List<AnswerView> answers;

    public void Render(QuestionModel model)
    {
        question.text = model.questionText;

        for (var i = 0; i < answers.Count; i++)
        {
            var answer = answers[i];
            var answerModel = model.answerModels[i];
            answer.Render(answerModel);
        }
    }
}
