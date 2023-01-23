using System;
using System.Collections;
using DG.Tweening;
using Game.Questions;
using Game.Score;
using Game.Skills;
using Sound;
using TMPro;
using UnityEngine;
using Utility;

namespace Game.Managers
{
    public class GameManager : Instancable<GameManager>
    {
        #region Public Properties
        public static event Action<string,string> OnQuestionAnswered;
        public static event Action<int> OnQuestionChanged;
        public int score;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI answerFeedback;
        public Timer.Timer timer;
        public int currentQuestionIndex;
        #endregion

        #region Private Properties
        private QuestionData _questionData;
        private ScoreConfig _scoreConfig;
        private bool _doubleChance;
        private string correctAnswer => _questionData
            .questions[currentQuestionIndex]
            .answer;
        #endregion
        
        private IEnumerator Start()
        {
            QuestionAPI.ReadDataFromJSON();
            yield return new WaitUntil(() => QuestionAPI.questionData != null);
            _questionData = QuestionAPI.questionData;
            
            ScoreAPI.ReadDataFromJSON();
            yield return new WaitUntil(() => ScoreAPI.scoreConfig != null);
            _scoreConfig = ScoreAPI.scoreConfig;

            SkillManager.OnSkillUsed += HandleSkillUsed;
            LoadQuestion();
            GetAndShowScore();
        }
        
        public void HandleQuestionAnswered(string chosenAnswer)
        {
            
            bool isWrongAnswer = correctAnswer != chosenAnswer;

            if (isWrongAnswer && _doubleChance)
            {
                _doubleChance = false;
                QuestionController.Instance.DisableAnswer(chosenAnswer);
                return;
            }
            
            OnQuestionAnswered?.Invoke(chosenAnswer,correctAnswer);

            if (chosenAnswer == String.Empty)
                UpdateScoreAndText(_scoreConfig.timeout);
            else
                UpdateScoreAndText(isWrongAnswer ? _scoreConfig.wrong : _scoreConfig.correct);

            AnswerFeedback(!isWrongAnswer);
            DOVirtual.DelayedCall(1f, ChangeQuestion);
        }

        private void AnswerFeedback(bool isCorrect)
        {
            answerFeedback.text = isCorrect ? "Correct Answer!" : "Wrong Answer!";
            AudioManager.Instance.PlaySFX("Button Select");
            AudioManager.Instance.PlaySFX(isCorrect ? "Correct" : "Wrong");
            answerFeedback.color = isCorrect ? Color.green : Color.red;
            answerFeedback.gameObject.SetActive(true);
            var sequence = DOTween.Sequence()
                .Append(answerFeedback.transform.DOScale(1.2f, 0.5f))
                .Append(answerFeedback.transform.DOScale(1f, 0.5f))
                .OnComplete(() =>
                {
                    answerFeedback.gameObject.SetActive(false);
                });
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
                })
                .OnComplete(() =>
                {
                    PlayerPrefs.SetInt("TotalScore", score);
                });
        }
    
        private void ChangeQuestion()
        {
            currentQuestionIndex++;
            currentQuestionIndex %= _questionData.questions.Count;

            _doubleChance = false;
            OnQuestionChanged?.Invoke(currentQuestionIndex);
            LoadQuestion();
        }
    
        private void LoadQuestion()
        {
            QuestionController.Instance.UpdateQuestionView();
            timer.RestartTimer();
        }
        
        private void HandleSkillUsed(SkillType skill)
        {
            AudioManager.Instance.PlaySFX("Button Select");
            switch (skill)
            {
                case SkillType.DoubleChance:
                    _doubleChance = true;
                    AudioManager.Instance.PlayDelayedSFX("Double Chance");
                    break;
                case SkillType.Bomb:
                    AudioManager.Instance.PlayDelayedSFX("Bomb");
                    var answersToBomb = HelperMethods.SelectRandomWrongChoices(correctAnswer);
                    foreach (var answer in answersToBomb)
                    {
                        QuestionController.Instance.DisableAnswer(answer);
                    }
                    break;
                case SkillType.Skip:
                    AudioManager.Instance.PlayDelayedSFX("Skip");
                    ChangeQuestion();
                    break;
                case SkillType.MagicAnswer:
                    AudioManager.Instance.PlayDelayedSFX("Magic Answer");
                    HandleQuestionAnswered(correctAnswer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skill), skill, null);
            }
        }

        private void GetAndShowScore()
        {
            score = PlayerPrefs.GetInt("TotalScore", 0);
            scoreText.text = score.ToString();
        }
    }
}
