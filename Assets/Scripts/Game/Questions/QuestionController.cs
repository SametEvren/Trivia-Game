using System;
using System.Collections;
using System.Collections.Generic;
using Game.Answers;
using Game.Managers;
using UnityEngine;
using UnityEngine.Assertions;
using Utility;

namespace Game.Questions
{
    public class QuestionController : Instancable<QuestionController>
    {
        [SerializeField] private QuestionView questionView;
        [SerializeField] private List<AnswerController> answerControllers;
        
        private QuestionData QuestionData => QuestionAPI.questionData;
        private Question CurrentQuestion => QuestionData.questions[GameManager.Instance.currentQuestionIndex];

        private void OnValidate()
        {
            Assert.IsNotNull(questionView);
            Assert.IsNotNull(answerControllers);
        }

        public void DisableAnswer(string choice)
        {
            int index = choice.ToAnswerIndex();
            if (index < 0 || index >= answerControllers.Count) return;
            
            answerControllers[index].ChangeButtonStatus(AnswerStatus.Inactive);
        }
        
        public void UpdateQuestionView()
        {
            StartCoroutine(UpdateQuestionViewRoutine());
        }
        
        private IEnumerator UpdateQuestionViewRoutine()
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