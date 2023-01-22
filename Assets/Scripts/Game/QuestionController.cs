using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class QuestionController : Instancable<QuestionController>
    {
        public int currentQuestionIndex;

        [SerializeField] private QuestionView questionView;

        public QuestionData QuestionData => QuestionAPI.questionData;

        private Question CurrentQuestion => QuestionData.questions[currentQuestionIndex];
        


        public IEnumerator UpdateQuestionView()
        {
            QuestionAPI.ReadDataFromJSON();
            yield return new WaitUntil(() => QuestionAPI.questionData != null);
            //CurrentQuestion = QuestionData.questions[currentQuestionIndex];
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
                answerModels = answerModels
            };
            
            questionView.Render(questionModel);
        }
    }
}