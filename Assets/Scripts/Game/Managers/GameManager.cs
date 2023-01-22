using System;
using System.Collections;
using DG.Tweening;
using Game.Questions;
using Game.Score;
using TMPro;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Instancable<GameManager>
    {
        public static event Action<string,string> OnQuestionAnswered; 
        public static event Action<int> OnQuestionChanged ; 

        public int score;
        public TextMeshProUGUI scoreText;
        public Timer.Timer timer;
        public int currentQuestionIndex;

        private QuestionData _questionData;
        private ScoreConfig _scoreConfig;
        
        private IEnumerator Start()
        {
            QuestionAPI.ReadDataFromJSON();
            yield return new WaitUntil(() => QuestionAPI.questionData != null);
            _questionData = QuestionAPI.questionData;
            
            ScoreAPI.ReadDataFromJSON();
            yield return new WaitUntil(() => ScoreAPI.scoreConfig != null);
            _scoreConfig = ScoreAPI.scoreConfig;
            
            LoadQuestion();
        }

        public void HandleQuestionAnswered(string chosenAnswer)
        {
            string correctAnswer = _questionData
                .questions[currentQuestionIndex]
                .answer;
        
            OnQuestionAnswered?.Invoke(chosenAnswer,correctAnswer);

            if (chosenAnswer == String.Empty)
                UpdateScoreAndText(_scoreConfig.timeout);
            else
                UpdateScoreAndText(correctAnswer == chosenAnswer ? _scoreConfig.correct : _scoreConfig.wrong);

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
            currentQuestionIndex %= _questionData.questions.Count;
        
            OnQuestionChanged?.Invoke(currentQuestionIndex);
            LoadQuestion();
        }
    
        private void LoadQuestion()
        {
            StartCoroutine(QuestionController.Instance.UpdateQuestionView());
            timer.RestartTimer();
        }
    }
}
