using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.Answers
{
    public class AnswerView : MonoBehaviour
    {
        #region Private Properties
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private Image background;
        #endregion
        
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