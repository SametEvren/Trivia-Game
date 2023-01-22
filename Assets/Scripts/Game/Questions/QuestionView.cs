using System.Collections.Generic;
using Game.Answers;
using TMPro;
using UnityEngine;

namespace Game.Questions
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI category;
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private List<AnswerView> answers;
    

        public void Render(QuestionModel model)
        {
            category.text = model.categoryText;
            question.text = model.questionText;
            for (var i = 0; i < answers.Count; i++)
            {
                var answer = answers[i];
                var answerModel = model.answerModels[i];
                answer.Render(answerModel);
            }
        }
    }
}
