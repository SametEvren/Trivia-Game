using System;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Answers
{
    public class AnswerController : MonoBehaviour
    {
        #region Private Properties
        [SerializeField] private AnswerView view;
        [SerializeField] private Button button;
        [SerializeField] private string optionName;
        private AnswerStatus _status = AnswerStatus.Default;
        #endregion
        
        private void Start()
        {
            GameManager.OnQuestionAnswered += ChangeButtonValidity;
            GameManager.OnQuestionChanged += ResetAnswer;
        }
        
        public void ChangeButtonStatus(AnswerStatus status)
        { 
            _status = status;
            button.interactable = status == AnswerStatus.Default;
            view.SetBackgroundColor(GetBackgroundColor());
        }

        private void ChangeButtonValidity(string chosenAnswer, string correctAnswer)
        {
            if (optionName != correctAnswer)
            {
                ChangeButtonStatus(optionName == chosenAnswer 
                    ? AnswerStatus.Incorrect 
                    : AnswerStatus.Inactive);
                return;
            }
            
            ChangeButtonStatus(AnswerStatus.Correct);
        }
        
        private void ResetAnswer(int questionIndex)
        {
            ChangeButtonStatus(AnswerStatus.Default);
        }

        private Color GetBackgroundColor()
        {
            switch (_status)
            {
                case AnswerStatus.Default:
                    return Color.white;
                case AnswerStatus.Correct:
                    return Color.green;
                case AnswerStatus.Incorrect:
                    return Color.red;
                case AnswerStatus.Inactive:
                    return Color.gray;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public enum AnswerStatus
    {
        Default,
        Correct,
        Incorrect,
        Inactive
    }
}