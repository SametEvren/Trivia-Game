using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Game
{
    public class QuestionController : Instancable<QuestionController>
    {
        
        [SerializeField] private QuestionView questionView;
        
        private QuestionData QuestionData => QuestionAPI.questionData;
        private Question CurrentQuestion => QuestionData.questions[GameManager.Instance.currentQuestionIndex];
        
        public IEnumerator UpdateQuestionView()
        {
            QuestionAPI.ReadDataFromJSON();
            yield return new WaitUntil(() => QuestionAPI.questionData != null);
            List<AnswerModel> answerModels = new();
            
            foreach (var answerText in CurrentQuestion.choices)
            {
                answerModels.Add(new AnswerModel()
                {
                    text = answerText
                });
            }
            
            var questionModel = new QuestionModel()
            {
                questionText = CurrentQuestion.question,
                categoryText = CurrentQuestion.category.ToCategoryName(),
                answerModels = answerModels
            };
            
            questionView.Render(questionModel);
        }
    }
}