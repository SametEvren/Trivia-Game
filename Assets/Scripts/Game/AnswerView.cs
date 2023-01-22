using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game
{
    public class AnswerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private Image background;
        public string answerChoice;
        
        public void SetAnswer(string newAnswer) => answerText.text = newAnswer;

        public void SetBackgroundColor(Color color) => background.color = color;

        private void OnValidate()
        {
            Assert.IsNotNull(answerText);
            Assert.IsNotNull(background);
        }

        public void Render(AnswerModel answerModel)
        {
            SetAnswer(answerModel.text);
        }
    }
}