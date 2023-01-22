using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class QuestionController : Instancable<QuestionController>
    {
        [SerializeField] private QuestionView questionView;
        public QuestionData questionData;
        public int currentQuestionIndex;
        private Question CurrentQuestion; //=> _questionData.questions[_currentQuestionIndex];
        


        public void UpdateQuestionView()
        {
            CurrentQuestion = questionData.questions[currentQuestionIndex];
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