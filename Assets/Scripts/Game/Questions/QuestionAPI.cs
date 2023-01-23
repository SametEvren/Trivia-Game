using Newtonsoft.Json;
using UnityEngine;

namespace Game.Questions
{
    public class QuestionAPI : MonoBehaviour
    {
        #region Public Properties
        public static QuestionData questionData;
        #endregion
        
        private void Awake()
        {
            ReadDataFromJSON();
        }

        public static void ReadDataFromJSON()
        {
            TextAsset textJSON = (TextAsset)Resources.Load("questions", typeof(TextAsset));
            string jsonString = textJSON.text.Replace("\n","");
            questionData = JsonConvert.DeserializeObject<QuestionData>(jsonString);
        }
    }
    
}